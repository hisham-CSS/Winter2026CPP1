using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    //public GameObject groundCheckTransform;
    public LayerMask groundLayer;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float groundCheckRadius = 0.02f;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private bool _isGrounded = false;
    private Vector2 groundCheckPos => CalculateGroundCheck();


    private Vector2 CalculateGroundCheck()
    {
        Bounds bounds = _collider.bounds;
        return new Vector2(bounds.center.x, bounds.min.y);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        ////initalize the ground check object here rather than in the inpsector for safety
        //if (groundCheckTransform == null)
        //{
        //    groundCheckTransform = new GameObject("GroundCheck");
        //    groundCheckTransform.transform.SetParent(transform);
        //    groundCheckTransform.transform.localPosition = Vector3.zero;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);

        //input handling
        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");

        //movement
        Vector2 velocity = _rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        _rb.linearVelocity = velocity;

        //jumping
        if (jumpInput && _isGrounded)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        
    }


}
