using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    void Update()
    {
        float speed = GameManager.Instance.ObstacleSpeed;
        Vector3 nPos = transform.position;
        nPos.x -= speed;
        transform.position = Vector3.Lerp(transform.position, nPos, Time.deltaTime);

        if(transform.position.x < GameManager.Instance.obstacleMinPosX)
            Destroy(gameObject);
    }
}
