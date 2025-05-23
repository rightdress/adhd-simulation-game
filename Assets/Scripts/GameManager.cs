using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    public static event Action<string, string> OnChecklistItemCollected;

    private Dictionary<string, string> _allChecklistItems = new Dictionary<string, string>();

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.RegularFocus);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.RegularFocus:
                HandleRegularFocus();
                break;
            case GameState.Hyperfocus:
                HandleHyperfocus();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleRegularFocus()
    {
        Debug.Log("Entered Regular Focus state");
    }

    private void HandleHyperfocus()
    {
        Debug.Log("Entered Hyperfocus state");
    }

    public void CollectItem(string id, string displayName)
    {
        
    }

    public void RegisterChecklistItem(string id, string displayName)
    {
        // Add item to dictionary of checklist items
        _allChecklistItems[id] = displayName;
        OnChecklistItemCollected?.Invoke(id, displayName);

        // Add checklist items to checklist in UI
        UIManager.Instance.AddChecklistItem(id, displayName);
    }
}

public enum GameState
{
    RegularFocus,
    Hyperfocus
}