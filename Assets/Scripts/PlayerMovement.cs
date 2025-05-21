using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSmoothTime;
    public float WalkSpeed;
    public float RunSpeed;
    public float Gravity;
    public float GroundSnapSpeed;

    private CharacterController _controller;
    private Vector3 _currentMoveVelocity;
    private Vector3 _moveDampVelocity;
    private Vector3 _verticalVelocity;
    private bool _isGrounded;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        _isGrounded = _controller.isGrounded;

        Vector3 playerInput = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        };

        if (playerInput.magnitude > 1f)
        {
            playerInput.Normalize();
        }

        Vector3 moveVector = transform.TransformDirection(playerInput);
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;

        _currentMoveVelocity = Vector3.SmoothDamp(
            _currentMoveVelocity,
            moveVector * currentSpeed,
            ref _moveDampVelocity,
            MoveSmoothTime
        );

        // Apply gravity or ground snapping if necessary
        if (_isGrounded && _verticalVelocity.y < 0)
        {
            _verticalVelocity.y = -GroundSnapSpeed;
        }
        else
        {
            _verticalVelocity.y += Gravity * Time.deltaTime;
        }

        Vector3 fullMovement = _currentMoveVelocity + _verticalVelocity;    
        _controller.Move(fullMovement * Time.deltaTime);
    }
}
