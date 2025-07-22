using UnityEngine;

public class EnemyFollowAndPatrol : MonoBehaviour
{
    public Transform player;
    public float followRange = 5f;
    public float followSpeed = 2f;
    public float patrolSpeed = 1.5f;

    public Transform leftPoint;
    public Transform rightPoint;

    private bool isGoingRight = true;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= followRange)
        {
            // Follow player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * followSpeed * Time.deltaTime;
        }
        else
        {
            // Patrol between leftPoint and rightPoint
            Patrol();
        }
    }

    void Patrol()
    {
        if (isGoingRight)
        {
            transform.position += Vector3.right * patrolSpeed * Time.deltaTime;

            if (transform.position.x >= rightPoint.position.x)
                isGoingRight = false;
        }
        else
        {
            transform.position += Vector3.left * patrolSpeed * Time.deltaTime;

            if (transform.position.x <= leftPoint.position.x)
                isGoingRight = true;
        }
    }
}