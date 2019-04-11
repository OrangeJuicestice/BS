using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectController : MonoBehaviour
{
    [SerializeField] private GameObject cheesePrefab;
    [SerializeField] private GameObject bunPrefab;
    [SerializeField] public GameObject hookPrefab;
    private GameObject _cheese;
    private GameObject _bun;
    private int ingredientNum;
    // Start is called before the first frame update
    void Start()
    {
        hookPrefab.GetComponent<HingeJoint2D>().enabled = false;
        ingredientNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addIngredient()
    {
        if (ingredientNum == 0)
        {
            hookPrefab.GetComponent<HingeJoint2D>().enabled = true;
            _cheese = Instantiate(cheesePrefab) as GameObject;
            _cheese.transform.position = new Vector3(hookPrefab.transform.position.x, hookPrefab.transform.position.y - 0.75f, -3);
            hookPrefab.GetComponent<HingeJoint2D>().connectedBody = _cheese.GetComponent<Rigidbody2D>();
        }
        else if (ingredientNum == 1)
        {
            hookPrefab.GetComponent<HingeJoint2D>().enabled = true;
            _cheese = Instantiate(cheesePrefab) as GameObject;
            _cheese.transform.position = new Vector3(hookPrefab.transform.position.x, hookPrefab.transform.position.y - 0.75f, -3);
            hookPrefab.GetComponent<HingeJoint2D>().connectedBody = _cheese.GetComponent<Rigidbody2D>();
        }
        else if (ingredientNum == 2)
        {
            hookPrefab.GetComponent<HingeJoint2D>().enabled = true;
            _cheese = Instantiate(cheesePrefab) as GameObject;
            _cheese.transform.position = new Vector3(hookPrefab.transform.position.x, hookPrefab.transform.position.y - 0.75f, -3);
            hookPrefab.GetComponent<HingeJoint2D>().connectedBody = _cheese.GetComponent<Rigidbody2D>();
        }
        else if(ingredientNum == 3)
        {
            hookPrefab.GetComponent<HingeJoint2D>().enabled = true;
            _bun = Instantiate(bunPrefab) as GameObject;
            _bun.transform.position = new Vector3(hookPrefab.transform.position.x, hookPrefab.transform.position.y - 0.75f, -3);
            hookPrefab.GetComponent<HingeJoint2D>().connectedBody = _bun.GetComponent<Rigidbody2D>();
        }
        
        ingredientNum++;
    }
}
