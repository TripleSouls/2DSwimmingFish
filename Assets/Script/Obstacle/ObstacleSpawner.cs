using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject bigPrefab;
    public GameObject smallPrefab;
    public GameObject parentObject;
    public Vector3 topSpawnPoint;
    public Vector3 bottomSpawnPoint;
    [SerializeField][Range(0.1f, 2f)] public float spawnTimeMax = 1f;
    [SerializeField][Range(0.1f, 2f)] public float spawnTimeMin = 1f;

    private float timer = 0f;
    private bool CanISpawn = true;

    void Start()
    {

        topSpawnPoint = ScreenManager.GetScreenPoses().TopRight;
        bottomSpawnPoint = ScreenManager.GetScreenPoses().BottomRight;
        topSpawnPoint.x += 1.5f;
        bottomSpawnPoint.x += 1.5f;
        StartSpawn();
    }

    public void StopSpawn()
    {
        StopCoroutine("Spawner");
    }

    public void StartSpawn()
    {
        StartCoroutine("Spawner");
    }

    IEnumerator Spawner()
    {
        float spawnTime = 0;
        while (true)
        {
            spawnTimeMin = (spawnTimeMin < spawnTimeMax) ? spawnTimeMax : spawnTimeMin;

            if (CanISpawn)
            {
                SpawnNewObstacles();
                timer = 0;
                CanISpawn = false;
                spawnTime = Random.RandomRange(spawnTimeMin, spawnTimeMax);
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= spawnTime)
                    CanISpawn = true;
            }

            yield return null;
        }
    }

    private void SpawnNewObstacles()
    {
        /*
         * Spawn Rule
         * bottom == big || top == big -> bottom != top
         */
        bool topIsBig = Random.RandomRange(0f, 2f) > 1 ? true : false;
        bool bottomIsBig = (topIsBig) ? false : Random.RandomRange(0f, 2f) > 1 ? true : false;

        GameObject top = Instantiate((topIsBig ? bigPrefab : smallPrefab), topSpawnPoint, Quaternion.Euler(0, 0, 180), parentObject.transform);
        GameObject bottom = Instantiate((bottomIsBig ? bigPrefab : smallPrefab), bottomSpawnPoint, Quaternion.identity, parentObject.transform);

    }
}
