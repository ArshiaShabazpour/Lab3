using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Friendly : MonoBehaviour
{
    public int healOnTouch = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (healOnTouch > 0)
        {
            GameManager.Instance?.HealPlayer(healOnTouch); 
        }

        Destroy(gameObject);
    }
}
