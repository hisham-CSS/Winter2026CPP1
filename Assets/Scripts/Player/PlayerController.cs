using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //public GameObject groundCheckTransform;
    public LayerMask groundLayer;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float groundCheckRadius = 0.02f;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private SpriteRenderer _sr;
    private Animator _anim;
    
    
    private bool _isGrounded = false;
    private bool _JumpAttack = false;
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
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        ////initalize the ground check object here rather than in the inpsector for safety - only if we use a gameobject to get our foot position
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
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool attack = Input.GetButtonDown("Fire1");

        //movement
        Vector2 velocity = _rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        _rb.linearVelocity = velocity;

        if (horizontalInput != 0) SpriteFlip(horizontalInput);
        
        //jumping
        if (jumpInput && _isGrounded)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (!_isGrounded && attack)
        {
            _JumpAttack = true;

            Debug.Log("Jump Attack!");
        }

        if (_isGrounded) _JumpAttack = false;

        //animation
        _anim.SetFloat("moveInput", Mathf.Abs(horizontalInput));
        _anim.SetFloat("yVel", _rb.linearVelocity.y);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetBool("JumpAttack", _JumpAttack);
    }

    /// <summary>
    /// Sprite flipping based on horizontal input - this function should only be called when horizontal input is non-zero
    /// </summary>
    /// <param name="horizontalInput">The input received from Unity's input system</param>
    private void SpriteFlip(float horizontalInput) => _sr.flipX = (horizontalInput < 0);
}
