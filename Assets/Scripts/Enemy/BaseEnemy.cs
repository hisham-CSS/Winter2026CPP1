using UnityEngine;

//abstract class can be derrived from but not instantiated at runtime
[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public abstract class BaseEnemy : MonoBehaviour
{
    //public: accessible from anywhere in the code
    //private: accessible only within the class it is declared in
    //protected: accessible within the class it is declared in and any class that inherits from it

    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;

    public int maxHealth = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //virtual functions can be overridden by derived classes to provide specific behavior while still maintaining a common interface. This allows us to create a base class with default behavior that can be customized by subclasses without changing the base class code.
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (maxHealth <= 0)
        {
            maxHealth = 5;
            Debug.LogWarning("Max health must be greater than 0. Setting max health to default value of 5.");
        }

        health = maxHealth;
    }

    public virtual void TakeDamage(int damage, DamageType damageType = DamageType.Default)
    {
        health -= damage;

        if (health <= 0)
        {
            anim.SetTrigger("Death");

            if (transform.parent != null)
                Destroy(transform.parent.gameObject, 0.5f);
            else
                Destroy(gameObject, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Lives--;
        }
    }
}

public enum DamageType
{
    Default,
    JumpedOn
}
