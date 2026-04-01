using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask brickLayer;

    [Header("Model")]
    [SerializeField] private Transform model;

    [Header("Stack")]
    [SerializeField] private float nextBrickY;
    [SerializeField] private GameObject playerBrickPrefab;

    private Vector3 startPosition;
    public MoveDirection direction = MoveDirection.None;
    public bool isMoving = false;
    private Vector3 targetPosition;

    public enum MoveDirection { None, Up, Down, Left, Right }

    private List<Transform> bricks = new List<Transform>();

    void Update()
    {
        if (!isMoving)
            HandleInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
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

            if (Mathf.Abs(deltaX - deltaY) < 10f) return;

            if (deltaX > deltaY)
                direction = delta.x > 0 ? MoveDirection.Right : MoveDirection.Left;
            else
                direction = delta.y > 0 ? MoveDirection.Up : MoveDirection.Down;

            Vector3 dirVector = MoveDirectionToVector(direction);
            
            if (HasNextNode(dirVector, out Vector3 next))
            {
                targetPosition = next;
                isMoving = true;
            }
            else
            {
                isMoving = false; 
            }
        }
    }

    void Move()
    {
        if (!isMoving) return;

        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = SnapToGrid(targetPosition);

            Vector3 dirVector = MoveDirectionToVector(direction);
            
            if (HasNextNode(dirVector, out Vector3 next))
            {
                targetPosition = next;
            }
            else
            {
                isMoving = false; 
            }
        }
    }

    bool HasNextNode(Vector3 dir, out Vector3 nextPos)
    {
        nextPos = transform.position + dir;
        nextPos = SnapToGrid(nextPos);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, 1f, brickLayer))
        { 
            return true;
        }

        Debug.DrawRay(transform.position, dir * 1f, Color.red, 1f);
        return false;
    }


    Vector3 SnapToGrid(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), pos.y, Mathf.Round(pos.z));
    }
    
    public void ReflectDirection(int nodeRotationY)
    {
        direction = GetReflectedDirection(direction, nodeRotationY);
        model.rotation = Quaternion.LookRotation(MoveDirectionToVector(direction));
    }

    private MoveDirection GetReflectedDirection(MoveDirection currentDir, int rot)
    {
        switch (rot)
        {
            case 0:
                if (currentDir == MoveDirection.Down) return MoveDirection.Left;
                if (currentDir == MoveDirection.Right) return MoveDirection.Up;
                break;
            case 90:
                if (currentDir == MoveDirection.Left) return MoveDirection.Up;
                if (currentDir == MoveDirection.Down) return MoveDirection.Right;
                break;
            case 180:
                if (currentDir == MoveDirection.Up) return MoveDirection.Right;
                if (currentDir == MoveDirection.Left) return MoveDirection.Down;
                break;
            case 270:
                if (currentDir == MoveDirection.Up) return MoveDirection.Left;
                if (currentDir == MoveDirection.Right) return MoveDirection.Down;
                break;
        }
        return currentDir;
    }
    
    public void AddBrick(float height)
    {
        GameObject newBrick = Instantiate(playerBrickPrefab, model);
        newBrick.transform.localRotation = Quaternion.identity;
        newBrick.transform.localPosition = new Vector3(0, nextBrickY - 0.8f, 0);
        bricks.Add(newBrick.transform);
        nextBrickY -= height;

        Vector3 pos = model.position;
        pos.y += height;
        model.position = pos;
        
        UIManager gm = FindFirstObjectByType<UIManager>();
        if (gm != null)
            gm.AddBrick(1);
    }

    public void RemoveBrick(float height)
    {
        if (bricks.Count <= 0)
        {
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
                gm.GameLose();
            return;
        }

        Transform lastBrick = bricks[bricks.Count - 1];
        bricks.RemoveAt(bricks.Count - 1);
        Destroy(lastBrick.gameObject);

        nextBrickY += height;

        Vector3 pos = model.position;
        pos.y -= height;
        model.position = pos;
    }

    public void ClearBrick()
    {
        if (bricks.Count > 0)
        {
            foreach (Transform brick in bricks)
            {
                Destroy(brick.gameObject);
            }

            bricks.Clear();
            
            nextBrickY = 0;

            Vector3 pos = model.position;
            pos.y = 0;
            model.position = pos;
        }
    }
    
    Vector3 MoveDirectionToVector(MoveDirection dir)
    {
        switch (dir)
        {
            case MoveDirection.Up: return Vector3.forward;
            case MoveDirection.Down: return Vector3.back;
            case MoveDirection.Left: return Vector3.left;
            case MoveDirection.Right: return Vector3.right;
            default: return Vector3.zero;
        }
    }
}