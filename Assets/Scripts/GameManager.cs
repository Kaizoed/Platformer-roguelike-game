using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject Player { get; private set; }
    [SerializeField] private Transform playerSpawnPoint;

    // Reference to the Upgrade Panel
    [SerializeField] private GameObject upgradePanel;  // Add this line

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

    // Method to hide the upgrade panel
    public void HideUpgradePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);  // This will hide the upgrade panel
        }
    }

    // Optional: Show upgrade panel if needed in future
    public void ShowUpgradePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);  // This will show the upgrade panel
        }
    }
}
