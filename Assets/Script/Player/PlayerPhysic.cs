using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysic : MonoBehaviour
{
    public static PlayerPhysic instance;
    private Rigidbody2D rb;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
    }

    public void addForce(Vector3 f)
    {
        rb.velocity = f;
    }

}
