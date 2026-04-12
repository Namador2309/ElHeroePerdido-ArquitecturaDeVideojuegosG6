using UnityEngine;

namespace SupanthaPaul
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;

        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float fallMultiplier;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private int extraJumpCount = 1;

        [Header("Dashing")]
        [SerializeField] private float dashSpeed = 30f;
        [SerializeField] private float startDashTime = 0.1f;
        [SerializeField] private float dashCooldown = 0.2f;

        [HideInInspector] public bool isGrounded;
        [HideInInspector] public float moveInput;
        [HideInInspector] public bool canMove = true;
        [HideInInspector] public bool isDashing = false;
        [HideInInspector] public bool actuallyWallGrabbing = false;
        [HideInInspector] public bool isCurrentlyPlayable = false;

        [Header("Wall grab & jump")]
        public Vector2 grabRightOffset = new Vector2(0.16f, 0f);
        public Vector2 grabLeftOffset = new Vector2(-0.16f, 0f);
        public float grabCheckRadius = 0.24f;
        public float slideSpeed = 2.5f;
        public Vector2 wallJumpForce = new Vector2(10.5f, 18f);
        public Vector2 wallClimbForce = new Vector2(4f, 14f);

        private Rigidbody2D m_rb;
        private ParticleSystem m_dustParticle;
        private bool m_facingRight = true;
        private float m_groundedRemember;
        private int m_extraJumps;
        private float m_extraJumpForce;
        private float m_dashTime;
        private bool m_hasDashedInAir = false;
        private bool m_onWall = false;
        private bool m_onRightWall = false;
        private bool m_onLeftWall = false;
        private bool m_wallGrabbing = false;
        private float m_wallStick = 0f;
        private bool m_wallJumping = false;
        private float m_dashCooldown;

        private int m_onWallSide = 0;
        private int m_playerSide = 1;

        void Start()
        {
            if (transform.CompareTag("Player"))
                isCurrentlyPlayable = true;

            m_extraJumps = extraJumpCount;
            m_dashTime = startDashTime;
            m_dashCooldown = dashCooldown;
            m_extraJumpForce = jumpForce * 0.7f;

            m_rb = GetComponent<Rigidbody2D>();
            m_dustParticle = GetComponentInChildren<ParticleSystem>();
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

            Vector2 position = transform.position;

            m_onWall = Physics2D.OverlapCircle(position + grabRightOffset, grabCheckRadius, whatIsGround)
                    || Physics2D.OverlapCircle(position + grabLeftOffset, grabCheckRadius, whatIsGround);

            m_onRightWall = Physics2D.OverlapCircle(position + grabRightOffset, grabCheckRadius, whatIsGround);
            m_onLeftWall = Physics2D.OverlapCircle(position + grabLeftOffset, grabCheckRadius, whatIsGround);

            CalculateSides();

            if ((m_wallGrabbing || isGrounded) && m_wallJumping)
                m_wallJumping = false;

            if (!isCurrentlyPlayable) return;

            if (m_wallJumping)
                m_rb.velocity = Vector2.Lerp(m_rb.velocity, new Vector2(moveInput * speed, m_rb.velocity.y), Time.fixedDeltaTime);
            else
                m_rb.velocity = new Vector2(moveInput * speed, m_rb.velocity.y);

            if (m_rb.velocity.y < 0f)
                m_rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;

            if (!m_facingRight && moveInput > 0f) Flip();
            else if (m_facingRight && moveInput < 0f) Flip();

            if (isDashing)
            {
                if (m_dashTime <= 0f)
                {
                    isDashing = false;
                    m_dashCooldown = dashCooldown;
                    m_dashTime = startDashTime;
                    m_rb.velocity = Vector2.zero;
                }
                else
                {
                    m_dashTime -= Time.deltaTime;
                    m_rb.velocity = m_facingRight ? Vector2.right * dashSpeed : Vector2.left * dashSpeed;
                }
            }

            float vel = m_rb.velocity.sqrMagnitude;

            if (m_dustParticle != null)
            {
                if (m_dustParticle.isPlaying && vel == 0f)
                    m_dustParticle.Stop();
                else if (!m_dustParticle.isPlaying && vel > 0f)
                    m_dustParticle.Play();
            }
        }

        private void Update()
        {
            moveInput = InputSystem.HorizontalRaw();

            if (isGrounded)
                m_extraJumps = extraJumpCount;

            if (!isCurrentlyPlayable) return;

            if (!isDashing && !m_hasDashedInAir && m_dashCooldown <= 0f)
            {
                if (InputSystem.Dash())
                {
                    isDashing = true;

                    if (!isGrounded)
                        m_hasDashedInAir = true;
                }
            }

            m_dashCooldown -= Time.deltaTime;

            if (m_hasDashedInAir && isGrounded)
                m_hasDashedInAir = false;

            if (InputSystem.Jump() && m_extraJumps > 0 && !isGrounded)
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, m_extraJumpForce);
                m_extraJumps--;
            }
            else if (InputSystem.Jump() && isGrounded)
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, jumpForce);
            }
        }

        void Flip()
        {
            m_facingRight = !m_facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        void CalculateSides()
        {
            if (m_onRightWall) m_onWallSide = 1;
            else if (m_onLeftWall) m_onWallSide = -1;
            else m_onWallSide = 0;

            m_playerSide = m_facingRight ? 1 : -1;
        }
    }
}