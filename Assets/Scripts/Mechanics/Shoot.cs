using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer _sr;
    [SerializeField] private Vector2 initalShotVelocity = new Vector2(3, 3);
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Projectile projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();

        if (initalShotVelocity == Vector2.zero)
        {
            initalShotVelocity = new Vector2(3, 3);
            Debug.LogWarning("Initial shot velocity was zero, setting to default (3,3)");
        }

        if (spawnPointLeft == null || spawnPointRight == null || projectilePrefab == null)
        {
            Debug.LogError("Spawn points or projectile for shooting are not assigned on: " + gameObject.name);
        }
    }

    public void Fire()
    {
        Projectile currentProjectile;
        if (!_sr.flipX)
        {
            currentProjectile = Instantiate(projectilePrefab, spawnPointRight.position, Quaternion.identity);
            currentProjectile.SetVelocity(initalShotVelocity);
        }
        else
        {
            currentProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, Quaternion.identity);
            currentProjectile.SetVelocity(initalShotVelocity);
        }
    }
}
