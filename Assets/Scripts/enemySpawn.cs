using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemySpawnPoint;
    private GameObject Enemy;

    void Start()
    {
        Enemy = Instantiate(enemyPrefab, enemySpawnPoint.transform.position, Quaternion.identity);
    }
}
