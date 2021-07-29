using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle")]
    [SerializeField] private float startXPosition = 20f;
    [SerializeField] private float endXPosition = -20f;
    [SerializeField] private float YRange = 2f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Obstacle Distance")]
    [SerializeField] private GameObject topObstacle;
    [SerializeField] private GameObject bottomObstacle;
    [SerializeField] private float MaxYRange = 15f;
    [SerializeField] private float MinYRange = 22f;

    private ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void OnEnable()
    {
        SetObstaclePosition();
    }

    void FixedUpdate()
    {
        float newPosition = transform.position.x - (moveSpeed * Time.deltaTime);
        transform.position = new Vector2(newPosition, transform.position.y);

        if (transform.position.x <= endXPosition)
        {
            objectPooler.ReturnObject(gameObject);
        }
    }

    private void SetObstaclePosition()
    {
        float distanceBetweenCenters = Random.Range(MinYRange, MaxYRange) / 2;
        topObstacle.transform.position = new Vector3(transform.position.x, distanceBetweenCenters, transform.position.z);
        bottomObstacle.transform.position = new Vector3(transform.position.x, -distanceBetweenCenters, transform.position.z);

        float obstacleYPosition = Random.Range(- YRange, YRange);
        transform.position = new Vector2(startXPosition, obstacleYPosition);
    }
}
