using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

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


}

public enum GameState
{
    RegularFocus,
    Hyperfocus
}