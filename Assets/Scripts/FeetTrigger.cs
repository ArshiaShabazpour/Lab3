using UnityEngine;

public class FeetTrigger : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnStomped();
                if (rb != null)
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 7f * 0.6f);
            }
        }
    }
}
