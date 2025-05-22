using System.Collections;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform PlayerCamera;
    public Vector2 Sensitivities;

    private Vector2 _XYRotation;
    private bool _canMove = true;
    private Camera _camera;

    void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = PlayerCamera.GetComponent<Camera>();
    }

    void Update()
    {
        if (!_canMove)
        {
            return;
        }

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

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.RegularFocus:
                _canMove = true;
                break;
            case GameState.Hyperfocus:
                _canMove = false;
                StartZoom(60f, 20f, 3f);
                break;
        }
    }

    public void StartZoom(float startFOV, float endFOV, float duration)
    {
        StartCoroutine(Zoom(startFOV, endFOV, duration));
    }

    private IEnumerator Zoom(float startFOV, float endFOV, float duration)
    {
        float elapsed = 0f;
        _camera.fieldOfView = startFOV;

        while (elapsed < duration)
        {
            _camera.fieldOfView = Mathf.Lerp(startFOV, endFOV, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset FOV
        _camera.fieldOfView = startFOV;
    }
}
