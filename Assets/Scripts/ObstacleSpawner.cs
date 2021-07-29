using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawn = 10f;
    private float timeSinceSpawn;
    private ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= timeBetweenSpawn)
        {
            GameObject newObj = objectPooler.GetObject();
            timeSinceSpawn = 0f;
        }
    }
}
