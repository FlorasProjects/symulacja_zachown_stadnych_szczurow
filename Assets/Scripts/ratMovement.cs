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
        Debug.DrawRay(transform.position, BoidSeparation(), Color.red);
       // Debug.DrawRay(transform.position, new Vector3(0f, BoidSeparation(), 0f), Color.red);
        if (nearRats.Count > 0)
         {
            transform.position += movementSpeed * Time.deltaTime * transform.forward;
            Vector3 lookVector = BoidSeparation();
            transform.LookAt(lookVector);
            //transform.LookAt(BoidSeparation());
           //transform.rotation *=  Quaternion.Euler(0f, BoidSeparation(), 0f);
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
    private Vector3 BoidSeparation()
    {
        Vector3 tempVector = transform.position;
        foreach (Transform rat in nearRats)
        {
            tempVector += transform.position - rat.position;
        }
        tempVector.y = 0.25f;
        return tempVector;
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
