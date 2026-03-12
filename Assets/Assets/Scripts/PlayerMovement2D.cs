using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour
{
    public InputActionAsset inputActions;
    [Header("Movement")]
    [SerializeField]private InputAction moveAction;

    [SerializeField]private Vector2 moveInput;
    [SerializeField]private Rigidbody2D rigidbody2D;

    public float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private float jumpForce = 12f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;

    private void Awake()
    {
        var playerActionMap = inputActions.FindActionMap("Player");

        moveAction = playerActionMap.FindAction("Move");
        jumpAction = playerActionMap.FindAction("Jump");

        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (jumpAction.WasReleasedThisFrame() && rigidbody2D.linearVelocity.y > 0)
        {
            rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, rigidbody2D.linearVelocity.y * 0.5f);
        }
        
        if (Mathf.Abs(moveInput.x) > 0.6f && Mathf.Abs(moveInput.y) > 0.6f)
        {
            moveInput = new Vector2(Mathf.Sign(moveInput.x), 0f);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void FixedUpdate()
    {
        Walk();
        Jump();
    }

    private void Walk()
    {
        rigidbody2D.linearVelocity = new Vector2(moveInput.x * moveSpeed, rigidbody2D.linearVelocity.y);
    }

    private void Jump()
    {
        if (jumpAction.triggered && isGrounded)
        {
            rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, jumpForce);
        }
    }
}