using UnityEngine;

public class SimplePickup : MonoBehaviour
{
    public enum PickupType
    {
        Life,
        Powerup,
    }

    public PickupType pickupType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();

            switch (pickupType)
            {
                case PickupType.Life:
                    GameManager.Instance.Lives++;
                    break;

                case PickupType.Powerup:
                    pc.JumpForceChange();
                    break;
            }

            Destroy(gameObject);
        }
    }
}
