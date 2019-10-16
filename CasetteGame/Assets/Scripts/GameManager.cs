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

    GameObject cameraObject;
    Spawner spawner;
    Casette player;

    private void Start()
    {
        //highScore = PlayerPrefs.GetInt("Highscore", highScore);
        score = 0;

        Application.targetFrameRate = 60;

        cameraObject = Camera.main.gameObject;
        spawner = FindObjectOfType<Spawner>();
        player = FindObjectOfType<Casette>();
        spawner.enemySpeed = moveSpeed;
    }

    private void Update()
    {
       
    }

    public void Damage()
    {
        
    }

    public void addScore(int points)
    {

        score += points;

        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "Highscore: " + highScore.ToString();

        moveSpeed += speedMultiplier;
        player.moveSpeed += speedMultiplier/10f;

        foreach (FallThing obj in objects)
        {
            obj.moveSpeed = moveSpeed;
        }

        spawner.enemySpeed = moveSpeed;
    }

    public IEnumerator CameraShake(float _duration, float _magnitude)
    {
        Vector3 _originalPos = cameraObject.transform.localPosition;

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
