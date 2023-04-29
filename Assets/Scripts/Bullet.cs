using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 20.0f;

    public GameObject hitEffect;
    public Player player;

    public GameObject[] item;

    public TextMeshProUGUI enemyCountText;

    private void Start()
    { 
        player = GameObject.Find("player").GetComponent<Player>();
    }

    void Update()
    {
        float distanceY = Time.deltaTime * moveSpeed;
        this.gameObject.transform.Translate(0, distanceY, 0);
        
        if (transform.position.y >= 20.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            other.GetComponent<Meteo>().meteoHP -= player.bulletPower;

            if (other.GetComponent<Meteo>().meteoHP <= 0)
            {
                int randomResult = Random.Range(0, 4);

                if (randomResult == 0)
                {
                    Instantiate(item[0], transform.position, Quaternion.identity);
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                }
                else if(randomResult == 1)
                {
                    Instantiate(item[1], transform.position, Quaternion.identity);
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                }
                else if (randomResult == 2)
                {
                    Instantiate(item[2], transform.position, Quaternion.identity);
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                }
                else if (randomResult == 3)
                {
                    Instantiate(item[3], transform.position, Quaternion.identity);
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                }
                else
                {
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                }
                GameObject.Find("player").GetComponent<Player>().score += 10;
                
                player.enemyCount--;

                if (player.enemyCount == 0)
                {
                    GameObject.Find("SpawnManager").GetComponent<SpawnManager>().BossSpawn();
                }
            }
            Destroy(gameObject);
        }
    }

    public void ItemDrop()
    {
        int randomResult = Random.Range(0, 4);

        if (randomResult == 0)
        {
            Instantiate(item[0], transform.position, Quaternion.identity);
        }
        else if(randomResult == 1)
        {
            Instantiate(item[1], transform.position, Quaternion.identity);
        }
        else if (randomResult == 2)
        {
            Instantiate(item[2], transform.position, Quaternion.identity);
        }
        else if (randomResult == 3)
        {
            Instantiate(item[3], transform.position, Quaternion.identity);
        }
    }
}
