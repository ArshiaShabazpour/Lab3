using UnityEngine;
using System.Collections.Generic;


public class LevelGenerator : MonoBehaviour
{
    public float minX = -1f;
    public float maxX = 188f;
    public float minY = -4f;
    public float maxY = 4f;

    public int platformCount = 12;
    public int spikeCount = 20;
    public int enemyCount = 8;
    public int friendlyCount = 3;

    public float patrolHalfWidth = 2f;

    public float minDistanceBetweenObjects = 2f;

    public float maxVerticalGap = 2f; 

    void Start()
    {
        SpawnPlatforms();
        SpawnSpikes();
        SpawnEnemies();
        SpawnFriendlies();
    }


    void SpawnPlatforms()
    {
        List<Vector2> spawned = new List<Vector2>();
        float segmentWidth = (maxX - minX) / platformCount;

        float lastY = 0f;
        for (int i = 0; i < platformCount; i++)
        {
            float x = minX + segmentWidth * i + Random.Range(0.2f, segmentWidth - 0.2f);

            float yMin = Mathf.Max(minY + 1f, lastY - maxVerticalGap);
            float yMax = Mathf.Min(maxY - 1f, lastY + maxVerticalGap);
            float y = Random.Range(yMin, yMax);

            Vector2 pos = new Vector2(x, y);
            SpawnManager.Instance?.SpawnPlatform(pos);
            spawned.Add(pos);
            lastY = y;
        }
    }


    void SpawnSpikes()
    {
        float segmentWidth = (maxX - minX) / spikeCount;

        for (int i = 0; i < spikeCount; i++)
        {
            float x = minX + segmentWidth * i + Random.Range(0.1f, segmentWidth - 0.1f);
            float y;
            float yOffset;

            if (Random.value < 0.5f) 
            {
                y = minY + 0.5f;
                yOffset = -2f;
            }
            else 
            {
                y = maxY - 0.5f;
                yOffset = 2f; 
            }

            Vector2 pos = new Vector2(x, y + yOffset);

            Spike spikeComp = SpawnManager.Instance?.SpawnSpike(pos);

            if (spikeComp != null && y > 0)
            {
                spikeComp.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f); 
            }
        }
    }




    void SpawnEnemies()
    {
        float segmentWidth = (maxX - minX) / enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            float x = minX + segmentWidth * i + Random.Range(0.5f, segmentWidth - 0.5f);
            float y = Random.Range(minY + 1f, maxY - 1f);

            Vector2 pos = new Vector2(x, y);
            SpawnManager.Instance?.SpawnEnemy(pos, Mathf.RoundToInt(patrolHalfWidth));
        }
    }


    void SpawnFriendlies()
    {
        float segmentWidth = (maxX - minX) / friendlyCount;

        for (int i = 0; i < friendlyCount; i++)
        {
            float x = minX + segmentWidth * i + Random.Range(0.5f, segmentWidth - 0.5f);
            float y = Random.Range(minY + 1f, maxY - 1f);

            Vector2 pos = new Vector2(x, y);
            SpawnManager.Instance?.SpawnFriendly(pos);
        }
    }
}
