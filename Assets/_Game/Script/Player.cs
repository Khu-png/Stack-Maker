using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Transform  model;
    [SerializeField] private float nextBrickY;
    
    private Vector3 startPosition;
    public Vector3 direction;
    public bool isMoving = false;
    private List<Transform> bricks = new List<Transform>();
    
    void  Start()
    {
        
    }

    void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.linearVelocity = new Vector3(direction.x * speed, 0f, direction.z * speed);
        }
    }
    
    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 endPosition = Input.mousePosition;

            Vector3 delta = endPosition - startPosition;

            float deltaX = Mathf.Abs(delta.x);
            float deltaY = Mathf.Abs(delta.y);

            if (Math.Abs(deltaX - deltaY) < 10f)
            {
                return;
            }
            
            isMoving = true;
            
            if (deltaX > deltaY)
            {
                if (delta.x > 0) direction = Vector3.right;
                else if (delta.x < 0) direction = Vector3.left;
            }
            else
            {
                if (delta.y > 0) direction = Vector3.forward;
                else if (delta.y < 0) direction = Vector3.back;
            }
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

        brick.localScale = Vector3.one;
        
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

