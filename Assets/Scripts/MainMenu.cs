using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject TutorialUI;

    public GameObject PlayerTemp;

    public List<Color> PlayerTemp_Colors = new List<Color>();

    public GameObject KnockSound;
    public GameObject YukiSound;

    private int cPlayerTemp_Colors = 0;


    static public MainMenu Instance;
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Data.PlayerColor = PlayerTemp_Colors[cPlayerTemp_Colors];
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                SinglePlayer();
            }
            else if (touch.position.x > Screen.width / 2)
            {
                MultiPlayer();
            }
            SinglePlayer();
        }*/

        if (Input.GetKeyDown("left"))
        {
            SinglePlayer();
        }
        else if (Input.GetKeyDown("right"))
        {
            SinglePlayer();
        }


        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    private void SinglePlayer()
    {
        // Kas kasutaja on vaadanud õpetust
        if(PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            MainMenuUI.SetActive(false);
            TutorialUI.SetActive(true);
            PlayerPrefs.SetInt("Tutorial", 1);
            print("tere");
        }
        else StartGame();
    }

    public void LeftSelect()
    {
        if(cPlayerTemp_Colors !=  0)
        {
            PlayerTemp.GetComponent<Image>().color = PlayerTemp_Colors[cPlayerTemp_Colors - 1];
            Data.PlayerColor = PlayerTemp_Colors[cPlayerTemp_Colors - 1];
            cPlayerTemp_Colors--;
        }
    }

    public void RightSelect()
    {
        if (cPlayerTemp_Colors != PlayerTemp_Colors.Count - 1)
        {
            PlayerTemp.GetComponent<Image>().color = PlayerTemp_Colors[cPlayerTemp_Colors + 1];
            Data.PlayerColor = PlayerTemp_Colors[cPlayerTemp_Colors + 1];
            cPlayerTemp_Colors++;
        }
    }

    public void AD()
    {
        YukiSound.GetComponent<AudioSource>().Play();
    }

    public void Leaderboard()
    {
        //KnockSound.GetComponent<AudioSource>().Play();
    }

    public void Sound()
    {
        KnockSound.GetComponent<AudioSource>().Play();
    }


    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
