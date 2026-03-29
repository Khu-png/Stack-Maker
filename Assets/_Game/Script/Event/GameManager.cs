using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    
    public static GameManager Instance;
    public int totalBricks = 0;
    public int savedBricks = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void AddBrick(int amount)
    {
        totalBricks += amount;
    }
    
    public void LevelUp()
    {
        Time.timeScale = 1f;
        winPanel.SetActive(false);
        savedBricks = totalBricks;
        
        string current = SceneManager.GetActiveScene().name;
        switch (current)
        {
            case "Lvl 1":
                SceneManager.LoadScene("Lvl 2");
                break;
            case "Lvl 2":
                SceneManager.LoadScene("Lvl 3");
                break;
            case "Lvl 3":
                SceneManager.LoadScene("Lvl 4");
                break;
            case "Lvl 4":
                SceneManager.LoadScene("Lvl 5");
                break;
        }
    }

    public void GameReplay()
    {
        savedBricks = 0;
        totalBricks = 0;
        Time.timeScale = 1f;
        winPanel.SetActive(false);
        SceneManager.LoadScene("Lvl 1");
    }
    
    public void GamePause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void GameResume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void GameWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameRetry()
    {
        losePanel.SetActive(false);
        Time.timeScale = 1f;
        totalBricks = savedBricks;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
