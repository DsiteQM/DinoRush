using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObstacle
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObstacle[] obstacles;

    public float spawnTime;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), spawnTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach ( var obs in obstacles ) 
        { 
            if(spawnChance < obs.spawnChance ) 
            {
                GameObject obstacle = Instantiate(obs.prefab);
                obstacle.transform.position += transform.position;
                break;  
            }
            spawnChance -= obs.spawnChance;
        }
        Invoke(nameof(Spawn), spawnTime);

    }
}
