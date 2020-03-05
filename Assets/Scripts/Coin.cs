using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            //MainCamera.GetComponent<GameMechanics>().Game_Over();
            Data.Coins++;
            CoinsController.Instance.DeleteCoin(this.gameObject);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            //MainCamera.GetComponent<GameMechanics>().Game_Over();
            GameObject.Find("CoinPickup_Sound").GetComponent<AudioSource>().Play();
            Data.Coins++;
            CoinsController.Instance.DeleteCoin_ByObject(this.gameObject);
        }
    }
}
