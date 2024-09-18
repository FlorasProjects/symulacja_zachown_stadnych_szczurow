using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class RatMovement : MonoBehaviour
{
    public float checkFrequency;
    public float checkRadius;
    public Vector3 ratRotation;
    public float rotationSpeed;
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
            transform.position += BoidSeparation().normalized * Time.deltaTime * rotationSpeed;
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
        Debug.Log(nearRats.Count);
    }
    private Vector3 BoidSeparation()
    {
        Vector3 resultingVector = transform.position;
        foreach (Transform rat in nearRats)
        {
            resultingVector -= rat.position;
        }
        resultingVector.y = 0.25f;
        return resultingVector;
    }
    private void Boidalignment()
    {

    }
}
