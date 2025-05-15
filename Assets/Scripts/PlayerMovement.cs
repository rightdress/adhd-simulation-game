using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSmoothTime;
    public float WalkSpeed;
    public float RunSpeed;

    private CharacterController _controller;
    private Vector3 _currentMoveVelocity;
    private Vector3 _moveDampVelocity;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    void Update()
    {
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

        _controller.Move(_currentMoveVelocity * Time.deltaTime);
    }
}
