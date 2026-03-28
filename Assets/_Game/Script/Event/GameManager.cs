using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    
    public static GameManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void LevelUp()
    {
        Time.timeScale = 1f;

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
            case "Lvl 5":
                Debug.Log("You Win!");
                break;
        }
        
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
}
