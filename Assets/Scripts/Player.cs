using System;
using UnityEngine;
using System.Collections;
using TMPro;
using TMPro.Examples;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Player : MonoBehaviour

{
    public float moveSpeed = 5.0f;

    public float skillCool01, skillCool02;
    
    public GameObject bullet;

    public Transform firePos;

    public int playerHP, playerHPmax, playerFuelMax, score, stage;

    public float playerFuel;
    
    public Slider playerHPbar, playerFuelBar;

    public TextMeshProUGUI scoreText, noticeText, skillCoolText01, skillCoolText02, highScoreText, godModeText, bulletPowerText, enemyCountText, stageText, enemyHpText;

    public GameObject noticeUI, godUI, clearUI;

    public GameObject skillBomb;

    public int highScore, bulletPower = 1;
    
    public int enemyCountMax, enemyCount;

    public bool god;

    private void Start()
    {
        score = 0;
        stage = 1;
        scoreText.text = score.ToString();
        playerHP = playerHPmax;
        playerFuel = playerFuelMax;
        noticeUI.SetActive(false);
        skillCool01 = 0;
        skillCool02 = 0;
        highScore = PlayerPrefs.GetInt("_HighScore", 0);
        highScoreText.text = highScore.ToString();
        god = false;
        godModeText.color = Color.gray;
        bulletPowerText.text = "Bullet Power : " + bulletPower;
        enemyCount = enemyCountMax;
        clearUI.SetActive(false);
        enemyCountText.text = enemyCountMax.ToString() + " / " + enemyCount.ToString();
    }

    void Update()
    {
        float distanceX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float distanceY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        this.gameObject.transform.Translate(distanceX, 0, distanceY);

        skillCool01 -= Time.deltaTime;

        skillCool02 -= Time.deltaTime;
        if ((int)skillCool01 != 0)
        {
            skillCoolText01.text = ((int)skillCool01).ToString();
        }
        else
        {
            skillCoolText01.text = "";
        }

        if ((int)skillCool02 != 0)
        {
            skillCoolText02.text = ((int)skillCool02).ToString();
        }
        else
        {
            skillCoolText02.text = "";
        }
        
        if (skillCool01 <= 0)
        {
            skillCool01 = 0;
        }

        if (skillCool02 <= 0)
        {
            skillCool02 = 0;
        }

        if (distanceX + distanceY != 0)
        { 
            playerFuel -= 0.001f;
        }

        if (this.transform.position.x < -15)
        {
            this.transform.position = new Vector3(-15, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.x > 15)
        {
            this.transform.position = new Vector3(15, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.y < -19)
        {
            this.transform.position = new Vector3(this.transform.position.x, -19, this.transform.position.z);
        }

        if (this.transform.position.y > 14)
        {
            this.transform.position = new Vector3(this.transform.position.x, 14, this.transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, firePos.position, Quaternion.identity);
        }

        playerHPbar.value = (float)playerHP / (float)playerHPmax;
        playerFuelBar.value = (float)playerFuel / (float)playerFuelMax;
        scoreText.text = score.ToString();

        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("_HighScore", highScore);
            highScoreText.text = highScore.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SkillHP();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SkillExplosion();
        }
        
        if (enemyCount >= 0)
        {
            enemyCountText.text = enemyCountMax.ToString() + " / " + enemyCount.ToString();
        }

        if (stage == 2)
        {
            stageText.text = "STAGE." + stage.ToString();
            enemyHpText.text = "enemy hp : 8";
        }

        if (playerHP <= 0 || playerFuel <= 0)
        {
            PlayerPrefs.SetInt("_Score", score);
            SceneManager.LoadScene("Final");
        }
        
        //enemyCountText.text = enemyCountMax.ToString() + " / " + enemyCount.ToString();
    }

    public void ScorePlus(int num)
    {
        score += num;
        scoreText.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HP")
        {
            playerHP += other.GetComponent<Item>().hpPlus;

            if (playerHP >= playerHPmax)
            {
                playerHP = playerHPmax;
            }
            
            ShowNotice("HP plus 30!");
            
            Destroy(other.gameObject);
        }
        else if (other.tag == "Fuel")
        {
            playerFuel += other.GetComponent<Item>().fuelPlus;

            if (playerFuel >= playerFuelMax)
            {
                playerFuel = playerFuelMax;
            }
            
            ShowNotice("Fuel plus 30!");
            
            Destroy(other.gameObject);
        }
        else if (other.tag == "Power")
        {
            if (bulletPower < 4)
            {
                bulletPower += other.GetComponent<Item>().powerPlus;
                bulletPowerText.text = "Bullet Power : " + bulletPower;
                ShowNotice("Power Level plus 1!");
            }
            else if (bulletPower >= 4)
            {
                score += 50;
                ShowNotice("Power Level Max!");
            }

            Destroy(other.gameObject);
        }
        else if (other.tag == "God")
        {
            StartCoroutine("GodMode");

            Destroy(other.gameObject);
        }

        if (other.tag == "BossBullet")
        {
            if(god == false) 
                playerHP -= 10;
            
            Destroy(other.gameObject);
        }
        
        if (other.tag == "Boss")
        {
            if(god == false) 
                playerHP -= 10;
        }
    }

    public void ShowNotice(string notice)
    {
        noticeText.text = notice;
        noticeUI.SetActive(true);

        StartCoroutine("NoticeFalse");
    }

    IEnumerator NoticeFalse()
    {
        yield return new WaitForSeconds(1.0f);
        noticeUI.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        noticeUI.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        noticeUI.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        noticeUI.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        noticeUI.SetActive(false);
    }

    public void SkillHP()
    {
        if (skillCool01 <= 0)
        {
            playerHP = playerHPmax;
            skillCool01 = 100;
        }
    }

    public void SkillExplosion()
    {
        if (skillCool02 <= 0)
        {
            Instantiate(skillBomb, new Vector3(0, 0, 0), quaternion.identity);
            skillCool02 = 100;
        }
    }

    IEnumerator GodMode()
    {
        god = true;
        godModeText.color = Color.yellow;
        ShowNotice("GodMode ON!");
        yield return new WaitForSeconds(2.5f);
        godUI.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        godUI.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        godUI.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        godUI.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        godUI.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        godUI.SetActive(true);
        godModeText.color = Color.gray;
        god = false;
    }

    IEnumerator StageClear()
    {
        yield return new WaitForSeconds(0.5f);
        clearUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        clearUI.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        clearUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        clearUI.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        clearUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        clearUI.SetActive(false);
    }

    public void Clear()
    {
        StartCoroutine("StageClear");
    }
}