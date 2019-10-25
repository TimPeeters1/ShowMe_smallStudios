using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
        if (Application.isMobilePlatform)
        {
            QualitySettings.vSyncCount = 0;
        }
    }
    #endregion

    [Header("Spawn Prefab")]
    public GameObject prefab;

    [Space]
    public Sheep player;

    [Space]
    [Header("Spawn Settings")]
    public Transform spawnPosition;

    [Space]
    [Header("Move Settings")]
    public float tileMoveSpeed;

    [Space]
    [Header("UI Settings")]
    [SerializeField] UnityEngine.UI.Text scoreText;
    [SerializeField] UnityEngine.UI.Text highscoreText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseScreen;

    [Space]
    [Header("|Debug Settings|")]
    [SerializeField] bool isDebug;
    [SerializeField] UnityEngine.UI.Text fpsCounter;

    [HideInInspector] public int currentscore;
    [HideInInspector] int highscore;

    GameObject mainCam;

    bool isTouching = false;
    bool canJump;


    // Start is called before the first frame update
    void Start()
    {

        highscore = PlayerPrefs.GetInt("Highscore", highscore);
        player = SpawnSheep();

        mainCam = Camera.main.gameObject;
        mainCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.gameObject.transform;
        mainCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = player.gameObject.transform;

        fpsCounter.gameObject.SetActive(false);

        if (isDebug)
        {
            fpsCounter.gameObject.SetActive(true);
        }

        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount > 0 && !isTouching || isDebug && Input.GetKeyDown(KeyCode.Space))
        {
            isTouching = true;

            if (player.isGrounded())
            {
                player.DoJump();
            }
        }

        if (Input.touchCount == 0 || isDebug && Input.GetKeyUp(KeyCode.Space))
        {
            isTouching = false;
        }

        scoreText.text = currentscore.ToString();
        highscoreText.text = highscore.ToString();

        if (isDebug)
        {
            fpsCounter.text = "FPS " + ((int)(1f / Time.unscaledDeltaTime)).ToString() + " || " + player.isGrounded();
        }
    }

    Sheep SpawnSheep()
    {
        GameObject sheep = Instantiate(prefab, spawnPosition.position, spawnPosition.rotation);
        return sheep.GetComponent<Sheep>();
    }

    public IEnumerator GameOver()
    {
        if (currentscore > highscore)
        {
            highscore = currentscore;
        }
        PlayerPrefs.SetInt("Highscore", highscore);

        tileMoveSpeed = 0;

        player.GetComponent<Animator>().enabled = false;

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
        player.GetComponent<Rigidbody>().AddForce(-Vector3.right * 5f, ForceMode.Impulse);
        player.GetComponent<Rigidbody>().AddTorque(Vector3.forward * 10f, ForceMode.Impulse);

        //Destroy(player.gameObject, 2);

        yield return new WaitForSecondsRealtime(2);

        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<UnityEngine.UI.Text>().text = "Your Score: " + currentscore + "\n Highscore: " + highscore;

    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
