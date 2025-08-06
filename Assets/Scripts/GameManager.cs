using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    [SerializeField] private Transform playerSpawnPoint;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(playerSpawnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
