using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingDeterminer : MonoBehaviour
{
    private bool rank5;
    private bool rank4;
    private bool rank3;
    private bool rank2;
    private bool rank1;
    private int rank5Score;
    private int rank4Score;
    private int rank3Score;
    private int rank2Score;
    private int rank1Score;

    // Start is called before the first frame update
    void Start()
    {
        rank5Score = 60;
        rank4Score = 180;
        rank3Score = 240;
        rank2Score = 300;

        rank5 = PlayerPrefs.GetInt("level1Score") <= rank5Score && PlayerPrefs.GetInt("level2Score") <= rank5Score && PlayerPrefs.GetInt("level3Score") <= rank5Score && PlayerPrefs.GetInt("level4Score") <= rank5Score && PlayerPrefs.GetInt("level5Score") <= rank5Score && PlayerPrefs.GetInt("level6Score") <= rank5Score && PlayerPrefs.GetInt("level7Score") <= rank5Score && PlayerPrefs.GetInt("level8Score") <= rank5Score && PlayerPrefs.GetInt("level9Score") <= rank5Score && PlayerPrefs.GetInt("level10Score") <= rank5Score;
        rank4 = PlayerPrefs.GetInt("level1Score") <= rank4Score && PlayerPrefs.GetInt("level2Score") <= rank4Score && PlayerPrefs.GetInt("level3Score") <= rank4Score && PlayerPrefs.GetInt("level4Score") <= rank4Score && PlayerPrefs.GetInt("level5Score") <= rank4Score && PlayerPrefs.GetInt("level6Score") <= rank4Score && PlayerPrefs.GetInt("level7Score") <= rank4Score && PlayerPrefs.GetInt("level8Score") <= rank4Score && PlayerPrefs.GetInt("level9Score") <= rank4Score && PlayerPrefs.GetInt("level10Score") <= rank4Score;
        rank3 = PlayerPrefs.GetInt("level1Score") <= rank3Score && PlayerPrefs.GetInt("level2Score") <= rank3Score && PlayerPrefs.GetInt("level3Score") <= rank3Score && PlayerPrefs.GetInt("level4Score") <= rank3Score && PlayerPrefs.GetInt("level5Score") <= rank3Score && PlayerPrefs.GetInt("level6Score") <= rank3Score && PlayerPrefs.GetInt("level7Score") <= rank3Score && PlayerPrefs.GetInt("level8Score") <= rank3Score && PlayerPrefs.GetInt("level9Score") <= rank3Score && PlayerPrefs.GetInt("level10Score") <= rank3Score;
        rank3 = PlayerPrefs.GetInt("level1Score") <= rank2Score && PlayerPrefs.GetInt("level2Score") <= rank2Score && PlayerPrefs.GetInt("level3Score") <= rank2Score && PlayerPrefs.GetInt("level4Score") <= rank2Score && PlayerPrefs.GetInt("level5Score") <= rank2Score && PlayerPrefs.GetInt("level6Score") <= rank2Score && PlayerPrefs.GetInt("level7Score") <= rank2Score && PlayerPrefs.GetInt("level8Score") <= rank2Score && PlayerPrefs.GetInt("level9Score") <= rank2Score && PlayerPrefs.GetInt("level10Score") <= rank2Score;
        rank1 = PlayerPrefs.GetInt("level1Score") > rank2Score && PlayerPrefs.GetInt("level2Score") > rank2Score && PlayerPrefs.GetInt("level3Score") > rank2Score && PlayerPrefs.GetInt("level4Score") > rank2Score && PlayerPrefs.GetInt("level5Score") > rank2Score && PlayerPrefs.GetInt("level6Score") > rank2Score && PlayerPrefs.GetInt("level7Score") > rank2Score && PlayerPrefs.GetInt("level8Score") > rank2Score && PlayerPrefs.GetInt("level9Score") > rank2Score && PlayerPrefs.GetInt("level10Score") > rank2Score;

        if (rank1)
        {
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Review1", typeof(Sprite)) as Sprite;
        } else if (rank2)
        {
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Review2", typeof(Sprite)) as Sprite;
        }
        else if (rank3)
        {
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Review3", typeof(Sprite)) as Sprite;
        }
        else if (rank4)
        {
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Review4", typeof(Sprite)) as Sprite;
        }
        else if (rank5)
        {
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Review5", typeof(Sprite)) as Sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
}
