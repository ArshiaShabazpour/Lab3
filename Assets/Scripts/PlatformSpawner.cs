using UnityEngine;

public class PlatformSpawner : MonoBehaviour, IBasicSpawner<GameObject>
{
    public GameObject platformPrefab;
    public Transform platformsParent;

    public GameObject SpawnPlatform(Vector2 pos)
    {
        if (platformPrefab == null) return null;
        return Instantiate(platformPrefab, pos, Quaternion.identity, platformsParent);
    }

    public GameObject Spawn(params object[] args)
    {
        Vector2 pos = (Vector2)args[0];
        return SpawnPlatform(pos);
    }
}
