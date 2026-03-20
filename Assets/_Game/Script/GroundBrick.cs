using UnityEngine;

public class GroundBrick : MonoBehaviour
{
    [SerializeField] private float height = 0.5f;
    private bool isTouched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTouched && other.CompareTag("Player"))
        {
            isTouched = true;

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // Lấy cube con
                Transform brick = transform.GetChild(0);

                player.AddBrick(brick, height);
            }
        }
    }
}