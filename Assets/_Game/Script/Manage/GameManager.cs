using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { MainMenu, Playing, Paused, Win, Lose }
    private GameState currentState;
    public GameState CurrentState => currentState;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 120;
        ChangeState(GameState.MainMenu);
    }

    #region Game Control
    public void GameStart() => ChangeState(GameState.Playing);
    public void GamePause() => ChangeState(GameState.Paused);
    public void GameResume() => ChangeState(GameState.Playing);
    public void GameWin() => ChangeState(GameState.Win);
    public void GameLose() => ChangeState(GameState.Lose);
    public void GameRestart()
    {
        ChangeState(GameState.Playing);
        LevelManager.Instance.OnReplay();
    }
    public void GameNextLevel()
    {
        ChangeState(GameState.Playing);
        LevelManager.Instance.OnNextLevel();
    }
    public void GameMenu()
    {
        ChangeState(GameState.MainMenu);
        LevelManager.Instance.OnReplay();
    }
    #endregion

    private void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                UIManager.Instance.ResetUI();
                break;

            case GameState.Playing:
                Time.timeScale = 1;
                UIManager.Instance.ResetUI();
                break;

            case GameState.Paused:
                Time.timeScale = 0;
                UIManager.Instance.UIPause();
                break;

            case GameState.Win:
                Time.timeScale = 0;
                UIManager.Instance.UIWin();
                break;

            case GameState.Lose:
                Time.timeScale = 0;
                UIManager.Instance.UILose();
                break;
        }
    }
    
    
}