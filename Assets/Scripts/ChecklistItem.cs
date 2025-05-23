using UnityEditor;
using UnityEngine;

public class ChecklistItem : MonoBehaviour, IInteractable
{
    public string ID;
    public string HoverText;
    public string DisplayName;

    public GameObject collectFXPrefab;
    public DialogueSequence InternalDialogue;

    private Outline _outline;
    private int _currentIndexDialogue = 0;

    void Start()
    {
        GameManager.Instance.RegisterChecklistItem(ID, DisplayName);

        _outline = GetComponent<Outline>();

        // If outline hasn't been assigned, assign it
        if (_outline == null)
        {
            _outline = gameObject.AddComponent<Outline>();
        }

        // Customize outline
        _outline.OutlineWidth = 10f;

        DisableOutline();
    }

    public void Interact()
    {
        Debug.Log($"Collected: {DisplayName}");

        // Play FX
        GameObject FXInstance = Instantiate(collectFXPrefab, transform.position, Quaternion.identity);
        Destroy(FXInstance, 3f);

        if (InternalDialogue == null || InternalDialogue.lines.Length == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        UIManager.Instance.UpdateDialogueLabel(InternalDialogue.lines[_currentIndexDialogue]);
        _currentIndexDialogue = (_currentIndexDialogue + 1) % InternalDialogue.lines.Length;

        gameObject.SetActive(false);
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
