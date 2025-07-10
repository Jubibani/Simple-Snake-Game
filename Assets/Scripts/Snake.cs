using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.WakeUp(); // This will ensure the Rigidbody2D is awake when the game starts
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
        Vector2 newPos = new Vector2(
            Mathf.Round(transform.position.x) + _direction.x,
            Mathf.Round(transform.position.y) + _direction.y
        );
        _rb.MovePosition(newPos);
    }
}