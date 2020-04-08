using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameController : MonoBehaviour
{
    static public GameController Instance { get; private set; }
    
    public enum GameState
    {
        Game,
        Pause,
        Victory,
    }
    
    public GameState State { get; private set; }

    private void Awake() =>
        Instance = this;

    private void OnDestroy() =>
        Instance = null;

    public void NotifyWin() =>
        State = GameState.Victory;
    
    private void Update()
    {
        Time.timeScale = State == GameState.Game ? 1.0f : 0.0f;
        switch (State)
        {
            case GameState.Game:
                if (Input.GetButtonDown("Pause"))
                    State = GameState.Pause;
                break;
            case GameState.Pause:
                if (Input.GetButtonDown("Pause"))
                    State = GameState.Game;
                if (Input.GetButtonDown("Restart"))
                    SceneManager.LoadScene("MainScene");
                break;
            case GameState.Victory:
                if (Input.GetButtonDown("Restart"))
                    SceneManager.LoadScene("MainScene");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
