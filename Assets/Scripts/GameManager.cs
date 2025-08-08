using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameObject player;
    public GameObject Player
    {
        get
        {
            if (player == null)
            {
                Debug.LogError("Can't find player, but player is being accessed");
            }
            return player;
        }
        set => player = value;
    }
    private int pauseCount = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerSpawn += SetPlayer;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerSpawn -= SetPlayer;
    }

    void SetPlayer(GameObject player)
    {
        Player = player;
    }

    /// <summary> Pause or Unpause the game </summary>
    public void PauseGame(bool pause)
    {
        if (pause)
            pauseCount++;
        else 
            pauseCount--;
        pauseCount = Mathf.Max(pauseCount, 0); // value won't go below 0;

        if (pauseCount == 0)
        {
            Time.timeScale = 1f; // resume
            Debug.Log($"Game Unpaused: {pause}");
        }
        else
        {
            Time.timeScale = 0f; // Set the time scale to 0 to pause, or 1 to unpause
            Debug.Log($"Game Paused or still paused: {pause}");
        }
    }
}
