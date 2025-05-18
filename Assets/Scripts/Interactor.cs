using UnityEngine;
using TMPro;
using System.Collections;

interface IInteractable
{
    public void Interact();
    public string GetHoverText();
    public void DisableOutline();
    public void EnableOutline();
    public void SetOutlineColor(Color color);
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactorSource;
    [SerializeField] private float _interactRange = 3f;
    [SerializeField] private TMP_Text _hoverTextLabel;
    private bool _canInteract = true;
    
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
                    if (_canInteract)
                    {
                        interactObj.Interact();
                        StartCoroutine(FlashRedOutline(interactObj));
                    }
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

    public void UpdateCanInteract(bool canInteract)
    {
        _canInteract = canInteract;
    }

    private IEnumerator FlashRedOutline(IInteractable interactObj)
    {
        interactObj.SetOutlineColor(Color.red);
        yield return new WaitForSeconds(3f);
        interactObj.SetOutlineColor(Color.white);
    }
}
