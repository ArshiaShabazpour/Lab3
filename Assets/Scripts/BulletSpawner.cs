using UnityEngine;

public class BulletSpawner : MonoBehaviour, IBasicSpawner<Bullet>
{
    public GameObject bulletPrefab;

    public Bullet SpawnBullet(Vector2 position, Vector2 direction, int damage = 1)
    {
        if (bulletPrefab == null) return null;
        var go = Instantiate(bulletPrefab, position, Quaternion.identity);
        var b = go.GetComponent<Bullet>();
        if (b != null) b.Initialize(direction, damage);
        return b;
    }

    public Bullet Spawn(params object[] args)
    {
        Vector2 pos = (Vector2)args[0];
        Vector2 dir = (Vector2)args[1];
        int dmg = (int)args[2];
        return SpawnBullet(pos, dir, dmg);
    }
}
