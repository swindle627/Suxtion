using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // when game ends just turn off the player script and all enemy scripts

    [Header("Match Score Stats")]
    public float upgradePoints = 0;
    public float killPoints = 0;
    public float colorMatchPoints = 0;
    public float aoePoints = 0;
    public float suctionPoints = 0;
    public float survivalPoints = 0;
    public float matchScore = 0;

    [Header("Penalty")]
    public float refillPenalty = 0;

    [Header("Game data")]
    public float timeLeft;

    [Header("Point Values")]
    public float basicKillValue = 100; // kill enemy with object
    public float enemyKillValue = 200; // kill enemy with an enemy
    public float colorKillValue = 400; // kill enemy with an enemy of matching color

    [Header("Ingame UI")]
    [SerializeField] private TextMeshProUGUI upgradePointsText;
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI survivalPointText;
    [SerializeField] private TextMeshProUGUI killPointText;
    [SerializeField] private TextMeshProUGUI colorMatchText;
    [SerializeField] private TextMeshProUGUI upgradePointsFinal;
    [SerializeField] private TextMeshProUGUI suctionPointText;
    [SerializeField] private TextMeshProUGUI aoePointText;
    [SerializeField] private TextMeshProUGUI penaltiesText;
    [SerializeField] private TextMeshProUGUI matchScoreText;

    [Header("Spawn Objects")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] objects;

    [Header("Spawn Locations")]
    [SerializeField] private Vector3[] blueObjects; // 6 spawns
    [SerializeField] private Vector3[] blueEnemies; // 4 spawns

    [SerializeField] private Vector3[] yellowObjects; // 6 spawns
    [SerializeField] private Vector3[] yellowEnemies; // 4 spawns

    [SerializeField] private Vector3[] brownObjects; // 6 spawns
    [SerializeField] private Vector3[] brownEnemies; // 4 spawns

    [SerializeField] private Vector3[] peachObjects; // 6 spawns
    [SerializeField] private Vector3[] peachEnemies; // 4 spawns

    private bool isPaused = false, gameOver = false;
    private float originalTime, secondsLeft, objectTimer = 0, enemyTimer = 0;
    private int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        originalTime = timeLeft;
        secondsLeft = timeLeft;
        InitialSpawn();
    }
    // Update is called once per frame
    void Update()
    {
        if(secondsLeft == 0)
        {
            gameOver = true;
        }

        if(!gameOver)
        {
            upgradePointsText.text = upgradePoints.ToString();
            Countdown();

            if (timeLeft > 180)
            {
                Level1();
            }
            else if (timeLeft > 80)
            {
                Level2();
                level = 2;
            }
            else
            {
                Level3();
                level = 3;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPaused = !isPaused;
            }
        }
        else
        {
            isPaused = true;
            SurvialPoints();
            gameOverUI.SetActive(true);
            DisplayGameOver();
        }
        

        if(isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }

        
    }
    private void DisplayGameOver()
    {
        survivalPointText.text = "Survival Points: " + survivalPoints;
        killPointText.text = "Kill Points: " + killPoints;
        colorMatchText.text = "Color Match Points: " + colorMatchPoints;
        upgradePointsFinal.text = "Upgrade Points: " + upgradePoints;
        suctionPointText.text = "Suction Points: " + suctionPoints;
        aoePointText.text = "AoE Points: " + aoePoints;
        penaltiesText.text = "Penalities: -" + refillPenalty;

        matchScore = (survivalPoints + killPoints + colorMatchPoints + upgradePoints + suctionPoints + aoePoints) - refillPenalty;
        matchScoreText.text = "Match Score: " + matchScore;

        PlayerPrefs.SetFloat("score", matchScore);
    }
    // Handles AI and object spawning
    private void Level1()
    {
        enemyTimer += Time.deltaTime;
        objectTimer += Time.deltaTime;

        if(enemyTimer > 10)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < 12)
            {
                Instantiate(enemies[0], blueEnemies[Random.Range(0, blueEnemies.Length)], Quaternion.identity);
                Instantiate(enemies[0], yellowEnemies[Random.Range(0, yellowEnemies.Length)], Quaternion.identity);
                Instantiate(enemies[0], brownEnemies[Random.Range(0, brownEnemies.Length)], Quaternion.identity);
                Instantiate(enemies[0], peachEnemies[Random.Range(0, peachEnemies.Length)], Quaternion.identity);
            }

            enemyTimer = 0;
        }

        if(objectTimer > 15)
        {
            DespawnObjects();
            SpawnObjects();
            objectTimer = 0;
        }
    }
    private void Level2()
    {
        enemyTimer += Time.deltaTime;
        objectTimer += Time.deltaTime;

        if (enemyTimer > 12)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < 36)
            {
                int posOne = Random.Range(0, 2);
                int posTwo = Random.Range(2, 4);

                Instantiate(enemies[0], blueEnemies[posOne], Quaternion.identity);
                Instantiate(enemies[0], blueEnemies[posTwo], Quaternion.identity);
                Instantiate(enemies[0], yellowEnemies[posOne], Quaternion.identity);
                Instantiate(enemies[0], yellowEnemies[posTwo], Quaternion.identity);
                Instantiate(enemies[0], brownEnemies[posOne], Quaternion.identity);
                Instantiate(enemies[0], brownEnemies[posTwo], Quaternion.identity);
                Instantiate(enemies[0], peachEnemies[posOne], Quaternion.identity);
                Instantiate(enemies[0], peachEnemies[posTwo], Quaternion.identity);
            }

            enemyTimer = 0;
        }

        if (objectTimer > 12)
        {
            DespawnObjects();
            SpawnObjects();
            objectTimer = 0;
        }
    }
    private void Level3()
    {
        enemyTimer += Time.deltaTime;
        objectTimer += Time.deltaTime;

        if (enemyTimer > 15)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < 48)
            {
                for (int i = 0; i < 4; i++)
                {
                    Instantiate(enemies[0], blueEnemies[i], Quaternion.identity);
                    Instantiate(enemies[0], yellowEnemies[i], Quaternion.identity);
                    Instantiate(enemies[0], brownEnemies[i], Quaternion.identity);
                    Instantiate(enemies[0], peachEnemies[i], Quaternion.identity);
                }
            }

            enemyTimer = 0;
        }

        if (objectTimer > 10)
        {
            DespawnObjects();
            SpawnObjects();
            objectTimer = 0;
        }
    }
    private void InitialSpawn()
    {
        Instantiate(enemies[0], blueEnemies[Random.Range(0, blueEnemies.Length)], Quaternion.identity);
        Instantiate(enemies[0], yellowEnemies[Random.Range(0, blueEnemies.Length)], Quaternion.identity);
        Instantiate(enemies[0], brownEnemies[Random.Range(0, blueEnemies.Length)], Quaternion.identity);
        Instantiate(enemies[0], peachEnemies[Random.Range(0, blueEnemies.Length)], Quaternion.identity);

        SpawnObjects();
    }
    private void SpawnObjects()
    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(objects[Random.Range(0, objects.Length)], blueObjects[i], Quaternion.identity);
            Instantiate(objects[Random.Range(0, objects.Length)], yellowObjects[i], Quaternion.identity);
            Instantiate(objects[Random.Range(0, objects.Length)], brownObjects[i], Quaternion.identity);
            Instantiate(objects[Random.Range(0, objects.Length)], peachObjects[i], Quaternion.identity);
        }
    }
    private void DespawnObjects()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Suckable");

        foreach(GameObject obj in objs)
        {
            if(obj.GetComponent<Rigidbody>().velocity.magnitude == 0)
            {
                Destroy(obj);
            }
        }

    }
    // handles timer countdown
    private void Countdown()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }

        secondsLeft = Mathf.FloorToInt(timeLeft);
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);

        string timerText = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeLeftText.text = timerText;
    }
    // the following methods are used to increase points
    public void KillPoints()
    {
        killPoints += 100;
    }
    public void ColorMatchPoints()
    {
        colorMatchPoints += 1000;
    }
    public void AreaPoints()
    {
        aoePoints += 100;
    }
    public void SuctionPoints()
    {
        suctionPoints += 50;
    }
    public void SurvialPoints()
    {
        survivalPoints = (originalTime - secondsLeft) * 10;
    }
    // pause and resume game
    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public bool GetPause()
    {
        return isPaused;
    }
    // used by player controller to end game when player dies
    public void EndGame()
    {
        gameOver = true;
    }
    // used by ranged enemy to increase speed as game goes on
    public int GetLevel()
    {
        return level;
    }
}
