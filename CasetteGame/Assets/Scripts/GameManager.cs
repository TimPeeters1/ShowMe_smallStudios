using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;

    }
    #endregion

    public List<FallThing> objects;

    [Space]
    public float speedMultiplier;
    public float moveSpeed;

    [Space]
    [Header("Score UI")]
    public int highScore;
    public int score;

    public Text scoreText;
    public Text highscoreText;

    [Space]
    [Header("Pause Menu")]
    public GameObject pauseMenu;

    GameObject cameraObject;
    Spawner spawner;
    Casette player;

    Vector3 _originalPos;

    bool isPaused = false;
    public void doPause()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    private void Start()
    {
        //highScore = PlayerPrefs.GetInt("Highscore", highScore);
        score = 0;

        Application.targetFrameRate = 60;
        Time.timeScale = 1;

        cameraObject = Camera.main.gameObject;
        spawner = FindObjectOfType<Spawner>();
        player = FindObjectOfType<Casette>();
        spawner.enemySpeed = moveSpeed;

        scoreText.text = score.ToString();
        highscoreText.text = highScore.ToString();

        _originalPos = cameraObject.transform.localPosition;

        pauseMenu.SetActive(false);
    }

    private void Update()
    {
       
    }

    public void addScore(int points)
    {
        score += points;

        scoreText.text = score.ToString();
        highscoreText.text = highScore.ToString();

        moveSpeed += speedMultiplier;
        player.maxMoveSpeed += speedMultiplier/10f;

        foreach (FallThing obj in objects)
        {
            obj.moveSpeed = moveSpeed;
        }

        spawner.enemySpeed = moveSpeed;
    }

    public IEnumerator CameraShake(float _duration, float _magnitude)
    {

        float _elapsed = 0.0f;

        while (_elapsed < _duration)
        {
            float x = Random.Range(-1f, 1f) * _magnitude;
            float y = Random.Range(-1f, 1f) * _magnitude;

            cameraObject.transform.localPosition = new Vector3(_originalPos.x + x, _originalPos.y + y, _originalPos.z);

            _elapsed += Time.deltaTime;

            yield return null;
        }

        cameraObject.transform.localPosition = _originalPos;
    }
}
