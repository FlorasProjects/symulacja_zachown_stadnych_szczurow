using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ratMovement : MonoBehaviour
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

        transform.rotation = Quaternion.Slerp( BoidSeparation(), transform.rotation, rotationSpeed * Time.deltaTime);
      //  transform.position += transform.forward * Time.deltaTime;
    }
   
    private void UpdateNearbyRats(ArrayList nearRats)
    {
        nearRats.Clear();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform rat = transform.parent.GetChild(i);
            if(rat.position.x - transform.position.x < checkRadius ||
               rat.position.y - transform.position.y < checkRadius ||
               rat.position.z - transform.position.z < checkRadius)
            {
                if(rat.position != transform.position)
                {
                    nearRats.Add(rat);
                }  
            }
        }
    }
    private Quaternion BoidSeparation()
    {
        Quaternion resultingQuaternion = Quaternion.identity;
        foreach (Transform rat in nearRats)
        {
            resultingQuaternion *= Quaternion.Inverse(rat.transform.rotation);
        }

        return resultingQuaternion;
    }

}
