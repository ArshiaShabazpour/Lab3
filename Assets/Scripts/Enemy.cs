using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public int HP = 1;

    private Vector2 target;
    private bool alive = true;
    public bool IsAlive => alive;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            pointA = new GameObject(name + "_A").transform;
            pointB = new GameObject(name + "_B").transform;
            pointA.position = transform.position + Vector3.left * 2f;
            pointB.position = transform.position + Vector3.right * 2f;
        }
        target = pointB.position;
    }

    void Update()
    {
        if (!alive) return;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            if (Vector2.Distance(target, pointA.position) < 0.01f)
                target = pointB.position;
            else
                target = pointA.position;
        }
    }

    public void InitializePatrol(Vector2 a, Vector2 b)
    {
        if (pointA == null) pointA = new GameObject(name + "_A").transform;
        if (pointB == null) pointB = new GameObject(name + "_B").transform;
        pointA.position = a;
        pointB.position = b;
        target = pointB.position;
    }

    public void TakeDamage(int dmg)
    {
        if (!alive) return;

        HP -= dmg;
        AudioManager.Instance?.PlayEnemyHit();

        if (HP <= 0) Die();
    }

    void Die()
    {
        if (!alive) return;
        alive = false;

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        var spawner = FindObjectOfType<EnemySpawner>();
        spawner?.NotifyEnemyRemoved(this);

        GameManager.Instance.EnemyStomped();

        Destroy(gameObject, 0.25f);
    }

    public void OnStomped()
    {
        if (!alive) return;
        AudioManager.Instance?.PlayEnemyStomp();
        Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
