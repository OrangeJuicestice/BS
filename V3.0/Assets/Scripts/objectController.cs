using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectController : MonoBehaviour
{
    [SerializeField] public Font textFont;
    [SerializeField] public GameObject hookPrefab;
    [SerializeField] public GameObject cratePrefab;
    [SerializeField] public GameObject ingredientSelector;
    [SerializeField] public GameObject leftArrow;
    [SerializeField] public GameObject rightArrow;
    [SerializeField] public List<string> ingredientList;
    private GameObject _ingredient;
    private GameObject creditsWindow;
    private GameObject scoreboard;
    private Matrix4x4 matrix;
    [HideInInspector] public int ingredientNum;
    private int maxIngredients;
    private int missedIngredients;
    [HideInInspector] public double crateMinX;
    [HideInInspector] public double crateMaxX;
    [HideInInspector] public double crateMinY;
    [HideInInspector] public double crateMaxY;
    private double startTime;
    private double totalTime;
    private double finalScore;
    [SerializeField] public int currentLevel;
    private string GUIMessage;
    private bool readyToSwitch = true;
    private bool sandwichNotDone = false;
    private bool ingredientFallen = true;
    private bool levelDone = false;
    private bool readyForPickup = true;

    // Start is called before the first frame update
    void Start()
    {
        hookPrefab.GetComponent<HingeJoint2D>().enabled = false;
        ingredientNum = 0;
        missedIngredients = 0;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            PlayerPrefs.SetInt("currentLevel", 1);
        }

        crateMinX = cratePrefab.transform.position.x - (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2);
        crateMaxX = cratePrefab.transform.position.x + (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2);
        crateMinY = cratePrefab.transform.position.y - (cratePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        crateMaxY = cratePrefab.transform.position.y;
        startTime = Time.fixedTime;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            creditsWindow = GameObject.Find("Credits");
            creditsWindow.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Level"))
        {
            scoreboard = GameObject.Find("Scoreboard");
            scoreboard.GetComponent<SpriteRenderer>().enabled = false;
        }
        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 2880f, Screen.height / 1800f, 1.0f));
        //currentLevel = PlayerPrefs.GetInt("currentLevel");
        currentLevel = 3;
        if (ingredientList.Count == 0)
        {
            if (currentLevel == 1)
            {
                ingredientList.Add("Tomatoes");
                ingredientList.Add("Lettuce");
                ingredientList.Add("Cheese");
                ingredientList.Add("Meat");
            } else if (currentLevel == 2)
            {
                ingredientList.Add("ChocolateIceCream");
                ingredientList.Add("MintIceCream");
                ingredientList.Add("VanillaIceCream");
                ingredientList.Add("StrawberryIceCream");
            }
            else
            {
                ingredientList.Add("Cheese");
            }
        }
        maxIngredients = ingredientList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && readyForPickup && ingredientList.Count > 0)
        {
            if (hookPrefab.transform.position.x > crateMinX && hookPrefab.transform.position.x < crateMaxX && hookPrefab.transform.position.y > crateMinY && hookPrefab.transform.position.y < crateMaxY)
            {
                readyForPickup = false;
                addIngredient();
                ingredientList.Remove(ingredientList[ingredientNum]);
                ingredientNum = 0;
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
        if (ingredientList.Count == 0 && !sandwichNotDone)
        {
            sandwichNotDone = true;
            ingredientList.Add("Top Bun");
            ingredientNum = 0;
        } else if(ingredientList.Count > 1 && ingredientList.Contains("Top Bun"))
        {
            ingredientList.Remove("Top Bun");
            sandwichNotDone = false;
        }

    }

    public void addIngredient()
    {
        if (ingredientNum < ingredientList.Count) {
            hookPrefab.GetComponent<HingeJoint2D>().enabled = true;
            _ingredient = Instantiate(Resources.Load(ingredientList[ingredientNum])) as GameObject;
            _ingredient.transform.position = new Vector3(hookPrefab.transform.position.x, hookPrefab.transform.position.y - 0.75f, -3);
            hookPrefab.GetComponent<HingeJoint2D>().connectedBody = _ingredient.GetComponent<Rigidbody2D>();
        }
    }

    private IEnumerator readyForNewIngredient()
    {
        yield return new WaitForSeconds(1);
        readyForPickup = true;
    }

    private IEnumerator switchIngredient()
    {
        readyToSwitch = false;
        yield return new WaitForSeconds(0.2f);
        readyToSwitch = true;
    }

    private IEnumerator flashArrowRight()
    {
        rightArrow.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        yield return new WaitForSeconds(0.1f);
        rightArrow.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    private IEnumerator flashArrowLeft()
    {
        leftArrow.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        yield return new WaitForSeconds(0.1f);
        leftArrow.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    private IEnumerator receiveOnlyOneFallenIngredient()
    {
        ingredientFallen = false;
        yield return new WaitForSeconds(0.5f);
        ingredientFallen = true;
    }

    private IEnumerator goToNextLevel()
    {
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }

    public void ingredientMissed(string s)
    {
        if (ingredientFallen)
        {
            StartCoroutine(receiveOnlyOneFallenIngredient());
            missedIngredients++;
            ingredientList.Add(s);
        }
    }

    public void ingredientDropped()
    {
        StartCoroutine(readyForNewIngredient());
    }

    private void OnGUI()
    {
        GUI.matrix = matrix;
        GUIStyle style = new GUIStyle();
        style.font = textFont;
        style.fontSize = (int)(70 * ((float)Screen.width / Screen.height));

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Level"))
        {
            if (!levelDone)
            {
                GUIMessage = "DROPPED INGREDIENTS: " + missedIngredients;
                GUI.Label(new Rect(20, 3, 500, 80), GUIMessage, style);

                GUIMessage = "TIME: " + (int)Time.timeSinceLevelLoad;
                GUI.Label(new Rect(2430, 3, 500, 80), GUIMessage, style);
            }
            else
            {
                style.fontSize = 120;
                style.alignment = TextAnchor.MiddleCenter;
                finalScore = Mathf.Floor((float)(((maxIngredients + missedIngredients) / maxIngredients) * totalTime));
                GUIMessage = "" + finalScore;
                GUI.Label(new Rect(1440 - 300, 900 - 80, 600, 160), GUIMessage, style);
            }
        }
    }

    public void endLevel()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Level"))
        {
            scoreboard.transform.position = new Vector3(0, 0, -8);
            scoreboard.GetComponent<SpriteRenderer>().enabled = true;
        }
        totalTime = Time.fixedTime - startTime;
        levelDone = true;
        PlayerPrefs.SetInt("currentLevel", 2);
        PlayerPrefs.SetInt("levelsUnlocked", PlayerPrefs.GetInt("currentLevel") + 1);
        StartCoroutine(goToNextLevel());
    }
}
