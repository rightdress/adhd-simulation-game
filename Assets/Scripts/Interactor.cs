using UnityEngine;
using TMPro;

interface IInteractable
{
    public void Interact();
    public string GetHoverText();
    public void DisableOutline();
    public void EnableOutline();
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactorSource;
    [SerializeField] private float _interactRange = 3f;
    [SerializeField] private TMP_Text _hoverTextLabel;
    
    private IInteractable _prevInteractable;

    void LateUpdate()
    {
        Ray r = new Ray(_interactorSource.position, _interactorSource.forward);

        // Show hover text if ray hits with interactable object
        if (Physics.Raycast(r, out RaycastHit hitObj, _interactRange))
        {
            if (hitObj.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                // Only update label and highlight when object being interacted with changes
                if (interactObj != _prevInteractable)
                {
                    interactObj.EnableOutline();

                    _hoverTextLabel.text = interactObj.GetHoverText();
                    _prevInteractable = interactObj;
                }

                // Press E to interact with object
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
            _prevInteractable.DisableOutline();

            _hoverTextLabel.text = "";
            _prevInteractable = null;
        }
    }
}
