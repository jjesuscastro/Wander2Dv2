using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(CircleCollider2D))]
    public class CharacterController2D : MonoBehaviour
    {
        public float maxClimbSlope = 45;
        RaycastOrigins raycastOrigins;
        public int horizontalRayCount = 4;
        public int verticalRayCount = 4;
        float horizontalRaySpacing;
        float verticalRaySpacing;
        float timer = 0;
        float delayCheck = 50;

        [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
        [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
        [SerializeField] private LayerMask m_PassThrough;                          // A mask determining what the character can pass through
        [SerializeField] private BoxCollider2D m_SlopeCheck;                        // A collider that will check the slope to climb
        [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
        [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
        [SerializeField] private Transform m_ExcNotif;                              // Exclamation popup
        [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
        [SerializeField] private Collider2D m_GroundCollider;                       // A collider that collides with the ground

        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private Vector3 m_Velocity = Vector3.zero;
        private bool isSimulated = true;

        [Header("Events")]
        [Space]

        public UnityEvent OnLandEvent;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        public BoolEvent OnCrouchEvent;
        private bool m_wasCrouching = false;

        private void Start()
        {
            CalculateRaySpacing();
        }

        void Update()
        {
            if (!isSimulated)
                DisableColliders();

            timer += Time.deltaTime * 100;
        }

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();

            if (OnCrouchEvent == null)
                OnCrouchEvent = new BoolEvent();
        }

        private void FixedUpdate()
        {
            bool wasGrounded = m_Grounded;
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;

                    if (timer >= delayCheck)
                    {
                        if (m_Grounded && wasGrounded) // && m_Rigidbody2D.velocity.y <= 0
                            OnLandEvent.Invoke();

                        timer = 0;
                    }
                }
            }
        }

        void HorizontallCollisions(ref Vector3 velocity)
        {
            float directionX = Mathf.Sign(velocity.x);
            float rayLength = Mathf.Abs(velocity.x);

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, 0.5f, m_WhatIsGround);

                Debug.DrawRay(rayOrigin, Vector2.right * directionX * 0.5f, Color.red);

                if (hit)
                {
                    //We only want to use this for the slopeChecking not the movement so use this only for slope checking
                    float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                    // Debug.Log(hit.collider.name + " " + slopeAngle);
                    if (i == 0 && slopeAngle <= maxClimbSlope && hit.collider.gameObject.layer != LayerMask.NameToLayer("PlatformTemp"))
                    {
                        ClimbSlope(ref velocity, slopeAngle);
                    }
                    rayLength = hit.distance;
                }
            }
        }

        void VerticalCollisions(ref Vector3 velocity, float moveV)
        {
            float directionY = Mathf.Sign(velocity.y);
            float rayLength = Mathf.Abs(velocity.y);

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += -Vector2.up * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, 1f, m_WhatIsGround);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY * 1f, Color.red);

                if (hit)
                {
                    //We only want to use this for the slopeChecking not the movement so use this only for slope checking
                    if (moveV == -1 && hit.collider.gameObject.layer == LayerMask.NameToLayer("PlatformAbove"))
                    {
                        m_GroundCollider.enabled = false;
                        m_CrouchDisableCollider.enabled = false;
                        Invoke("ReenableColliders", .3f);
                    }

                    m_Grounded = true;

                    if (OnLandEvent != null && timer >= delayCheck)
                    {
                        OnLandEvent.Invoke();
                        timer = 0;
                    }
                }
            }
        }

        void ReenableColliders()
        {
            m_GroundCollider.enabled = true;
            m_CrouchDisableCollider.enabled = true;
        }

        void ClimbSlope(ref Vector3 velocity, float slopeAngle)
        {
            float moveDistance = Mathf.Abs(velocity.x);
            float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

            if (velocity.y <= climbVelocityY)
            {
                velocity.y = climbVelocityY;
                velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                m_Grounded = true;
            }
        }

        void UpdateRaycastOrigins()
        {
            Bounds bounds = m_SlopeCheck.bounds;

            raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
            raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        }

        void CalculateRaySpacing()
        {
            Bounds bounds = m_SlopeCheck.bounds;

            horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
            verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

            horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
            verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        }

        public void Move(float move, float moveV, bool crouch, bool jump)
        {
            UpdateRaycastOrigins();
            // If crouching, check to see if the character can stand up
            if (!crouch)
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {

                // If crouching
                if (crouch)
                {
                    if (!m_wasCrouching)
                    {
                        m_wasCrouching = true;
                        OnCrouchEvent.Invoke(true);
                    }

                    // Reduce the speed by the crouchSpeed multiplier
                    move *= m_CrouchSpeed;

                    // Disable one of the colliders when crouching
                    // if (m_CrouchDisableCollider != null)
                    //     m_CrouchDisableCollider.enabled = false;
                }
                else
                {
                    // Enable the collider when not crouching
                    // if (m_CrouchDisableCollider != null)
                    //     m_CrouchDisableCollider.enabled = true;

                    if (m_wasCrouching)
                    {
                        m_wasCrouching = false;
                        OnCrouchEvent.Invoke(false);
                    }
                }

                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                if (targetVelocity.x != 0)
                    HorizontallCollisions(ref targetVelocity);

                //Changed vertical collisions to work even without pressing down
                VerticalCollisions(ref targetVelocity, moveV);

                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }

            }
            // If the player should jump...
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        public bool IsGrounded()
        {
            return m_Grounded;
        }

        public void QueueDisableColliders()
        {
            isSimulated = false;
        }

        public void DisableColliders()
        {
            if (m_Grounded)
                m_Rigidbody2D.simulated = false;
        }

        public void EnableColliders()
        {
            isSimulated = true;
            m_Rigidbody2D.simulated = true;
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

            if (m_ExcNotif != null)
            {
                Vector3 thePosition = m_ExcNotif.transform.localPosition;
                theScale = m_ExcNotif.transform.localScale;
                thePosition.x *= -1;
                theScale.x *= -1;
                m_ExcNotif.transform.localPosition = thePosition;
                m_ExcNotif.transform.localScale = theScale;
            }
        }

        struct RaycastOrigins
        {
            public Vector2 topLeft, topRight;
            public Vector2 bottomLeft, bottomRight;
        }
    }
}