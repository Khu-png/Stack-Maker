using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private Transform mapHolder;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CameraFollow cameraFollow;

    private GameObject playerInstance;
    private GameObject currentLevel;
    private int level = 0;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Level", 0);
        LoadLevel(level);
        OnInit();
    }

    private void OnInit()
    {
        if (playerPrefab == null) return;

        Vector3 spawnPos = new Vector3(0, 1, 0);
        playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        
        if (cameraFollow != null)
            cameraFollow.SetTarget(playerInstance.transform);
    }

    public void LoadLevel(int index)
    {
        if (levels.Count == 0) return;

        if (index >= levels.Count) index = 0;

        if (currentLevel != null)
            Destroy(currentLevel);

        currentLevel = Instantiate(levels[index], mapHolder);
    }

    public void OnNextLevel()
    {
        level++;
        if (level >= levels.Count) level = 0;

        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();

        OnReplay();
    }

    public void OnReplay()
    {
        Despawn();
        LoadLevel(level);
        OnInit();
        UIManager.Instance.UpdateLevelText();
    }

    private void Despawn()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = null;
        }

        if (currentLevel != null)
        {
            Destroy(currentLevel);
            currentLevel = null;
        }
    }

    public void OnWin() => GameManager.Instance.GameWin();
    public void OnLose() => GameManager.Instance.GameLose();
}