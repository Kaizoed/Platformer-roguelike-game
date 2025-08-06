using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;
    public float followRange = 5f;
    public float followSpeed = 2f;

    [Header("Patrol Settings")]
    public Transform leftPoint;
    public Transform rightPoint;
    public float patrolSpeed = 1.5f;

    private bool isGoingRight = true;
    private Animator animator;
    private Rigidbody2D rb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // If these were never assigned in the Inspector (because you're using a prefab),
        // find them by tag or name in the scene:
        if (player == null)
        {
            GameObject _player = GameManager.Instance.Player;
            if (_player != null) player = _player.transform;
        }

        if (leftPoint == null)
        {
            var lp = GameObject.Find("LeftLimit");
            if (lp != null) leftPoint = lp.transform;
        }

        if (rightPoint == null)
        {
            var rp = GameObject.Find("RightLimit");
            if (rp != null) rightPoint = rp.transform;
        }
    }

    void Update()
    {
        if (player == null || leftPoint == null || rightPoint == null)
            return; // missing references, so do nothing

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= followRange) FollowPlayer();
        else Patrol();
    }

    void FollowPlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(dir.x * followSpeed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        FlipSprite(rb.linearVelocity.x);
    }

    void Patrol()
    {
        float vx = isGoingRight ? patrolSpeed : -patrolSpeed;
        rb.linearVelocity = new Vector2(vx, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(vx));
        FlipSprite(vx);

        if (isGoingRight && transform.position.x >= rightPoint.position.x) isGoingRight = false;
        if (!isGoingRight && transform.position.x <= leftPoint.position.x) isGoingRight = true;
    }

    void FlipSprite(float vx)
    {
        if (vx > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (vx < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }
}
