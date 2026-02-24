using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => GameManager.Instance.SpawnPlayer(spawnPoint.position);
}
