using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public GameObject[] pickupPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randNum = Random.Range(0, pickupPrefabs.Length);

        if (pickupPrefabs[randNum] != null)
            Instantiate(pickupPrefabs[randNum], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
