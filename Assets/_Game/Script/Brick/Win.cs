using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Player player = other.GetComponent<Player>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            player.ClearBrick();
            Invoke(nameof(CallWin), 0.1f);
        }
    }

    private void CallWin()
    {
        LevelManager.Instance.OnWin();
    }
}