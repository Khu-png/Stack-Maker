using UnityEngine;

public class UnroundBrick : MonoBehaviour
{
    [SerializeField] private float height = 0.5f;
    [SerializeField] private Transform brickHolder;
    private bool isTouched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTouched && other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Transform brick = player.RemoveBrick(height);

                if (brick != null)
                {
                    brick.SetParent(brickHolder);
                    brick.position = transform.position;
                    isTouched = true;
                }
            }
        }
    }
}