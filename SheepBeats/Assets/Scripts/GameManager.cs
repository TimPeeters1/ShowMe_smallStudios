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

        //Application.targetFrameRate = 60;
        if (Application.isMobilePlatform)
        {
            QualitySettings.vSyncCount = 0;
        }
    }
    #endregion

    [Header("Spawn Prefab")]
    public GameObject prefab;

    [Space]
    public List<Sheep> activeSheeps;

    [Space]
    [Header("Spawn Settings")]
    public Transform spawnPosition;
    public float spawnInterval;

    [Space]
    [Header("Move Settings")]
    public float walkInterval;

    [Space]
    [Header("UI Settings")]
    [SerializeField] UnityEngine.UI.Text scoreText;

    [Space]
    [Header("|Debug Settings|")]
    [SerializeField] bool isDebug;
    [SerializeField] UnityEngine.UI.Text fpsCounter;

    [HideInInspector] public float currentScore;

    bool isTouching = false;
    bool canJump()
    {
        for (int i = 0; i < activeSheeps.Count; i++)
        {
            if (activeSheeps[i].isGrounded() && !activeSheeps[i].isJumping) { return true; } else { return false; }
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSheep", 0f, spawnInterval);

        InvokeRepeating("UpdateRow", 0f, walkInterval);

        fpsCounter.gameObject.SetActive(false);

        if (isDebug)
        {
            fpsCounter.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && !isTouching )
        {
            isTouching = true;

            if (canJump())
            {
                JumpRow();
            }
        }

        if(Input.touchCount == 0)
        {
            isTouching = false;
        }

        scoreText.text = currentScore.ToString();

        if (isDebug)
        {
            fpsCounter.text = "FPS " + ((int)(1f / Time.unscaledDeltaTime)).ToString() + " || " + canJump().ToString();
        }

    }

    void SpawnSheep()
    {
        GameObject sheep = Instantiate(prefab, spawnPosition.position, spawnPosition.rotation);
        activeSheeps.Add(sheep.GetComponent<Sheep>());
    }

    void JumpRow()
    {
        for (int i = 0; i < activeSheeps.Count; i++)
        {
            activeSheeps[i].isJumping = true;
            activeSheeps[i].DoJump();
        }
    }

    void UpdateRow()
    {   
        for (int i = 0; i < activeSheeps.Count; i++)
        {
            activeSheeps[i].StartCoroutine("DoMove");
        }
    }
}
