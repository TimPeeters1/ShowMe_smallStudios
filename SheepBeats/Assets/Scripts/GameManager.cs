using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DragScript))]
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
    public float moveMultiplier;

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
    DragScript dragScript;

    bool isTouching = false;
    bool canJump;

    [Space]
    [SerializeField] bool jumpPending;

    [SerializeField] float timerValue;

    // Start is called before the first frame update
    void Start()
    {

        highscore = PlayerPrefs.GetInt("Highscore", highscore);
        player = SpawnSheep();

        dragScript = GetComponent<DragScript>();

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

    IEnumerator Timer()
    {
        while (Input.touchCount > 0)
        {
            timerValue++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && !isTouching|| isDebug && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Timer());
            isTouching = true;
        }

        if(Input.touchCount > 0 && isTouching)
        {
            if (timerValue <= 8)
            {
                jumpPending = true;
            }
            else
            {
                jumpPending = false;
                dragScript.enabled = true;
            }
        }

        if (Input.touchCount == 0 || isDebug && Input.GetKeyUp(KeyCode.Space))
        {
            if (jumpPending && player.isGrounded())
            {
                player.DoJump();
                jumpPending = false;
            }

            dragScript.enabled = false;

            isTouching = false;
            timerValue = 0;
            //StopCoroutine(Timer());
        }

        scoreText.text = currentscore.ToString();
        highscoreText.text = highscore.ToString();

        if (isDebug)
        {
            fpsCounter.text = "FPS " + ((int)(1f / Time.unscaledDeltaTime)).ToString() + " || " + player.isGrounded() + " || " + tileMoveSpeed.ToString();
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

        player.GetComponentInChildren<Animator>().enabled = false;

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
        player.GetComponent<Rigidbody>().AddForce(-Vector3.right * 5f, ForceMode.Impulse);
        player.GetComponent<Rigidbody>().AddTorque(Vector3.forward * 10f, ForceMode.Impulse);
        player.GetComponent<Rigidbody>().AddTorque(Vector3.right * 3f, ForceMode.Impulse);

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
