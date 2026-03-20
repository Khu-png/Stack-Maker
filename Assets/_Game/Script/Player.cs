using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Transform  model;
    [SerializeField] private float nextBrickY;
    
    private Vector3 direction;
    private bool isMoving = false;
    private List<Transform> bricks = new List<Transform>();
    
    void  Start()
    {
        
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isMoving = true;
                direction = Vector3.forward;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                isMoving = true;
                direction = Vector3.back;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                isMoving = true;
                direction = Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                isMoving = true;
                direction = Vector3.right;
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.linearVelocity = new Vector3(direction.x * speed, 0f, direction.z * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            isMoving = false;
            rb.linearVelocity = Vector3.zero;
        }
    }

    public void AddBrick(Transform brick, float height)
    {
        brick.SetParent(model);
        
        brick.localRotation = Quaternion.identity;
        
        brick.localPosition = new Vector3(0, nextBrickY - 1.7f, 0);
        
        bricks.Add(brick);
        
        nextBrickY -= height;
        
        Vector3 position = model.transform.position;
        position.y += height;
        model.transform.position = position;
    }

    public Transform RemoveBrick(float height)
    {
        if (bricks.Count == 0)
        {
            return null;
        }
        
        Transform lastBrick = bricks[bricks.Count - 1];
        bricks.RemoveAt(bricks.Count - 1);
        
        nextBrickY -= height;
        
        Vector3 position = model.transform.position;
        position.y -= height;
        model.transform.position = position;

        return lastBrick;
    }
}

