using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    public float initialGameSpeed = 3f;
    public float gameSpeedIncrease = 0.3f;
    public float gameSpeed { get; private set; }

    public bool parallaxController;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI onemoreText;
    public Button retry;

    private Dino dino;
    private ObstacleSpawner obstacleSpawner;

    private float score;
    private void Awake()
    {
        if ( Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance= null;
        }
    }

    private void Start()
    {
        dino = FindObjectOfType<Dino>();
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        score = 0f;
        enabled= true;

        dino.gameObject.SetActive(true);
        obstacleSpawner.gameObject.SetActive(true);
        onemoreText.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);

        parallaxController = true;
        UpdateHiscore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled= false;
        dino.gameObject.SetActive(false);
        obstacleSpawner.gameObject.SetActive(false);
        onemoreText.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);

        parallaxController = false;
        UpdateHiscore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;

        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore) 
        {
            hiscore= score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }
        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
