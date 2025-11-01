using UnityEngine;

public class FriendlySpawner : MonoBehaviour, IBasicSpawner<Friendly>
{
    public GameObject friendlyPrefab;
    public Transform friendlyParent;

    public Friendly SpawnFriendly(Vector2 pos)
    {
        if (friendlyPrefab == null) return null;
        var go = Instantiate(friendlyPrefab, pos, Quaternion.identity, friendlyParent);
        return go.GetComponent<Friendly>();
    }

    public Friendly Spawn(params object[] args)
    {
        Vector2 pos = (Vector2)args[0];
        return SpawnFriendly(pos);
    }
}
