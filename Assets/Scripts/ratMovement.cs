using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class RatMovement : MonoBehaviour
{
    public float checkRadius;
    [Range(0.1f, 1f)]
    public float rotationSpeed;
    [Range(1f, 10f)]
    public float movementSpeed;
    ArrayList nearRats;
    float[] boidWeights = { 2f, 2f, 1f };

    void Start()
    {
        nearRats = new ArrayList();
    }

    void Update()
    {
        UpdateNearbyRats(nearRats);
       
        if (nearRats.Count > 0)
         {
            RotateRat(BoidSeparation() + BoidAlignment() + BoidCohesion());
            //Debug.Log();
            transform.position += movementSpeed * Time.deltaTime * transform.forward;
         }
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
