using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Speed")] //sets the speeds for run, walk, and crouch
    [SerializeField] private float walkspeed = 20f;




    [Header("Jump and Fall")]//sets the jump force, gravity, and initial fall velocity
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = -12f;
    [SerializeField] private float initialFallVelocity = -2f;//prevents character from floating in the air when falling from a height

    [Header("Dash")]//sets the dash time and cooldown
    [SerializeField] private float dashSpeed = 40f;
    [SerializeField] private float DashDecaySpeed = -10f;


    [Header("References")] //references to the player and the character controller
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference dashAction;


    //check if characters are performing an action
    private CharacterController _characterController;
    private Vector2 _moveInput;
    private bool _isGrounded;
    private bool _isDashing;

    private float _verticalVelocity;
    private float _horizontalVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

    }

    private void OnEnable() //make input change to zero on button release
    {
        moveAction.action.performed += StoreMovementInput;
        moveAction.action.canceled += StoreMovementInput;
        jumpAction.action.performed += Jump;
        dashAction.action.performed += Dash;

    }

    private void OnDisable()
    {
        moveAction.action.performed -= StoreMovementInput;
        moveAction.action.canceled -= StoreMovementInput;
        jumpAction.action.performed -= Jump;
        dashAction.action.performed -= Dash;

    }



    private void StoreMovementInput(InputAction.CallbackContext context) //store the movement input from the player
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext context) //makes the player jump
    {
        if (_isGrounded)
        {
            _verticalVelocity = jumpForce;
        }
    }

 

private void Dash(InputAction.CallbackContext context) //makes the player dash
    {
        if (!_isDashing)
        {
            walkspeed = dashSpeed;
            _isDashing = true;
            print("dashing");
        }
    }

    public void Start()
    {
        _horizontalVelocity = walkspeed;
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;//checks if the player is on the ground
        HandleGravity();
        HandleDash();
        HandleMovement();

    }

    private void HandleGravity()
    {
        if (_isGrounded && _verticalVelocity <= 0)
        {
            _verticalVelocity = initialFallVelocity;
        }

        _verticalVelocity += gravity * Time.deltaTime;
    }

    private void HandleDash()
    {
        if (_isDashing && walkspeed <= _horizontalVelocity)
        {
            walkspeed = _horizontalVelocity;
            _isDashing = false;

        }
        else if( walkspeed <= _horizontalVelocity)
        {
            walkspeed = _horizontalVelocity;
        }

        walkspeed += DashDecaySpeed * Time.deltaTime;
    }

    private void HandleMovement() //changes movement accoring to the camera direction and the input from the player
    {
        Vector3 move = cameraTransform.TransformDirection(new Vector3(_moveInput.x, 0f, _moveInput.y)).normalized;
        float currentSpeed = walkspeed;
       
        Vector3 finalMove = move * currentSpeed;
        finalMove.y = _verticalVelocity;

        CollisionFlags collisions = _characterController.Move(motion: finalMove * Time.deltaTime);

        if((collisions &CollisionFlags.Above) !=0) //prevents floating in air before falling
        {
            _verticalVelocity = initialFallVelocity;
        }



    }



}
