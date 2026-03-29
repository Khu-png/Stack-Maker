using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    // 🔥 chỉ giữ data, không giữ object
    public static int totalBricks = 0;
    public static int savedBricks = 0;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void AddBrick(int amount)
    {
        totalBricks += amount;
    }

    public void LevelUp()
    {
        Time.timeScale = 1f;

        if (winPanel != null)
            winPanel.SetActive(false);

        savedBricks = totalBricks;

        string current = SceneManager.GetActiveScene().name;

        switch (current)
        {
            case "Lvl 1": SceneManager.LoadScene("Lvl 2"); break;
            case "Lvl 2": SceneManager.LoadScene("Lvl 3"); break;
            case "Lvl 3": SceneManager.LoadScene("Lvl 4"); break;
            case "Lvl 4": SceneManager.LoadScene("Lvl 5"); break;
        }
    }

    public void GameReplay()
    {
        savedBricks = 0;
        totalBricks = 0;

        Time.timeScale = 1f;

        if (winPanel != null)
            winPanel.SetActive(false);

        SceneManager.LoadScene("Lvl 1");
    }

    public void GamePause()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void GameResume()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void GameWin()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void GameLose()
    {
        if (losePanel != null)
            losePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void GameRetry()
    {
        if (losePanel != null)
            losePanel.SetActive(false);

        Time.timeScale = 1f;

        totalBricks = savedBricks;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}