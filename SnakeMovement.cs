using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private SpriteRenderer player;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;

    public Sprite up;
    public Sprite down;
    public Sprite leftRight;
    public Vector2 direction = Vector2.right;
    public float moveSpeed = 5f; 
    public float segmentSpacing = 0.5f; 

    private void Start()
    {
        segments.Add(this.transform);
        player = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction != Vector2.down)
            {
                direction = Vector2.up;
                player.sprite = up;
                player.flipX = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction != Vector2.up)
            {
                direction = Vector2.down;
                player.sprite = down;
                player.flipX = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction != Vector2.left)
            {
                direction = Vector2.right;
                player.sprite = leftRight;
                player.flipX = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction != Vector2.right)
            {
                direction = Vector2.left;
                player.sprite = leftRight;
                player.flipX = true;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + (Vector3)direction * moveSpeed * Time.fixedDeltaTime;

        for (int i = 1; i < segments.Count; i++)
        {
            Transform currentSegment = segments[i];
            Transform previousSegment = segments[i - 1];

            float distance = Vector3.Distance(previousSegment.position, currentSegment.position);

            Vector3 newPosition = previousSegment.position;

            if (distance > segmentSpacing)
            {
                float step = moveSpeed * Time.fixedDeltaTime;
                currentSegment.position = Vector3.MoveTowards(currentSegment.position, newPosition, step);
            }
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
        }
    }
}
