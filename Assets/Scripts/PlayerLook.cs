using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform PlayerCamera;
    public Vector2 Sensitivities;

    private Vector2 _XYRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseInput = new Vector2
        {
            x = Input.GetAxis("Mouse X"),
            y = Input.GetAxis("Mouse Y")
        };

        _XYRotation.x -= mouseInput.y * Sensitivities.y;
        _XYRotation.y += mouseInput.x * Sensitivities.x;

        _XYRotation.x = Mathf.Clamp(_XYRotation.x, -90f, 90f);

        transform.eulerAngles = new Vector3(0f, _XYRotation.y, 0f);
        PlayerCamera.localEulerAngles = new Vector3(_XYRotation.x, 0f, 0f);
    }
}
