using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    [SerializeField] private GameObject armPrefab;
    public float moveSpeed = 5.0f;
    private Rigidbody2D rb2d;

    void FixedUpdate()
    {
        rb2d = armPrefab.GetComponent<Rigidbody2D>();
        if (Input.GetKey(KeyCode.D) && armPrefab.transform.position.x <= -5.781)
        {
            rb2d.MovePosition(rb2d.position + (new Vector2(0.01f, 0.0f) * moveSpeed));
        }
        else if (Input.GetKey(KeyCode.A) && armPrefab.transform.position.x >= -15.17)
        {
            rb2d.MovePosition(rb2d.position + (new Vector2(0.01f, 0.0f) * -moveSpeed));
        }

    }
}
