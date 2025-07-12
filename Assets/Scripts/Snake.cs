using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    public Transform segmentPrefab;
    private bool shouldGrow = false;
    private Vector3 lastTailPosition;

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    public void SetDirectionUp()
    {
        if (_direction != Vector2.down) _direction = Vector2.up;
    }

    public void SetDirectionDown()
    {
        if (_direction != Vector2.up) _direction = Vector2.down;
    }

    public void SetDirectionLeft()
    {
        if (_direction != Vector2.right) _direction = Vector2.left;
    }

    public void SetDirectionRight()
    {
        if (_direction != Vector2.left) _direction = Vector2.right;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down) _direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up) _direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right) _direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left) _direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        // Store the tail's position before movement
        lastTailPosition = _segments[_segments.Count - 1].position;

        // Move each segment to the position of the segment ahead of it
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        // Move head
        this.transform.position = new Vector2(
            Mathf.Round(transform.position.x) + _direction.x,
            Mathf.Round(transform.position.y) + _direction.y
        );

        // Grow after movement
        if (shouldGrow)
        {
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = lastTailPosition;
            _segments.Add(segment);
            shouldGrow = false;
        }
    }

    private void Grow()
    {
        shouldGrow = true; // Defer growth until after the next move
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
        _direction = Vector2.right;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            SoundManager.Instance.EatFoodSound();
            Grow();
        }
        else if (other.CompareTag("SnakeSegment") || (other.CompareTag("Obstacle")))
        {
            SoundManager.Instance.HitObstacleSound();
            ResetState();
        }
    }
}