using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject menuPanel;
    
    [Header("Button")]
    [SerializeField] private GameObject pauseButton;
    
    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI brickText;
    [SerializeField] private TextMeshProUGUI levelText;

    private int totalBricks = 0;
    private int savedBricks = 0;
    
    private void Start()
    {
        ResetUI();
        pauseButton?.SetActive(false);
        
        UpdateLevelText(LevelManager.Instance.CurrentLevel + 1);
        
        savedBricks = PlayerPrefs.GetInt("TotalBricks", 0);
        brickText.text = savedBricks.ToString();
    }

    public void AddBrick(int amount)
    {
        totalBricks += amount;
        if (brickText != null)
            brickText.text = totalBricks.ToString();
    }

    public void UpdateLevelText(int currentLevel)
    {
        if (levelText != null)
            levelText.text = "Level " + currentLevel;
    }

    public void UIWin()
    {
        savedBricks = totalBricks;
        
        PlayerPrefs.SetInt("TotalBricks", savedBricks);
        PlayerPrefs.Save();

        winPanel?.SetActive(true);
    }
    
    public void UILose() => losePanel?.SetActive(true);
    public void UIPause() => pausePanel?.SetActive(true);
    public void ResetUI()
    {
        pausePanel?.SetActive(false);
        winPanel?.SetActive(false);
        losePanel?.SetActive(false);
    }
    
    public void OnClickPlay()
    {
        menuPanel?.SetActive(false);
        levelText?.gameObject.SetActive(true);
        pauseButton?.SetActive(true);
        GameManager.Instance.GameStart();
    }

    public void OnClickMenu()
    {
        ResetUI();
        menuPanel?.SetActive(true);
        pauseButton?.SetActive(false);
        levelText?.gameObject.SetActive(false);
        GameManager.Instance.GameMenu();
        brickText.text = savedBricks.ToString();
    }

    public void OnClickPause() => GameManager.Instance.GamePause();
    public void OnClickResume() => GameManager.Instance.GameResume();
    public void OnClickNext() => GameManager.Instance.GameNextLevel();

    public void OnClickRestart()
    {
        GameManager.Instance.GameRestart();
        brickText.text = savedBricks.ToString(); 
    }
    
    // hàm chỉ dùng cho testing
    public void OnClickClearLevel()
    {
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("TotalBricks");

        savedBricks = 0;
        brickText.text = "0";

        LevelManager.Instance.ResetLevelState();
        menuPanel.SetActive(true);
    }
}