using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;

    //event - a way to broadcast messages to any interested listeners - this is a way to implement the observer pattern in C#
    public delegate void PlayerInstanceDelegate(PlayerController player);
    public event PlayerInstanceDelegate OnPlayerSpawned;

    public Action<int> OnLifeValueChanged;

    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }
    #endregion

    #region Life Management
    private int _lives = 3;
    private int maxLives = 5;

    //C# way of doing getters and setters - property accesors
    public int Lives
    {
        get => _lives;
        set
        {
            if (value < 0)
            {
                GameOver();                
                return;
            }

            if (_lives > value)
            {
                Respawn();
            }

            _lives = value;
            if (value > maxLives)
            {
                _lives = maxLives;
            }

            OnLifeValueChanged?.Invoke(_lives);

            if (debugMode) Debug.Log("Life value changed to " + _lives);
        }
    }
    #endregion

    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;
    private Vector3 currentCheckpoint;

    // Update is called once per frame
    void Update()
    {
        //Debug toggle to show how to load scenes and toggle between them
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            string sceneToLoad = currentSceneName == "Title" ? "Game" : "Title";

            SceneManager.LoadScene(sceneToLoad);
        }

       if (Input.GetKeyDown(KeyCode.L))
       {
            Lives++;
       }

       if (Input.GetKeyDown(KeyCode.K))
       {
           Lives--;
       }
    }

    public void SpawnPlayer(Vector3 spawnPos)
    {
        _playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        UpdateCheckpoint(spawnPos);

        //if (OnPlayerSpawned != null)
        //{
        //    OnPlayerSpawned.Invoke(_playerInstance);
        //}

        OnPlayerSpawned?.Invoke(_playerInstance);
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint) => currentCheckpoint = newCheckpoint;

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }

    private void Respawn()
    {
        _playerInstance.transform.position = currentCheckpoint;
    }
}
