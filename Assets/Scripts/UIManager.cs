using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMP_Text StateLabel;
    public TMP_Text HoverLabel;
    public TMP_Text DialogueLabel;
    public Interactor Interactor;

    void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        UpdateStateLabel(state.ToString());

        switch (state)
        {
            case GameState.RegularFocus:
                HoverLabel.gameObject.SetActive(true);
                break;
            case GameState.Hyperfocus:
                HoverLabel.gameObject.SetActive(false);
                break;
        }
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
