using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMechanics : MonoBehaviour
{

    // !!!
    private bool EnableLogging = true;


    // go - GameObject
    //public GameObject goMainCamera = null;
    //public GameObject goPlayer = null;

    //public GameObject goScore = null;

    public GameObject goEndUI = null;

    //public GameObject goENDScore = null;
    //public GameObject goENDRecord = null;
    //public GameObject goENDNewRecord = null;

    // Kaamera servad
    /*public float Camera_minX = 1.4f;
    public float Camera_minY = 0.2f;
    public float Camera_maxX = 18.6f;
    public float Camera_maxY = 9.7f;*/

    //public float Player_StartX = 0f;
    //public float Player_StartY = 0f;

    static public GameMechanics Instance;
    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        print(Mathf.Abs(1.3f - (-0.7f)));
        if(Mathf.Abs(1.3f - (-0.7f)) <= 1f)
        {
            print("tere");
        }

        this.GetComponent<WallsController>().enabled = true;

        //PlayerPrefs.SetInt("GameRunning", 1);
        //PlayerPrefs.SetInt("FirstTime", 1);
        //(ScoreUpdate());

        /*PlayerPrefs.SetFloat("Camera_minX", -((Screen.width / 100f) - 0.8f));
        PlayerPrefs.SetFloat("Camera_minY", -((Screen.height / 100f) - 0.8f));
        PlayerPrefs.SetFloat("Camera_maxX", (Screen.width / 100f) - 0.8f);
        PlayerPrefs.SetFloat("Camera_maxY", (Screen.height / 100f) - 0.8f);*/

        /*PlayerPrefs.SetFloat("Camera_minX", -((Camera.main.orthographicSize * Camera.main.aspect)));
        PlayerPrefs.SetFloat("Camera_minY", -((Camera.main.orthographicSize)));
        PlayerPrefs.SetFloat("Camera_maxX", (Camera.main.orthographicSize * Camera.main.aspect));
        PlayerPrefs.SetFloat("Camera_maxY", (Camera.main.orthographicSize));*/

        Data.Camera_minX = -(Camera.main.orthographicSize * Camera.main.aspect);
        Data.Camera_minY = -(Camera.main.orthographicSize);
        Data.Camera_maxX = (Camera.main.orthographicSize * Camera.main.aspect);
        Data.Camera_maxY = (Camera.main.orthographicSize) + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (PlayerController.Instance.transform.position.x < Data.Camera_minX
            || PlayerController.Instance.transform.position.y < Data.Camera_minY
            || PlayerController.Instance.transform.position.x > Data.Camera_maxX
            || PlayerController.Instance.transform.position.y > Data.Camera_maxY)
        {
            //print("SurmX:" + goPlayer.transform.position.x);
            //print("SurmY:" + goPlayer.transform.position.y);
            Game_Over();
        }
    }

    /*private IEnumerator ScoreUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (PlayerPrefs.GetInt("GameRunning") == 1)
            {
                //print(tempScore.ToString());
                tempScore++;
                //PlayerPrefs.SetInt("tempScore", PlayerPrefs.GetInt("tempScore") + 1);
                goScore.GetComponent<TextMeshProUGUI>().text = tempScore.ToString();
            }
        }
    }*/

    public void Game_Over()
    {
        goEndUI.SetActive(true);

        this.GetComponent<TapToStart>().enabled = true;

        PlayerController.Instance.MoveToStartPosition();

        Data.PlayerDead = true;

        PlayerController.Instance.gameObject.SetActive(false);

        Score.Instance.ShowEndScore();

    }
    public float ToNotSpawnNearToOtherObject(float xpos)
    {
        if (WallsController.Instance.IsNearToWall(xpos) == true || CoinsController.Instance.IsNearToCoin(xpos) == true)
        {
            return ToNotSpawnNearToOtherObject(Random.Range(Data.Camera_minX, Data.Camera_maxX));
        }
        //print("ToNotSpawnNearToOtherObject : " + xpos);
        return xpos;
    }

    public void sLog(string text) // sLog - smartLog
    {
        if(EnableLogging == true)
        {
            print(text);
        }
    }
}
