using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockController : MonoBehaviour
{
    private GameObject hookPrefab;
    private GameObject craneArmPrefab;
    private GameObject craneBasePrefab;
    private GameObject creditsWindow;
    public float rotateSpeed = 100.0f;
    private bool ingredientHooked = true;
    private bool readyToDrop = false;
    private Rigidbody2D rigidbody;
    private GameObject _controller;
    private objectController _objectController;
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
        craneArmPrefab = GameObject.Find("CraneArm");
        craneBasePrefab = GameObject.Find("CraneBase");
        creditsWindow = GameObject.Find("Credits");
        StartCoroutine(readyToDropIngred());
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            creditsWindow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (ingredientHooked && readyToDrop && (hookPrefab.transform.position.x < _objectController.crateMinX || hookPrefab.transform.position.x > _objectController.crateMaxX || hookPrefab.transform.position.y < _objectController.crateMinY || hookPrefab.transform.position.y > _objectController.crateMaxY))
            {
                ingredientHooked = false;
                rigidbody.freezeRotation = true;
                rigidbody.freezeRotation = false;
                hookPrefab.GetComponent<HingeJoint2D>().connectedBody = null;
                hookPrefab.GetComponent<HingeJoint2D>().enabled = false;
                StartCoroutine(fixCollision());
                rigidbody.gravityScale = 1;
                rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                _objectController.ingredientDropped();
            }
        }
        else if (ingredientHooked)
        {
            transform.eulerAngles = previousEulerAngles;
            if (this.GetComponent<BoxCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<BoxCollider2D>(), true);
            }
            if (this.GetComponent<CircleCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<CircleCollider2D>(), true);
            }
            if (this.GetComponent<CapsuleCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<CapsuleCollider2D>(), true);
            }
            if (this.GetComponent<PolygonCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<PolygonCollider2D>(), true);
            }
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
        if (Input.GetKey(KeyCode.Escape))
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Menu"))
            {
                if (creditsWindow.GetComponent<SpriteRenderer>().enabled)
                {
                    creditsWindow.GetComponent<SpriteRenderer>().enabled = false;
                    _objectController.GetComponent<objectController>().ingredientMissed(this.name.Substring(0, this.name.IndexOf('(')));
                    Destroy(this.gameObject);
                }
            }
        }
        if (_objectController.levelDone)
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        previousEulerAngles = this.transform.eulerAngles;
    }

    private IEnumerator fixCollision()
    {
        yield return new WaitForSeconds(1.5f);
        if (this.GetComponent<BoxCollider2D>() != null)
        {
            Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<BoxCollider2D>(), false);
        }
        if (this.GetComponent<CircleCollider2D>() != null)
        {
            Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<CircleCollider2D>(), false);
        }
        if (this.GetComponent<CapsuleCollider2D>() != null)
        {
            Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<CapsuleCollider2D>(), false);
        }
        if (this.GetComponent<PolygonCollider2D>() != null)
        {
            Physics2D.IgnoreCollision(hookPrefab.GetComponent<Collider2D>(), this.GetComponent<PolygonCollider2D>(), false);
        }
    }

    private IEnumerator readyToDropIngred()
    {
        yield return new WaitForSeconds(1);
        readyToDrop = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (this.name.Equals("Top Bun(Clone)"))
        {
            if (!col.gameObject.name.Equals("CraneBase") && !col.gameObject.name.Equals("CraneArm") && !col.gameObject.name.Equals("Hook"))
            {
                _objectController.endLevel(col.gameObject.name.Substring(0, col.gameObject.name.IndexOf('(')));
            }
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Menu"))
        {

            if (col.gameObject.name.Equals("Building"))
            {
                if (this.name.Equals("Play Bun(Clone)"))
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Level1Cutscene");
                }
                else if (this.name.Equals("Levels Bun(Clone)"))
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
                }
                else if (this.name.Equals("Credits Bun(Clone)"))
                {
                    this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    creditsWindow.transform.position = new Vector3(0, 0, -10);
                    creditsWindow.GetComponent<SpriteRenderer>().enabled = true;
                }
                else if (this.name.Equals("Exit Bun(Clone)"))
                {
                    Application.Quit();
                }
            }
        }
    }
}
