using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class addRats : MonoBehaviour
{
    public GameObject ratPrefab;
    public int numberOfRats;
    public float radius = 6f;
    public List<GameObject> ratList;

    void Start()
    {
        CircleSpawn();
    }
    public void RandomSpawn()
    {
        for (int i = 0; i < numberOfRats; i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-5.0f, 5.0f), 0f, Random.Range(-5.0f, 5.0f));
            Quaternion rot = Quaternion.Euler(0, Random.Range(-30.0f, 30.0f), 0);
            var rat = Instantiate(ratPrefab, pos, rot, transform);
            Debug.Log(rat);
            ratList.Add(rat);
        }
    }
    public void CircleSpawn()
    {
        for (int i = 0; i < numberOfRats; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfRats;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg - 90;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(ratPrefab, pos, rot, transform);
        }
    }
}
