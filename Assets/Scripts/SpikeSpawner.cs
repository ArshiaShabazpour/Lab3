using UnityEngine;

public class SpikeSpawner : MonoBehaviour, IBasicSpawner<Spike>
{
    public GameObject spikePrefab;
    public Transform spikesParent;

    public Spike SpawnSpike(Vector2 pos)
    {
        if (spikePrefab == null) return null;
        var go = Instantiate(spikePrefab, pos, Quaternion.identity, spikesParent);
        return go.GetComponent<Spike>();
    }

    public Spike Spawn(params object[] args)
    {
        Vector2 pos = (Vector2)args[0];
        return SpawnSpike(pos);
    }

}
