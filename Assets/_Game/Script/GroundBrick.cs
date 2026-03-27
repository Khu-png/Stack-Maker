using UnityEngine;

public class GroundBrick : MonoBehaviour
{
    [SerializeField] private float height = 0.1f;
    private bool isTouched = false;
    [SerializeField] private GameObject nodePad;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTouched && other.CompareTag("Player"))
        {
            isTouched = true;

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Transform brick = transform.GetChild(0);
                player.AddBrick(height);
                
                if (nodePad != null)
                {
                    nodePad.SetActive(false);
                }
            }
        }
    }
}