using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMP_Text StateLabel;
    public TMP_Text DialogueLabel;
    public Interactor Interactor;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        UpdateStateLabel(state.ToString());
    }

    private void Start()
    {
        UpdateDialogueLabel("");
    }

    public void UpdateStateLabel(string label)
    {
        StateLabel.text = label;
    }

    public void UpdateDialogueLabel(string dialogue)
    {
        DialogueLabel.text = dialogue;

        StartCoroutine(ClearDialogueAfterDelay(3f));
    }

    private IEnumerator ClearDialogueAfterDelay(float delay)
    {
        Interactor.UpdateCanInteract(false);
        yield return new WaitForSeconds(delay);
        DialogueLabel.text = "";
        Interactor.UpdateCanInteract(true);
    }
}
