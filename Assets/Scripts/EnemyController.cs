using UnityEngine;

public enum EnemyType { BorderPatrol, RandomPatrol }

public class EnemyController : MonoBehaviour
{
    public EnemyType enemyType;
    public float moveSpeed = 2f;
    public float chaseDistance = 5f;
    public Transform player;

    // Border patrol points
    public Vector3[] borderPoints;
    private int currentPointIndex = 0;

    // Random patrol
    public Vector2 randomAreaMin = new Vector2(-10, -10);
    public Vector2 randomAreaMax = new Vector2(10, 10);
    private Vector3 randomTarget;

    private void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (enemyType == EnemyType.RandomPatrol)
        {
            SetNewRandomTarget();
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            if (enemyType == EnemyType.BorderPatrol)
            {
                PatrolBorders();
            }
            else if (enemyType == EnemyType.RandomPatrol)
            {
                PatrolRandom();
            }
        }
    }

    private void ChasePlayer()
    {
        MoveTowards(player.position);
    }

    private void PatrolBorders()
    {
        if (borderPoints == null || borderPoints.Length == 0) return;

        Vector3 targetPoint = borderPoints[currentPointIndex];
        MoveTowards(targetPoint);

        if (Vector3.Distance(transform.position, targetPoint) < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % borderPoints.Length;
        }
    }

    private void PatrolRandom()
    {
        MoveTowards(randomTarget);

        if (Vector3.Distance(transform.position, randomTarget) < 0.5f)
        {
            SetNewRandomTarget();
        }
    }

    private void SetNewRandomTarget()
    {
        float randomX = Random.Range(randomAreaMin.x, randomAreaMax.x);
        float randomZ = Random.Range(randomAreaMin.y, randomAreaMax.y);
        randomTarget = new Vector3(randomX, transform.position.y, randomZ);
    }

    private void MoveTowards(Vector3 targetPos)
    {
        var toTarget = targetPos - transform.position;
        toTarget.y = 0f;
        if (toTarget.sqrMagnitude < 0.0001f) return;

        var dir = toTarget.normalized;
        transform.position += dir * (moveSpeed * Time.deltaTime);
        transform.forward = dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            p.TakeDamage();
        }
    }
}
