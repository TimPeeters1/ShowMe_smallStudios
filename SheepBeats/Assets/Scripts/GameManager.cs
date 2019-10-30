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
    public bool canJump;

    [Space]
    //Dragscript variables
    private Vector3 mOffset;
    private Vector3 hitPoint;
    private Vector3 releasePoint;
    private float mZCoord;
    private GameObject currentObject;
    public float CustomMass = 1f;
    public float ForceToAdd = 1f;
    Touch touchOne;

    Ray GenerateTouchray()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPosFar = new Vector3(touch.position.x, touch.position.y, Camera.main.farClipPlane);
        Vector3 touchPosNear = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane);
        Vector3 touchPosF = Camera.main.ScreenToWorldPoint(touchPosFar);
        Vector3 touchPosN = Camera.main.ScreenToWorldPoint(touchPosNear);

        Ray mr = new Ray(touchPosN, touchPosF - touchPosN);
        return mr;
    }
    private Vector3 GetTouchAsWorldPoint()
    {
        Touch touch = Input.GetTouch(0);
        // Pixel coordinates of mouse (x,y)
        Vector3 touchPoint = touch.position;
        // z coordinate of game object on screen
        touchPoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(touchPoint);
    }

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
        canJump = true;
    }

    void Update()
    {
        if (Input.touchCount > 0 && !isTouching || isDebug && Input.GetKeyDown(KeyCode.Space))
        {
            isTouching = true;

            Ray touchRay = GenerateTouchray();
            RaycastHit hit;
            //Debug.DrawRay(touchRay.origin, touchRay.direction * 100f, Color.red);

            if (Physics.Raycast(touchRay.origin, touchRay.direction, out hit))
            {
                if (hit.transform.GetComponent<IsDraggable>())
                {
                    currentObject = hit.transform.gameObject;
                    hitPoint = hit.point;

                    hit.collider.enabled = false;

                    mZCoord = Camera.main.WorldToScreenPoint(currentObject.transform.position).z;
                    mOffset = currentObject.transform.position - GetTouchAsWorldPoint();
                }
                else
                {
                    if (player.isGrounded())
                    {
                        player.DoJump();
                    }
                }
            }
        }

        if (Input.touchCount > 0 && currentObject)
        {
            if (currentObject.GetComponent<Rigidbody>() == null)
            {
                currentObject.AddComponent<Rigidbody>();
            }

            currentObject.GetComponent<Rigidbody>().mass = CustomMass;
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
            currentObject.transform.position = GetTouchAsWorldPoint() + mOffset;

        }

        if (Input.touchCount == 0 || isDebug && Input.GetKeyUp(KeyCode.Space))
        {
            isTouching = false;

            if (currentObject)
            {
                currentObject.GetComponent<IEvent>().DisableEvent();

                releasePoint = currentObject.transform.position;
                currentObject.GetComponent<Rigidbody>().AddForce((releasePoint - hitPoint) * ForceToAdd);
                currentObject = null;

            }

           
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
