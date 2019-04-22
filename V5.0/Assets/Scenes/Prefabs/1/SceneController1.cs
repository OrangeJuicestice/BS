using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController1 : MonoBehaviour
{
    public string nextScene;
    private int oldScreenHeight;

    public void ChangeScenes()
    {
        SceneManager.LoadScene(nextScene);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(JumpToScene());
        oldScreenHeight = Screen.currentResolution.height;
        Screen.SetResolution(Screen.currentResolution.width, (int)(Screen.currentResolution.width / (16f / 9f)), Screen.fullScreen);
        GameObject.Find("Main Camera").GetComponent<Camera>().aspect = 16f / 9f;
    }

    IEnumerator JumpToScene() {
        yield return new WaitForSeconds(10.0f);
        Screen.SetResolution(Screen.currentResolution.width, oldScreenHeight, Screen.fullScreen);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
