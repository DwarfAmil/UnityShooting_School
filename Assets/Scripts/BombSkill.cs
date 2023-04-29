using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombSkill : MonoBehaviour
{
    public GameObject[] item;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Meteo>().meteoHP -= 10;
            
            int randomResult = Random.Range(0, 4);

            if (randomResult == 0)
            {
                Instantiate(item[0], other.transform.position, Quaternion.identity);
            }
            else if(randomResult == 1)
            {
                Instantiate(item[1], other.transform.position, Quaternion.identity);
            }
            else if (randomResult == 2)
            {
                Instantiate(item[2], other.transform.position, Quaternion.identity);
            }
            else if (randomResult == 3)
            {
                Instantiate(item[3], other.transform.position, Quaternion.identity);
            }
            
            Debug.Log("sadfsadfsadf");
        }
    }

    void Update()
    {
        StartCoroutine("BombDel");
    }
    
    IEnumerator BombDel()
    {
        yield return new WaitForSeconds(1.0f);
        
        Destroy(gameObject);
    }
}
