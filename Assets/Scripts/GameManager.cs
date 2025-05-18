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
        UpdateGameState(GameState.First);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.First:
                HandleFirst();
                break;
            case GameState.Second:
                HandleSecond();
                break;
            case GameState.Third:
                HandleThird();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleFirst()
    {

    }

    private void HandleSecond()
    {

    }

    private void HandleThird()
    {

    }


}

public enum GameState
{
    First,
    Second,
    Third
}