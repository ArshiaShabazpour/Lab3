using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckDistance = 0.12f;

    public Transform firePoint;
    public int bulletDamage = 1;

    public float stompForce = 20f;
    public float stompCooldown = 0.5f;
    private float lastStompTime = -10f;

    public int maxHP = 3;

    private Rigidbody2D rb;
    private float moveInput;
    private bool wantsJump;
    private bool isGrounded;

    public int CurrentHP { get; private set; }
    public PlayerState State { get; private set; } = PlayerState.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentHP = maxHP;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wantsJump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.S))
        {
            TryStomp();

        }
        UpdateState();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        Vector2 origin = groundCheck != null ? (Vector2)groundCheck.position : (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (wantsJump && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            wantsJump = false;
            GameManager.Instance?.PlayerJumped();
        }
        else wantsJump = false;
    }

    void UpdateState()
    {
        if (State == PlayerState.Dead) return;

        if (!isGrounded)
            State = PlayerState.Jump;
        else if (Mathf.Abs(moveInput) > 0.1f)
            State = PlayerState.Move;
        else
            State = PlayerState.Idle;
    }

    public void TakeDamage(int dmg)
    {
        if (GameManager.Instance == null || GameManager.Instance.IsGameOver) return;

        GameManager.Instance.DamagePlayer(dmg);

        AudioManager.Instance?.PlayPlayerHit();
    }



    void Shoot()
    {
        if (firePoint == null) return;
        Vector2 dir = transform.localScale.x >= 0 ? Vector2.right : Vector2.left;
        SpawnManager.Instance?.SpawnBullet(firePoint.position, dir, bulletDamage);
        GameManager.Instance?.PlayerShot();

    }

    public void TryStomp()
    {
        if (Time.time - lastStompTime < stompCooldown) return;
        lastStompTime = Time.time;
        Stomp();
    }

    public void Stomp()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -stompForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(2); 
        }
    }
}
