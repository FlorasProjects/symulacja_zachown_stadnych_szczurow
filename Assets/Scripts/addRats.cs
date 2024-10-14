using System.Collections.Generic;
using UnityEngine;


public class AddRats : MonoBehaviour
{
    public GameObject ratPrefab;
    public int numberOfRats;
    public float radius = -10f;
    public List<GameObject> ratList;

    void Start()
    {
        RandomSpawn();
        //CircleSpawn();
    }
    public void RandomSpawn()
    {
        for (int i = 0; i < numberOfRats; i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-1 * radius, radius), 0.25f, Random.Range(-1 * radius, radius));
            Quaternion rot = Quaternion.Euler(0, Random.Range(-180.0f, 180.0f), 0f);
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
