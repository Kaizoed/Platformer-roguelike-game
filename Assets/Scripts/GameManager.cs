using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject Player { get; private set; }
    [SerializeField] private Transform playerSpawnPoint;

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
}
