using System.Collections;
using TMPro.Examples;
using UnityEditor;
using UnityEngine;

public class TrapItem : MonoBehaviour, IInteractable
{
    public UIManager UIManager;
    public string HoverText = "";
    public DialogueSequence InternalDialogue;
    public Transform FocusPoint;

    private Outline _outline;
    private int _currentIndexDialogue = 0;

    void Start()
    {
        _outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        StartCoroutine(HyperfocusSequence());
    }

    private IEnumerator HyperfocusSequence()
    {
        // Player movement is disabled and enabled in PlayerMovement.cs
        GameManager.Instance.UpdateGameState(GameState.Hyperfocus);

        // Camera zoom to focus on object is set up in PlayerLook.cs
        // Hover text is hidden during hyperfocus state in UIManager.cs

        // Display dialogue
        // Dialogue is set to clear after 3 seconds in UIManager.cs
        UIManager.UpdateDialogueLabel(InternalDialogue.lines[_currentIndexDialogue]);
        _currentIndexDialogue = (_currentIndexDialogue + 1) % InternalDialogue.lines.Length;

        // Wait 3 seconds before updating game state
        yield return new WaitForSeconds(3f);
        GameManager.Instance.UpdateGameState(GameState.RegularFocus);
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
