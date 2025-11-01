using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public BulletSpawner bulletSpawner;
    public EnemySpawner enemySpawner;
    public SpikeSpawner spikeSpawner;
    public FriendlySpawner friendlySpawner;
    public PlatformSpawner platformSpawner;

    public Bullet SpawnBullet(Vector2 pos, Vector2 dir, int dmg = 1)
        => bulletSpawner != null ? bulletSpawner.SpawnBullet(pos, dir, dmg) : null;

    public Enemy SpawnEnemy(Vector2 center, int hp = 1)
        => enemySpawner != null ? enemySpawner.SpawnEnemy(center, hp) : null;

    public Spike SpawnSpike(Vector2 pos)
        => spikeSpawner != null ? spikeSpawner.SpawnSpike(pos) : null;

    public Friendly SpawnFriendly(Vector2 pos)
        => friendlySpawner != null ? friendlySpawner.SpawnFriendly(pos) : null;

    public GameObject SpawnPlatform(Vector2 pos)
        => platformSpawner != null ? platformSpawner.SpawnPlatform(pos) : null;
}
