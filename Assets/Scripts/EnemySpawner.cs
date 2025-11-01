using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IBasicSpawner<Enemy>
{
    [Header("Prefab")]
    public GameObject enemyPrefab;
    public Transform spawnParent;

    [Header("Area")]
    public Vector2 minSpawn = new Vector2(-50, -1);
    public Vector2 maxSpawn = new Vector2(50, 3);

    [Header("Patrol")]
    public float patrolHalfWidth = 2f;

    private readonly List<Enemy> spawned = new List<Enemy>();

    public Enemy SpawnEnemy(Vector2 center, int hp = 1)
    {
        if (enemyPrefab == null) return null;

        Vector2 pos = center;
        GameObject go = Instantiate(enemyPrefab.gameObject, pos, Quaternion.identity, spawnParent);
        var e = go.GetComponent<Enemy>();
        if (e != null)
        {
            e.HP = hp;
            e.InitializePatrol(new Vector2(pos.x - patrolHalfWidth, pos.y), new Vector2(pos.x + patrolHalfWidth, pos.y));
            spawned.Add(e);
        }
        return e;
    }

    // spawn N enemies randomly in area
    public List<Enemy> SpawnEnemies(int count)
    {
        var list = new List<Enemy>();
        for (int i = 0; i < count; i++)
        {
            var pos = new Vector2(
                Random.Range(minSpawn.x, maxSpawn.x),
                Random.Range(minSpawn.y, maxSpawn.y)
            );
            var e = SpawnEnemy(pos, 1);
            if (e != null) list.Add(e);
        }
        return list;
    }

    public int AliveEnemyCount()
    {
        int alive = 0;
        for (int i = spawned.Count - 1; i >= 0; i--)
        {
            if (spawned[i] == null) spawned.RemoveAt(i);
            else if (spawned[i].IsAlive) alive++;
        }
        return alive;
    }

    // notify removal used by Enemy on death
    public void NotifyEnemyRemoved(Enemy e)
    {
        if (spawned.Contains(e)) spawned.Remove(e);
    }

    // IBasicSpawner implementation (args: center(Vector2), hp(int))
    public Enemy Spawn(params object[] args)
    {
        Vector2 center = (Vector2)args[0];
        int hp = args.Length > 1 ? (int)args[1] : 1;
        return SpawnEnemy(center, hp);
    }
}
