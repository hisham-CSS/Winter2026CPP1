using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Ground Check Settings")]
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.02f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;


    private Rigidbody2D _rb;
    private Collider2D _collider;
    private SpriteRenderer _sr;
    private Animator _anim;
    private GroundCheck _groundCheck;
    
    private bool _isGrounded = false;
    private bool _isFiring = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _groundCheck = new GroundCheck(_collider, _rb, groundCheckRadius, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _groundCheck.IsGrounded();

        //input handling
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool fireInput = Input.GetButtonDown("Fire1");

        //movement
        if (!_isFiring)
        {
            Vector2 velocity = _rb.linearVelocity;
            velocity.x = horizontalInput * moveSpeed;
            _rb.linearVelocity = velocity;
        }

        if (horizontalInput != 0) SpriteFlip(horizontalInput);
        
        //jumping
        if (jumpInput && _isGrounded)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        //shooting
        if (fireInput && !_isFiring)
        {
            //_rb.linearVelocity = Vector2.zero;
            _isFiring = true;
        }

        //animation
        _anim.SetFloat("moveInput", Mathf.Abs(horizontalInput));
        _anim.SetFloat("yVel", _rb.linearVelocity.y);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetBool("Fire", _isFiring);
    }

    /// <summary>
    /// Sprite flipping based on horizontal input - this function should only be called when horizontal input is non-zero
    /// </summary>
    /// <param name="horizontalInput">The input received from Unity's input system</param>
    private void SpriteFlip(float horizontalInput) => _sr.flipX = (horizontalInput < 0);

    /// <summary>
    /// Animation event function to reset the firing animation state
    /// </summary>
    public void ResetFireAnimation()
    {
        _isFiring = false;
    }
}
