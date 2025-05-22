using UnityEngine;

public class SunController : MonoBehaviour
{
    public float RegularRotationSpeed = 5f;
    public float HyperfocusRotationSpeed = 25f;

    private Transform _sunTransform;
    private float _currentSpeed;
    private float _sunAngle = 0f;

    void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        _sunTransform = transform;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    void Update()
    {
        _sunAngle += _currentSpeed * Time.deltaTime;

        // Ensure sun's angle loops between 0 and 90
        if (_sunAngle > 90f)
        {
            _sunAngle = 0f;
        }

        _sunTransform.rotation = Quaternion.Euler(_sunAngle, 0f, 0f);
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.RegularFocus:
                _currentSpeed = RegularRotationSpeed;
                break;
            case GameState.Hyperfocus:
                _currentSpeed = HyperfocusRotationSpeed;
                break;
        }
    }

}
