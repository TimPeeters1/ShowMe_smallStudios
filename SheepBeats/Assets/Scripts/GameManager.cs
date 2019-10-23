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

    [Space]
    [Header("|Debug Settings|")]
    [SerializeField] bool isDebug;
    [SerializeField] UnityEngine.UI.Text fpsCounter;

    [HideInInspector] public float currentScore;

    GameObject mainCam;

    bool isTouching = false;
    bool canJump;


    // Start is called before the first frame update
    void Start()
    {

        player = SpawnSheep();

        mainCam = Camera.main.gameObject;
        mainCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.gameObject.transform;
        mainCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = player.gameObject.transform;

        fpsCounter.gameObject.SetActive(false);

        if (isDebug)
        {
            fpsCounter.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.touchCount > 0 && !isTouching)
        {
            isTouching = true;

            if (player.isGrounded())
            {
                player.DoJump();
            }
        }

        if (Input.touchCount == 0)
        {
            isTouching = false;
        }

        scoreText.text = currentScore.ToString();

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

}
