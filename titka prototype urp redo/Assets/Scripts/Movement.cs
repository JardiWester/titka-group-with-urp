using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float sprintSpeed = 13f;
    [SerializeField] private float jumpHeight = 3f;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator playerAnim;
    //public SFX SFX;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Ground check using Raycast
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hit, groundDistance, groundMask);

        if (isGrounded)
        {
            playerAnim.SetBool("jumpAnim", false);
            //playerAnim.SetBool("runJumpAnim", false);
        }

        // Walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Check if sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Calculate speed
        float currentSpeed = isSprinting ? sprintSpeed : speed;

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerAnim.SetBool("jumpAnim", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("jump now");
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            playerAnim.SetBool("AnimWalk", true);
            playerAnim.SetBool("RunAnim", false);
            //SFX.PlayWalkSound();

            if (isSprinting)
            {
                playerAnim.SetBool("AnimWalk", false);
                playerAnim.SetBool("RunAnim", true);
                //SFX.PlayRunSound();
            }

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
        else
        {
            playerAnim.SetBool("AnimWalk", false);
            playerAnim.SetBool("RunAnim", false);
            //SFX.StopSound();
        }
    }
}
