using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private Rigidbody2D _rb;
    private List<Transform> _segments;
    public Transform segmentPrefab;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.WakeUp(); // This will ensure the Rigidbody2D is awake when the game starts
    }

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);

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
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        Vector2 newPos = new Vector2(
            Mathf.Round(transform.position.x) + _direction.x,
            Mathf.Round(transform.position.y) + _direction.y
        );
        _rb.MovePosition(newPos);
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
        }

    }
}