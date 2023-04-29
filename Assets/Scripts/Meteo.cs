using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Meteo : MonoBehaviour
{
    public float moveSpeed = 20.0f;

    public int damage = 1;

    public int meteoHP, meteoHpMax;

    private void Start()
    {
        if (GameObject.Find("player").GetComponent<Player>().stage == 2)
        {
            meteoHpMax *= 2;
        }
        meteoHP = meteoHpMax;
    }

    void Update()
    {
        float distanceY = Time.deltaTime * moveSpeed;
        this.gameObject.transform.Translate(0, -distanceY, 0);

        if (transform.position.y <= -20.0f)
        {
            Destroy(gameObject);
        }

        if (meteoHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("충돌");
            if (other.GetComponent<Player>().god == false)
            {
                other.GetComponent<Player>().playerHP -= damage;
            }
            else
            {
                other.GetComponent<Player>().score += 10;
            }
            Destroy(gameObject);
            
        }
    }
}
