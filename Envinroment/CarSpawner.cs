using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public float spawnInterval = 2f;
    public float delay = 1f;

    private void Start()
    {
        InvokeRepeating("SpawnCar", spawnInterval, delay);
    }

    void SpawnCar()
    {
        Instantiate(carPrefab, transform.position, transform.rotation);
    }
}
