using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType type = ProjectileType.PlayerProjectile;
    [SerializeField, Range(0.5f, 10f)] private float lifetime = 10f;
    [SerializeField] private int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(Vector2 Velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = Velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == ProjectileType.PlayerProjectile)
        {
            BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        if (type == ProjectileType.EnemyProjectile)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.Lives--;
                Destroy(gameObject);
            }
        }
    }
}

public enum ProjectileType
{
    PlayerProjectile,
    EnemyProjectile
}
