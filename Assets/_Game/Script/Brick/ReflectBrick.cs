using UnityEngine;

public class Reflect : MonoBehaviour
{
    [SerializeField] private Transform model;  
    [SerializeField] private GameObject nodePad;
    [SerializeField] private float height;

    private bool isTouched = false;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && player.isMoving)
        {
            int rotY = Mathf.RoundToInt(model.eulerAngles.y / 90f) * 90 % 360;
            player.ReflectDirection(rotY);
            if (!isTouched)
            {
                player.AddBrick(height);
                nodePad.SetActive(false);
                isTouched = true;
            }
        }
    }
}