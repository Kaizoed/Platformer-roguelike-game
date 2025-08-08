using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player player;
    public Player Player
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
        Player.OnPlayerSpawn += SetPlayer;
    }

    private void OnDisable()
    {
        Player.OnPlayerSpawn -= SetPlayer;
    }

    void SetPlayer(Player player)
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
