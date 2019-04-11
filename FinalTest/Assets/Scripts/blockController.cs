using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockController : MonoBehaviour
{
    private GameObject hookPrefab;
    public float rotateSpeed = 100.0f;
    bool drop = false;
    Rigidbody2D rigidbody;
    private GameObject _controller;
    objectController _objectController;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        rigidbody.angularVelocity = 0.0f;
        rigidbody.freezeRotation = true;
        _controller = GameObject.Find("Controller");
        _objectController = _controller.GetComponent<objectController>();
        hookPrefab = GameObject.Find("Hook");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (drop == false)
            {
                StartCoroutine(dropped());
            }
            drop = true;
        }
        else if(drop == false)
        {
            Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
            if (Input.GetKey(KeyCode.W))
            {
                rigidbody.freezeRotation = false;
                transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rigidbody.freezeRotation = false;
                transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            }
            else
            { 
                rigidbody.freezeRotation = true;
            }
        }
        
    }

    private IEnumerator dropped()
    {
        rigidbody.freezeRotation = false;
        hookPrefab.GetComponent<HingeJoint2D>().connectedBody = null;
        hookPrefab.GetComponent<HingeJoint2D>().enabled = false;
        StartCoroutine(fixCollision());
        rigidbody.gravityScale = 1;
        rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(3);
        _objectController.addIngredient();
    }

    private IEnumerator fixCollision()
    {
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), false);
    }
}
