using UnityEngine;

public class Life : Pickup
{
    private Rigidbody2D rb;
    public int livesToAdd = 1;


    public override void OnPickup(GameObject player) => player.GetComponent<PlayerController>().lives += livesToAdd;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(-2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(-2f, rb.linearVelocity.y);
    }
}
