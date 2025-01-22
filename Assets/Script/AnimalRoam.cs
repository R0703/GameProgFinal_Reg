using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRoam : MonoBehaviour
{
    public float moveSpeed = 2f; 
    public float changeDirectionInterval = 2f;
    private Vector2 targetDirection; 
    private float changeDirectionTimer;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            SetRandomDirection();
        }
    }

    private void Move()
    {
        Vector2 newPosition = (Vector2)transform.position + targetDirection * moveSpeed * Time.deltaTime;
        transform.position = newPosition;

        if (targetDirection.x != 0)
        {
            spriteRenderer.flipX = targetDirection.x > 0; 
        }
    }

    private void SetRandomDirection()
    {
        targetDirection = Random.insideUnitCircle.normalized;
        changeDirectionTimer = changeDirectionInterval;
    }
}
