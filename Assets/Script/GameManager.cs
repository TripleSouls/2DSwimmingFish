using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject backgroundObject;
    public float obstacleMinPosX = -12f;
    public float ObstacleSpeed = 3f;
    public float playerMaxY = 6.5f;
    public float playerMinY = -6.5f;
    public GameObject spawner;
    public GameObject spawnedObjects;

    public GameObject youDiedText;
    public bool canRestart = false;
    public GameObject ScoreText;
    public float score;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        youDiedText.SetActive(false);
        ScreenManager.Instance.addEventListener(PrepareScreen);
        ScreenManager.Instance.UpdateScreenSizeAndCallEvent(true); //force run
        Application.targetFrameRate = 60;
    }

    void PrepareScreen(Vector2 newSize, Vector2 oldSize)
    {
        if (backgroundObject == null)
            return;

        playerMaxY = ScreenManager.GetScreenPoses().TopLeft.y + 1f;
        playerMinY = ScreenManager.GetScreenPoses().BottomLeft.y - 1f;

        Vector2 uniteSize = Camera.main.ScreenToWorldPoint(newSize) * 2;
        backgroundObject.transform.localScale = uniteSize;
        backgroundObject.transform.position = new Vector3(0, 0, 10f);
    }

    private void Update()
    {
        if (canRestart && Input.GetKeyDown(KeyCode.Space))
        {
            score = 0;
            canRestart = false;
            Player.Instance.transform.position = new Vector3(-5f, 0, 0);
            Player.Instance.GetComponent<Rigidbody2D>().isKinematic = false;
            spawner.GetComponent<ObstacleSpawner>().StartSpawn();
            spawnedObjects.SetActive(true);
            youDiedText.SetActive(false);
        }

        if (!canRestart)
        {
            score += Time.deltaTime;
        }

        ScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString("F1") + " Score";

    }

    public void Die()
    {
        youDiedText.SetActive(true);
        Player.Instance.GetComponent<Rigidbody2D>().isKinematic = true;
        Player.Instance.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Player.Instance.transform.position = Vector3.zero;
        spawner.GetComponent<ObstacleSpawner>().StopSpawn();
        foreach(Transform g in spawnedObjects.transform)
        {
            Destroy(g.gameObject);
        }
        spawnedObjects.SetActive(false);
        canRestart = true;
    }

}
