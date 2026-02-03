using UnityEngine;

//abstract classes are classes that cannot be instantiated directly. They are meant to be inherited by other classes. We can leverage abstract classes to create a common base class for all pickups in our game. We can use polymorphism to treat all pickups as the same type, while still allowing for specific behavior in each derived class.
public abstract class Pickup : MonoBehaviour
{
    // Abstract method to be implemented by derived classes
    abstract public void OnPickup(GameObject player);

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnPickup(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }
}
