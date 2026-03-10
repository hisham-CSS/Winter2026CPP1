using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [Header("Debug Mode")]
    public bool debugMode = false;

    [Header("Ground Check Settings")]
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.02f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Powerup Settings")]
    public float initialPowerupDuration = 5f;
    public float powerupJumpForce = 20f;

    [Header("Audio Settings")]
    public AudioClip jumpSound;
    public AudioClip stompSound;

    private float currentPowerupDuration = 0f;
    private float initalJumpForce = 7f;
    private Coroutine jumpforceCoroutine = null;




    private Rigidbody2D _rb;
    private Collider2D _collider;
    private SpriteRenderer _sr;
    private Animator _anim;
    private GroundCheck _groundCheck;
    private AudioSource _audioSource;

    private bool _isGrounded = false;
    private bool _isFiring = false;
    private bool _airAttack = false;
    public float PowerupDuration() => currentPowerupDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _groundCheck = new GroundCheck(_collider, _rb, groundCheckRadius, groundLayer);

        initalJumpForce = jumpForce;
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
            _audioSource.PlayOneShot(jumpSound);
        }
        //shooting
        if (fireInput && !_isFiring)
        {
            //_rb.linearVelocity = Vector2.zero;
            _isFiring = true;
        }

        if (!_isGrounded && fireInput && verticalInput == 1)
        {
            _airAttack = true;
            _isFiring = false;
        }

        if (_isGrounded)
        {
            _airAttack = false;
        }

        //animation
        _anim.SetFloat("moveInput", Mathf.Abs(horizontalInput));
        _anim.SetFloat("yVel", _rb.linearVelocity.y);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetBool("Fire", _isFiring);
        _anim.SetBool("AirAttack", _airAttack);

        if (debugMode) Debug.Log($"Velocity is: {_rb.linearVelocity}");
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

    public void IncreaseGravity()
    {
        _rb.gravityScale = 5f;
    }

    public void JumpForceChange()
    {
        if (jumpforceCoroutine != null)
        {
            StopCoroutine(jumpforceCoroutine);
            jumpforceCoroutine = null;
            jumpForce = initalJumpForce;
        }

        jumpforceCoroutine = StartCoroutine(JumpForceChangeCoroutine());
    }

    IEnumerator JumpForceChangeCoroutine()
    {
        currentPowerupDuration = initialPowerupDuration + currentPowerupDuration;
        jumpForce = powerupJumpForce;

        while (currentPowerupDuration > 0)
        {
            currentPowerupDuration -= Time.deltaTime;
            if (currentPowerupDuration < 0) currentPowerupDuration = 0;
            if (debugMode) Debug.Log("Jump Powerup Time Remaining: " + currentPowerupDuration);
            yield return null;
        }

        jumpForce = initalJumpForce;
        jumpforceCoroutine = null;
        currentPowerupDuration = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish") && _rb.linearVelocityY < 0)
        {
            collision.GetComponentInParent<BaseEnemy>().TakeDamage(0, DamageType.JumpedOn);
            _rb.linearVelocityY = 0;
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            _audioSource.PlayOneShot(stompSound);
        }
    }
}
