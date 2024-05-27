using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float sprintSpeed = 13f; // Added sprint speed
    [SerializeField] private float jumpHeight = 3f;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator playerAnim;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Gravity
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);

        // Check if sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Calculate speed
        float currentSpeed = isSprinting ? sprintSpeed : speed;

        if (direction.magnitude >= 0.1f)
        {
            playerAnim.SetBool("AnimWalk", true);
            playerAnim.SetBool("RunAnim", false);
            if (isSprinting)
            {
                playerAnim.SetBool("AnimWalk", false);

                playerAnim.SetBool("RunAnim", true);
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
            Debug.Log("stoop walk");
        }
    }
}
