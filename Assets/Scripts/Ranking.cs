using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public int[] rank = new int[10];

    public string[] name = new string[10];

    public string player, nameKeep;

    public TextMeshProUGUI[] rankText;

    public int  scoreKeep, score;

     private void Awake()
    {
        int a = 0;
        
        for (int i = 0; i < rank.Length; i++)
        {
            rank[i] = PlayerPrefs.GetInt((i + 1) + "rank", 0);
            name[i] = PlayerPrefs.GetString((i + 1) + "name", "");
        }
        
        score = PlayerPrefs.GetInt("_Score", 0);
        player = PlayerPrefs.GetString("_PlayerName", "");

        for (int j = 0; j < rank.Length; j++)
        {
            if (score != rank[j])
            {
                a++;
            }
        }

        if (score > rank[9] && a == 10)
        {
            rank[9] = PlayerPrefs.GetInt("_Score", 0);
            name[9] = PlayerPrefs.GetString("_PlayerName", "");
            SortRanking();
        }
    }

    private void Start()
    {
        for (int y = 0; y < 10; y++)
        {
            if (rank[y] != 0)
            {
                if(name[y] != "")
                    rankText[y].text = (y + 1).ToString() + ". " + name[y] + " / " + rank[y].ToString();
                else if (name[y] == "")
                {
                    rankText[y].text = (y + 1).ToString() + ". noname / " + rank[y].ToString();
                }
            }
        }
    }

    void SortRanking()
    {
        for (int i = 0; i < (rank.Length - 1); i++)
        {
            if (rank[9] > rank[i])
            {
                scoreKeep = rank[i];
                rank[i] = rank[9];
                rank[9] = scoreKeep;

                nameKeep = name[i];
                name[i] = name[9];
                name[9] = nameKeep;
            }
        }

        for (int j = 0; j < rank.Length; j++)
        {
            PlayerPrefs.SetInt((j + 1) + "rank", rank[j]);
            PlayerPrefs.SetString((j + 1) + "name", name[j]);
        }
    }
}
