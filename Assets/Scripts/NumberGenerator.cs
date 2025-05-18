using UnityEditor;
using UnityEngine;

public class NumberGenerator : MonoBehaviour, IInteractable
{
    [SerializeField] public string ObjectName = "";
    [SerializeField] public string ObjectDialogue = "";

    private Outline _outline;

    void Start()
    {
        _outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        Debug.Log(ObjectDialogue);
    }

    public string GetHoverText()
    {
        return ObjectName;
    }

    public void DisableOutline()
    {
        _outline.enabled = false;
    }

    public void EnableOutline()
    {
        _outline.enabled = true;
    }
}
