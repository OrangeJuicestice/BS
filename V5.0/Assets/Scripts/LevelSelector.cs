using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    private GameObject cam;
    private GameObject musicSource;
    private GameObject soundSource;
    private Matrix4x4 matrix;
    private int selectedLevel;
    private int currentlySelectedLevel;
    private bool readyForInput = true;
    private bool gotMouse = false;
    private bool levelSelected = false;
    private string GUIMessage;

    void Start()
    {
        if (PlayerPrefs.GetInt("levelsUnlocked") >= int.Parse(this.name))
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        cam = GameObject.Find("Main Camera");
        soundSource = GameObject.Find("Sound Source");
        musicSource = GameObject.Find("Music Source");
        musicSource.GetComponent<AudioSource>().time = PlayerPrefs.GetFloat("songPosition");
        matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 2880f, Screen.height / 1800f, 1.0f));
    }

    void Update()
    {
        if (readyForInput)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                soundSource.GetComponent<AudioSource>().volume = 0.03f;
                soundSource.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Blip_Select");
                soundSource.GetComponent<AudioSource>().Play();
                if (currentlySelectedLevel == PlayerPrefs.GetInt("levelsUnlocked"))
                {
                    currentlySelectedLevel = 1;
                }
                else
                {
                    currentlySelectedLevel++;
                }
                StartCoroutine(slowDownInput());
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                soundSource.GetComponent<AudioSource>().volume = 0.03f;
                soundSource.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Blip_Select");
                soundSource.GetComponent<AudioSource>().Play();
                if (currentlySelectedLevel == 1 || currentlySelectedLevel == 0)
                {
                    currentlySelectedLevel = PlayerPrefs.GetInt("levelsUnlocked");
                }
                else
                {
                    currentlySelectedLevel--;
                }
                StartCoroutine(slowDownInput());
            }
        }
        if (int.Parse(this.name) == currentlySelectedLevel)
        {
            if (!levelSelected)
            {
                this.transform.localScale = new Vector3(11, 11, 11);
            }
            if (!levelSelected && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return)))
            {
                levelSelected = true;
                this.transform.localScale = new Vector3(10, 10, 10);
                selectedLevel = int.Parse(this.name);
                StartCoroutine(startLevel());
            }
        }
        else
        {
            this.transform.localScale = new Vector3(10, 10, 10);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
        PlayerPrefs.SetFloat("songPosition", musicSource.GetComponent<AudioSource>().time);
    }

    private IEnumerator startLevel()
    {
        soundSource.GetComponent<AudioSource>().volume = 0.1f;
        soundSource.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Randomize");
        soundSource.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f);
        this.transform.localScale = new Vector3(11, 11, 11);
        yield return new WaitForSeconds(0.05f);
        PlayerPrefs.SetInt("currentLevel", selectedLevel);
        cam.GetComponent<Camera>().aspect = 16f / 9f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + selectedLevel + "Cutscene");
    }

    private IEnumerator slowDownInput()
    {
        readyForInput = false;
        yield return new WaitForSeconds(0.15f);
        readyForInput = true;
    }

    private void OnGUI()
    {
        GUI.matrix = matrix;
        GUIStyle style = new GUIStyle();
        style.font = (Font)Resources.Load("Impact");
        style.fontSize = (int)(60 * ((float)Screen.width / Screen.height));
        style.alignment = TextAnchor.MiddleCenter;

        if (PlayerPrefs.GetInt("levelsUnlocked") == currentlySelectedLevel) 
        {
            GUIMessage = "SCORE: N/A";
        }
        else
        {
            GUIMessage = "SCORE: " + PlayerPrefs.GetInt("level" + currentlySelectedLevel + "Score");
        }
        GUI.Label(new Rect(1440, 1550, 30, 50), GUIMessage, style);
    }
}
