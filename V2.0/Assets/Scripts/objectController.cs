using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectController : MonoBehaviour
{
    [SerializeField] public GameObject hookPrefab;
    [SerializeField] public GameObject cratePrefab;
    [SerializeField] public GameObject ingredientSelector;
    [SerializeField] public GameObject leftArrow;
    [SerializeField] public GameObject rightArrow;
    [SerializeField] public List<string> ingredientList;
    private GameObject _ingredient;
    public int ingredientNum;
    private double crateMinX;
    private double crateMaxX;
    private double crateMinY;
    private double crateMaxY;
    bool ingredientHooked;
    bool readyToSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        hookPrefab.GetComponent<HingeJoint2D>().enabled = false;
        ingredientNum = 0;
        ingredientHooked = false;

        crateMinX = cratePrefab.transform.position.x - (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2);
        crateMaxX = cratePrefab.transform.position.x + (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2);
        crateMinY = cratePrefab.transform.position.y - (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        crateMaxY = cratePrefab.transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!ingredientHooked && hookPrefab.transform.position.x > crateMinX && hookPrefab.transform.position.x < crateMaxX && hookPrefab.transform.position.y > crateMinY && hookPrefab.transform.position.y < crateMaxY)
            {
                addIngredient();
                ingredientList.Remove(ingredientList[ingredientNum]);
                ingredientNum = 0;
                ingredientHooked = true;
            }
            else if(ingredientHooked)
            {
                StartCoroutine(nextIngredAvailable());
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) && readyToSwitch && ingredientList.Count > 0)
        {
            StartCoroutine(flashArrowRight());
            if (ingredientNum == ingredientList.Count - 1)
            {
                ingredientNum = 0;
                StartCoroutine(switchIngredient());
            }
            else
            {
                ingredientNum++;
                StartCoroutine(switchIngredient());
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && readyToSwitch && ingredientList.Count > 0)
        {
            StartCoroutine(flashArrowLeft());
            if (ingredientNum == 0)
            {
                ingredientNum = ingredientList.Count - 1;
                StartCoroutine(switchIngredient());
            }
            else
            {
                ingredientNum--;
                StartCoroutine(switchIngredient());
            }
        }
    }

    public void addIngredient()
    {
        if (ingredientNum < ingredientList.Count) {
            hookPrefab.GetComponent<HingeJoint2D>().enabled = true;
            _ingredient = Instantiate(Resources.Load(ingredientList[ingredientNum])) as GameObject;
            _ingredient.transform.position = new Vector3(hookPrefab.transform.position.x, hookPrefab.transform.position.y - 0.75f, -3);
            hookPrefab.GetComponent<HingeJoint2D>().connectedBody = _ingredient.GetComponent<Rigidbody2D>();
            ingredientSelector.GetComponent<IngredientSelector>().fixSizing();
        }
    }

    private IEnumerator nextIngredAvailable()
    {
        yield return new WaitForSeconds(1);
        ingredientHooked = false;
    }

    private IEnumerator switchIngredient()
    {
        readyToSwitch = false;
        yield return new WaitForSeconds(0.5f);
        readyToSwitch = true;
    }

    private IEnumerator flashArrowRight()
    {
        rightArrow.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        yield return new WaitForSeconds(0.1f);
        rightArrow.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    private IEnumerator flashArrowLeft()
    {
        leftArrow.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        yield return new WaitForSeconds(0.1f);
        leftArrow.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }
}
