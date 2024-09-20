using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class RatMovement : MonoBehaviour
{
    public float checkFrequency;
    public float checkRadius;
    public Vector3 ratRotation;
    [Range(0.1f, 1f)]
    public float rotationSpeed;
    [Range(1f, 5f)]
    public float movementSpeed;
    ArrayList nearRats;

    Vector3 lookVector = Vector3.zero;
    void Start()
    {
        nearRats = new ArrayList();
    }

    void Update()
    {
        UpdateNearbyRats(nearRats);
       
        if (nearRats.Count > 0)
         {
            lookVector = BoidSeparation();
            Debug.Log(lookVector);
            //transform.LookAt(lookVector);
            Vector3 relativePos = lookVector - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Slerp(Quaternion.identity,rotation,rotationSpeed); ;
            transform.position += movementSpeed * Time.deltaTime * transform.forward;
         }
        //Debug.Log(BoidAlignment());
        // transform.rotation *= Quaternion.Slerp(Quaternion.identity,  Quaternion.Euler(BoidSeparation().normalized), rotationSpeed * Time.deltaTime);
        //Quaternion.Euler(0f, BoidSeparation(), 0f);
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
                    /* for(int j = 0; j < checkRadius - Mathf.Abs(rat.position.x - transform.position.x); j++)
                     {
                         nearRats.Add(rat);
                     }
                     for (int k = 0; k < checkRadius - Mathf.Abs(rat.position.z - transform.position.z); k++)
                     {
                         nearRats.Add(rat);
                     }*/
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
        return result;
    }
    private Vector3 BoidAlignment()
    {
        Vector3 resultingVector = transform.forward;
        Vector3 tempVector = Vector3.zero;
        foreach(Transform rat in nearRats)
        {
            tempVector += rat.position;
        }

        resultingVector.y = 0f;
        return resultingVector;
    }
}
