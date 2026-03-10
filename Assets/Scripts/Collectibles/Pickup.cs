using UnityEngine;

//abstract classes are classes that cannot be instantiated directly. They are meant to be inherited by other classes. We can leverage abstract classes to create a common base class for all pickups in our game. We can use polymorphism to treat all pickups as the same type, while still allowing for specific behavior in each derived class.
[RequireComponent(typeof(AudioSource))]
public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    protected AudioSource audioSource;


    // Abstract method to be implemented by derived classes
    abstract public void OnPickup(GameObject player);

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
            audioSource.PlayOneShot(pickupSound);
            GetComponent<Renderer>().enabled = false; // Hide the pickup visually
            GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent multiple pickups
            Destroy(gameObject, pickupSound.length);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnPickup(collision.collider.gameObject);
            audioSource.PlayOneShot(pickupSound);
            GetComponent<Renderer>().enabled = false; // Hide the pickup visually
            GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent multiple pickups
            Destroy(gameObject, pickupSound.length);
        }
    }
}
