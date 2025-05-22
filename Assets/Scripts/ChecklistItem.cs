using UnityEditor;
using UnityEngine;

public class ChecklistItem : MonoBehaviour, IInteractable
{
    public UIManager UIManager;
    public string HoverText = "";
    public DialogueSequence InternalDialogue;

    private Outline _outline;
    //private int _currentIndexDialogue = 0;

    void Start()
    {
        _outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        gameObject.SetActive(false);
        
        /*
        if (InternalDialogue == null || InternalDialogue.lines.Length == 0)
        {
            return;
        }

        UIManager.UpdateDialogueLabel(InternalDialogue.lines[_currentIndexDialogue]);
        _currentIndexDialogue = (_currentIndexDialogue + 1) % InternalDialogue.lines.Length;
        */
    }

    public string GetHoverText()
    {
        return HoverText;
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
