using UnityEngine;

public class GroundCheck
{
    private bool isGrounded = false;

    private LayerMask groundLayer;
    private Collider2D col;
    private Rigidbody2D rb;
    private float groundCheckRadius;

    private Vector2 groundCheckPosition => new Vector2(col.bounds.center.x, col.bounds.min.y);

    public GroundCheck(Collider2D col, Rigidbody2D rb, float radius, LayerMask groundLayer)
    {
        this.col = col;
        this.rb = rb;
        this.groundCheckRadius = radius;
        this.groundLayer = groundLayer;
    }

    public bool IsGrounded()
    {
        if (!isGrounded && rb.linearVelocityY <= 0 || isGrounded)
            isGrounded = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, groundLayer);

        return isGrounded;
    }
}