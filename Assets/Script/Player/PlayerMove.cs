using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Vector3 moveVector = new Vector3(0, 4f, 0);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPhysic.instance.addForce(moveVector);
        }

        if (
            Player.Instance.transform.position.y > GameManager.Instance.playerMaxY
            ||
            Player.Instance.transform.position.y < GameManager.Instance.playerMinY
            )
            GameManager.Instance.Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
            GameManager.Instance.Die();
    }
}
