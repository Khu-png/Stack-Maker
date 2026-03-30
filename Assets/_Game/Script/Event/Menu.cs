using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
    [SerializeField] private GameObject playButton;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Lvl 1");
    }
}
