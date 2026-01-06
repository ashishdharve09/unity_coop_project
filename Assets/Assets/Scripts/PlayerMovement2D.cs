using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveInput;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private void Awake()
    {
        var playerActionMap = inputActions.FindActionMap("Player");
        moveAction = playerActionMap.FindAction("Move");
        jumpAction = playerActionMap.FindAction("Jump");

        rb = GetComponent<Rigidbody2D>();
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
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        // Vector2 move = Vector2.right * moveInput.x * moveSpeed * Time.fixedDeltaTime;
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed,rb.linearVelocity.y);
    }    
}
