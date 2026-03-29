using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true;
            }

            Invoke(nameof(CallWin), 0.1f);
        }
    }

    private void CallWin()
    {
        GameManager gm = FindFirstObjectByType<GameManager>();

        if (gm != null)
        {
            gm.GameWin();
        }
    }
}