using UnityEngine;

public class Reflect : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private GameObject nodePad;

    [SerializeField] private float height;
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && player.isMoving)
        {
            Debug.Log("Before: " + player.direction);
            float y = model.eulerAngles.y;
            int rot = Mathf.RoundToInt(y / 90f) * 90 % 360;
            if (rot == 0)
            {
                if (player.direction == Vector3.back)
                {
                    player.direction = Vector3.left;
                }
                else if (player.direction == Vector3.right)
                {
                    player.direction = Vector3.forward;
                }
            }
            else if (rot == 90)
            {
                if (player.direction == Vector3.left)
                {
                    player.direction = Vector3.forward;
                }
                else if (player.direction == Vector3.back)
                {
                    player.direction = Vector3.right;
                }
            }
            else if (rot == 180)
            {
                if (player.direction == Vector3.forward)
                {
                    player.direction = Vector3.right;
                }
                else if (player.direction == Vector3.left)
                {
                    player.direction = Vector3.back;
                }
            }
            else if (rot == 270)
            {
                if (player.direction == Vector3.forward)
                {
                    player.direction = Vector3.left;
                }
                else if (player.direction == Vector3.right)
                {
                    player.direction = Vector3.back;
                }
            }
            Debug.Log("After: " + player.direction);
            player.AddBrick(height);
            
            player.transform.rotation = Quaternion.LookRotation(player.direction);
        }   
    }
}
