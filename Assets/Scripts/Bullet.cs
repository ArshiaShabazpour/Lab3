using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 3f;
    public int damage = 1;
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(Vector2 dir, int dmg = 1)
    {
        direction = dir.normalized;
        damage = dmg;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Friendly"))
        {
            Debug.Log("Shot a friendly!");
            GameManager.Instance?.DamagePlayer(1); 
            var friendly = other.GetComponent<Friendly>();
            if (friendly != null)
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Spike") || other.CompareTag("Wall") || other.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
