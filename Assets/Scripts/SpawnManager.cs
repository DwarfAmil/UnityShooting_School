using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject meteo, boss;
    
    public Transform spawnPoint;

    public bool bossSpawn, spawn;

    private void Start()
    {
        bossSpawn = false;
        spawn = true;
    }

    private void Update()
    {
        if (bossSpawn == false && spawn == true)
        {
            StartCoroutine("EnemySpawn");
        }
    }

    IEnumerator EnemySpawn()
    {
        spawn = false;
        Invoke("SpawnObject", 1.0f);
        yield return new WaitForSeconds(5.0f);
        spawn = true;
    }

    void SpawnObject()
    {
        spawnPoint.position = new Vector3(Random.Range(-34, 33), 20, 0);
        Instantiate(meteo, spawnPoint.position, Quaternion.identity);
    }

    public void BossSpawn()
    {
        bossSpawn = true;
        
        spawnPoint.position = new Vector3(Random.Range(0, 0), 15, 0);
        Instantiate(boss, spawnPoint.position, Quaternion.identity);
    }

    public void NextStage()
    {
        GameObject.Find("player").GetComponent<Player>().enemyCount = 20;
        StartCoroutine("StageCtrl");
    }

    IEnumerator StageCtrl()
    {
        yield return new WaitForSeconds(3.0f);
        bossSpawn = false;
    }
}
