using System;
using System.Collections;
using UnityEngine;


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

    void Start()
    {
        nearRats = new ArrayList();
    }

    void Update()
    {
        UpdateNearbyRats(nearRats);

         if(nearRats.Count > 0 )
         {
             transform.position += movementSpeed * Time.deltaTime * transform.forward;
             transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, BoidSeparation(), 0f), rotationSpeed);
            //transform.LookAt(BoidSeparation());

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
                    nearRats.Add(rat);
                }
            }
        }
    }
    private float BoidSeparation()
    {
        Vector3 resultingVector = transform.position;
        foreach (Transform rat in nearRats)
        {
            resultingVector -= rat.position;
        }
        
        resultingVector.y = 0f;
        return Vector3.Angle(new Vector3(transform.forward.x, 0f, transform.forward.z), resultingVector);
    }
    private Vector3 BoidAlignment()
    {
        Vector3 resultingVector = transform.forward;
        foreach(Transform rat in nearRats)
        {
            resultingVector = Vector3.Cross(resultingVector, rat.forward);      
        }
        resultingVector.y = 0.25f;
        return resultingVector/nearRats.Count;
    }
}
