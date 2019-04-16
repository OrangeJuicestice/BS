using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockController : MonoBehaviour
{
    private GameObject hookPrefab;
    private GameObject craneArmPrefab;
    private GameObject craneBasePrefab;
    public float rotateSpeed = 100.0f;
    bool ingredientHooked = true;
    bool readyToDrop = false;
    Rigidbody2D rigidbody;
    private GameObject _controller;
    objectController _objectController;
    private Vector3 previousEulerAngles;

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
        craneArmPrefab = GameObject.Find("CraneArmx2");
        craneBasePrefab = GameObject.Find("CraneBase");
        StartCoroutine(readyToDropIngred());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (ingredientHooked && readyToDrop)
            {
                ingredientHooked = false;
                rigidbody.freezeRotation = true;
                rigidbody.freezeRotation = false;
                hookPrefab.GetComponent<HingeJoint2D>().connectedBody = null;
                hookPrefab.GetComponent<HingeJoint2D>().enabled = false;
                StartCoroutine(fixCollision());
                rigidbody.gravityScale = 1;
                rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        else if(ingredientHooked)
        {
            transform.eulerAngles = previousEulerAngles;
            Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
            if (Input.GetKey(KeyCode.W) && !this.GetComponent<Collider2D>().IsTouching(craneArmPrefab.GetComponent<Collider2D>()) && !this.GetComponent<Collider2D>().IsTouching(craneBasePrefab.GetComponent<Collider2D>()))
            {
                rigidbody.freezeRotation = false;
                transform.eulerAngles = previousEulerAngles + new Vector3(0, 0, -rotateSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S) && !this.GetComponent<Collider2D>().IsTouching(craneArmPrefab.GetComponent<Collider2D>()) && !this.GetComponent<Collider2D>().IsTouching(craneBasePrefab.GetComponent<Collider2D>()))
            {
                rigidbody.freezeRotation = false;
                transform.eulerAngles = previousEulerAngles + new Vector3(0, 0, rotateSpeed * Time.deltaTime);
            }
            else
            { 
                rigidbody.freezeRotation = true;
            }
        }
        previousEulerAngles = this.transform.eulerAngles;
    }

    private IEnumerator fixCollision()
    {
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), false);
    }

    private IEnumerator readyToDropIngred()
    {
        yield return new WaitForSeconds(1);
        readyToDrop = true;
    }
}
