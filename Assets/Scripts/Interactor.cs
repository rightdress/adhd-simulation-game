using UnityEngine;
using TMPro;

interface IInteractable
{
    public void Interact();
    public string GetHoverText();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 3f;
    public TMP_Text hoverText;

    private IInteractable _prevInteractable;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        // Show hover text if ray hits with interactable object
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                // Only update label with object being interacted with changes
                if (interactObj != _prevInteractable)
                {
                    hoverText.text = interactObj.GetHoverText();
                    _prevInteractable = interactObj;
                }

                // Make object interact if E is pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }

                return; // Prevent label from being cleared if ray hits interactable object
            }
        }

        // Clear label if ray does not hit interactable object
        if (_prevInteractable != null)
        {
            hoverText.text = "";
            _prevInteractable = null;
        }
    }
}
