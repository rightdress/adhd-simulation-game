using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public UIManager UIManager;
    public string ObjectName = "";
    public DialogueSequence Dialogue;

    private Outline _outline;
    private int _currentIndexDialogue = 0;

    void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        //throw new System.NotImplementedException();
    }

    void Start()
    {
        _outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        if (Dialogue == null || Dialogue.lines.Length == 0)
        {
            return;
        }

        UIManager.UpdateDialogueLabel(Dialogue.lines[_currentIndexDialogue]);
        _currentIndexDialogue = (_currentIndexDialogue + 1) % Dialogue.lines.Length;
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

    public void SetOutlineColor(Color color)
    {
        _outline.OutlineColor = color;
    }
}
