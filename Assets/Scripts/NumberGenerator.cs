using UnityEditor;
using UnityEngine;

public class NumberGenerator : MonoBehaviour, IInteractable
{
    [SerializeField] public string ObjectName = "";
    [SerializeField] public string ObjectDialogue = "";

    public void Interact()
    {
        Debug.Log(ObjectDialogue);
    }

    public string GetHoverText()
    {
        return ObjectName;
    }
}
