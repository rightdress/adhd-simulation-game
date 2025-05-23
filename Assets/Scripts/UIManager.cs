using UnityEngine;
using TMPro;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text StateLabel;
    public TMP_Text HoverLabel;
    public TMP_Text DialogueLabel;
    public GameObject ChecklistPanel;
    public GameObject ChecklistItemPrefab;

    public Interactor Interactor;

    void Awake()
    {
        Instance = this;
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

    public void AddChecklistItem(string id, string displayName) {
        GameObject newItem = Instantiate(ChecklistItemPrefab, ChecklistPanel.transform);
        
        TMP_Text label = newItem.GetComponentInChildren<TMP_Text>();
        label.text = displayName;
    }

    public void ShuffleChecklistItems()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in ChecklistPanel.transform)
        {
            children.Add(child);
        }

        // Using Fisher-Yates algorithm to shuffle
        for (int i = 0; i < children.Count; i++)
        {
            int randIndex = Random.Range(i, children.Count);
            Transform temp = children[i];
            children[i] = children[randIndex];
            children[randIndex] = temp;
        }

        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }
}
