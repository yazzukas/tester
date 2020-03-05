using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToStart : MonoBehaviour
{
    //public List<GameObject> ThingsToShow = new List<GameObject>();
    //public List<GameObject> ThingsToDelete = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            /*Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                StartGame();
            }*/
            StartGame();
        }

        if (Input.GetKeyDown("left"))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Data.PlayerDead = false;
        SceneManager.LoadScene("Main");
        //MainMenu.Instance.StartGame();

        /*for (int i = 0; i < ThingsToShow.Count; i++)
        {
            ThingsToShow[i].SetActive(true);
        }

        for (int i = 0; i < ThingsToDelete.Count; i++)
        {
            ThingsToDelete[i].SetActive(false);
        }
        this.enabled = false;*/
    }

}
