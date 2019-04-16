using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSelector : MonoBehaviour
{

    [SerializeField] public GameObject cratePrefab;
    [SerializeField] public GameObject objectController;
    [SerializeField] public GameObject rightArrow;
    [SerializeField] public GameObject leftArrow;
    private List<string> ingredientList;
    private int ingredientNum;
    private int scaleValue = 0;

    void Start()
    {
        this.transform.position = new Vector3(cratePrefab.transform.position.x, cratePrefab.transform.position.y - (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 4), cratePrefab.transform.position.z - 1);
        ingredientList = objectController.GetComponent<objectController>().ingredientList;
        ingredientNum = objectController.GetComponent<objectController>().ingredientNum;
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/" + ingredientList[ingredientNum], typeof(Sprite)) as Sprite;
        fixSizing();
    }

    void Update()
    {
        rightArrow.transform.position = new Vector3(cratePrefab.transform.position.x + 2.25f, cratePrefab.transform.position.y - (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 4), cratePrefab.transform.position.z - 1);
        leftArrow.transform.position = new Vector3(cratePrefab.transform.position.x - 2.2f, cratePrefab.transform.position.y - (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 4), cratePrefab.transform.position.z - 1);
        ingredientNum = objectController.GetComponent<objectController>().ingredientNum;
        if (ingredientNum < ingredientList.Count) {
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/" + ingredientList[ingredientNum], typeof(Sprite)) as Sprite;
            this.transform.position = new Vector3(cratePrefab.transform.position.x, cratePrefab.transform.position.y - (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 4), cratePrefab.transform.position.z - 1);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = null;
        }

    }

    public void fixSizing()
    {
        scaleValue = 1;
        if (this.GetComponent<SpriteRenderer>().bounds.size.x >= this.GetComponent<SpriteRenderer>().bounds.size.y)
        {
            while (this.GetComponent<SpriteRenderer>().bounds.size.x < cratePrefab.GetComponent<SpriteRenderer>().bounds.size.x)
            {
                this.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                scaleValue++;
            }
            this.transform.localScale = new Vector3(scaleValue - 2, scaleValue - 2, scaleValue - 2);
        }
        else
        {
            while (this.GetComponent<SpriteRenderer>().bounds.size.y < (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 2))
            {
                this.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                scaleValue++;
            }
            this.transform.localScale = new Vector3(scaleValue - 2, scaleValue - 2, scaleValue - 2);
        }
    }
}
