using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Boss : MonoBehaviour
{
    public GameObject bullet;

    public Transform enemySpawn;

    public bool spawn;

    public Animator ani;

    public int bossHP, bossHpMax;

    public Slider bossHpBar;

    public float spawnSpeed;

    private void Start()
    {
        if (GameObject.Find("player").GetComponent<Player>().stage == 2)
        {
            bossHpMax *= 2;
        }
        
        spawn = true;
        bossHP = bossHpMax;
        StartCoroutine("NextAni");

        if (GameObject.Find("player").GetComponent<Player>().stage == 1)
        {
            spawnSpeed = 3.0f;
        }
        else if (GameObject.Find("player").GetComponent<Player>().stage == 2)
        {
            spawnSpeed = 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn == true)
        {
            StartCoroutine("EnemySpawn");
        }
        
        bossHpBar.value = (float)bossHP / (float)bossHpMax;
    }
    
    IEnumerator EnemySpawn()
    {
        spawn = false;
        Instantiate(bullet, enemySpawn.position, quaternion.identity);
        yield return new WaitForSeconds(spawnSpeed);
        spawn = true;
    }

    IEnumerator NextAni()
    {
        yield return new WaitForSeconds(5.0f);
        ani.SetBool("mov", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            bossHP -= GameObject.Find("player").GetComponent<Player>().bulletPower;

            if (bossHP <= 0)
            {
                GameObject.Find("player").GetComponent<Player>().score += 100;
                GameObject.Find("player").GetComponent<Player>().Clear();

                if (GameObject.Find("player").GetComponent<Player>().stage == 1)
                {
                    GameObject.Find("SpawnManager").GetComponent<SpawnManager>().NextStage();
                    GameObject.Find("player").GetComponent<Player>().stage += 1;
                }
                else if (GameObject.Find("player").GetComponent<Player>().stage == 2)
                {
                    PlayerPrefs.SetInt("_Score", GameObject.Find("player").GetComponent<Player>().score);
                    SceneManager.LoadScene("Final");
                }
                Destroy(gameObject);
            }
            
            Destroy(other.gameObject);
        }
    }
}
