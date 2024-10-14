using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RatMovement : MonoBehaviour
{
    public float checkRadius;
    [Range(0.1f, 1f)]
    public float rotationSpeed;
    [Range(1f, 10f)]
    public float movementSpeed;
    ArrayList nearRats;
    float[] boidWeights = { 0.5f, 0.05f, 0.4f };
    private Vector3 targetPos = new Vector3(4,0.33f,22);
    private Vector3 proxWeightDisable = new Vector3(3, 0, 3);
    private Vector3 currentPos = Vector3.zero;
    public NavMeshAgent agent;
    void Start()
    {
        nearRats = new ArrayList();
    }

    void Update()
    {
        UpdateNearbyRats(nearRats);
       
        if (nearRats.Count > 0)
         {
            // RotateRat(BoidSeparation() + BoidAlignment() + BoidCohesion());
            //Debug.Log();
            //transform.position += movementSpeed * Time.deltaTime * transform.forward;
            //Vector3.Scale(new Vector3(1, 2, 3), new Vector3(2, 3, 4))
            AIMove(targetPos + BoidSeparation() + BoidAlignment() + BoidCohesion());
            currentPos = targetPos - transform.position;
            if(currentPos.x <= proxWeightDisable.x && currentPos.y <= proxWeightDisable.y)
            {
                boidWeights[0] = boidWeights[1] = boidWeights[2] = 0;
            }
            if(boidWeights[0] != 0)
            {
                Debug.Log(currentPos);
            }
         }
    }
    private void AIMove(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void UpdateNearbyRats(ArrayList nearRats)
    {
        nearRats.Clear();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform rat = transform.parent.GetChild(i);
            
            if (Mathf.Abs(rat.position.x - transform.position.x) < checkRadius &&
               Mathf.Abs(rat.position.y - transform.position.y) < checkRadius &&
               Mathf.Abs(rat.position.z - transform.position.z) < checkRadius)
            {
                if (rat.transform != this.transform)
                {
                    nearRats.Add(rat);
                }
            }
        }
    }
    private Vector3 BoidSeparation()
    {
        Vector3 result = transform.position;
        foreach (Transform rat in nearRats)
        {
            result += transform.position - rat.position;
        }
        result.y = 0.25f;
        return result * boidWeights[0];
    }
    private Vector3 BoidAlignment()
    {
        Vector3 result = transform.forward + transform.position;
        foreach (Transform rat in nearRats)
        {
            result += rat.forward / nearRats.Count;
        }
        result.y = 0.25f;
        return result * boidWeights[1];
    }
    public Vector3 BoidCohesion()
    {
        Vector3 result = transform.position;
        foreach (Transform rat in nearRats)
        {
            result += rat.position - transform.position;
        }
        result.y = 0.25f;
        return result * boidWeights[2];
    }
    public void RotateRat(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        if (directionToTarget == Vector3.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime );
    }
}
