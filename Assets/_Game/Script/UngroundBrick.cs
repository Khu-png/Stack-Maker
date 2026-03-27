using UnityEngine;

public class UnroundBrick : MonoBehaviour
{
    [SerializeField] private float height = 0.5f;
    [SerializeField] private GameObject brickHolder;
    private bool isTouched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTouched && other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.RemoveBrick(height);

                if (brickHolder != null)
                {
                    brickHolder.SetActive(true);
                }
            }
        }
    }
}