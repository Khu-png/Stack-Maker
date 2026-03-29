using TMPro;
using UnityEngine;

public class BrickCountUI : MonoBehaviour
{
    public TextMeshProUGUI brickText;

    void Update()
    {
        brickText.text = GameManager.totalBricks.ToString();
    }
}