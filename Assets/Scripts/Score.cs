using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public GameObject ScoreText = null;

    public GameObject endScoreText = null;
    public GameObject endRecordScoreText = null;


    static public Score Instance;
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SomeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SomeCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            if (Data.PlayerDead == false)
            {
                Data.Score += 1;
                //PlayerPrefs.SetInt("tempScore", PlayerPrefs.GetInt("tempScore") + 1);
                //Player.Data.tempScore++;
                ScoreText.GetComponent<TextMeshProUGUI>().text = Data.Score.ToString();
            }
        }
    }

    public void ShowEndScore()
    {
        endScoreText.GetComponent<TextMeshProUGUI>().text = Data.Score.ToString();
        if (Data.Score <= PlayerPrefs.GetInt("Score")) endRecordScoreText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score").ToString();
        else
        {
            PlayerPrefs.SetInt("Score", Data.Score);
            endRecordScoreText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score").ToString();
        }

        Data.Score = 0;

        ScoreText.SetActive(false);
        ScoreText.GetComponent<TextMeshProUGUI>().text = "0";
    }
}
