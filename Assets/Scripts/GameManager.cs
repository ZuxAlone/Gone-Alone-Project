using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    [SerializeField] private Player playerRef;
    [SerializeField] private Transform respawnPosition;
    [SerializeField] private Transform[] keySpawnPositions;
    [SerializeField] private GameObject[] keyPartPrefabs;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject deadParticles;
    [SerializeField] private GameObject pausePanelObject;
    [SerializeField] private GameObject introPanelObject;

    [SerializeField] private CinemachineVirtualCamera virtualCam;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private KeyDisplay keyDisplay;

    [SerializeField] private Animator blackCoverAnimator;

    [SerializeField] private int partsCollected;
    public bool isGamePaused;
    public bool canPlay;

    private void Start()
    {
        canPlay = false;
        InitializeAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPlay)
        {
            PauseGame();
        }
    }

    private void InitializeAll()
    {
        InitializeKeyDisplay();
        InitializeHealth(playerRef.GetHealth());
        UpdateHealth(playerRef.GetHealth());
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    public void PauseGame()
    {
        if (canPlay)
        {
            if (!isGamePaused) Time.timeScale = 0;
            else Time.timeScale = 1;
            isGamePaused = !isGamePaused;
            pausePanelObject.SetActive(!pausePanelObject.activeSelf);
        }
    }

    public void StartPlaying()
    {
        canPlay = true;
        Destroy(introPanelObject);
    }

    public void RespawnPlayer() 
    {
        Invoke("InstantiatePlayer", 1);
        StartCoroutine("SpawnParticles");
    }

    private void InstantiatePlayer() 
    {
        var player = Instantiate(playerPrefab, respawnPosition.position, Quaternion.identity);
        virtualCam.Follow = player.transform;
        playerRef = player.GetComponent<Player>();
        InitializeAll();
    }

    IEnumerator SpawnParticles()
    {
        var player = FindObjectOfType<Player>();
        var particles = Instantiate(deadParticles, player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(particles);
    }

    public void InitializeKeyDisplay()
    {
        partsCollected = 0;
        DestroyKeyPickUps();
        keyDisplay.InitializeKeyDisplay();
        InstantiateKeyPickUps();
    }

    private void DestroyKeyPickUps()  
    {
        foreach (var obj in FindObjectsOfType<KeyPickUp>())
        {
            Destroy(obj.gameObject);
        }
    }

    private void InstantiateKeyPickUps()
    {
        for (int i = 0; i < keyPartPrefabs.Length; i++)
        {
            Instantiate(keyPartPrefabs[i], keySpawnPositions[i]);
        }
    }

    public void UpdateKeyDisplay(int numPart)
    {
        partsCollected++;
        keyDisplay.UpdateKeyDisplay(numPart);
        UpdateWinCondition();
    }

    private void UpdateWinCondition() 
    {
        if (partsCollected != 3)
        {
            playerRef.SetWin(false);
        }
        else
        {
            playerRef.SetWin(true);
        }
    }

    public void InitializeHealth(int initHealth)
    {
        healthDisplay.InitializeHealth(initHealth);
    }

    public void UpdateHealth(int currentHealth) 
    {
        healthDisplay.UpdateHealth(currentHealth);
    }

    public void EndGame()
    {
        StartCoroutine("EndRoutine");
    }

    IEnumerator EndRoutine()
    {
        blackCoverAnimator.Play("BlackCoverAnim");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }
}
