using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsController : MonoBehaviour
{

    public GameObject CoinObject;
    public GameObject CoinsText;

    public int MaxCoins = 1;

    public List<Sprite> RandomSprites;

    private List<GameObject> coins = new List<GameObject>();

    private int CoinsCount = 0;


    static public CoinsController Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    int last_maxcoins_update = 0;

    // Update is called once per frame
    void Update()
    {
        CoinsText.GetComponent<TextMeshProUGUI>().text = Data.Coins.ToString();

        if (CoinsCount < MaxCoins && Data.PlayerDead == false && Data.Score > 5)
        {
            int spawnornot = Random.Range(0, 50);
            if (spawnornot == 0)
            {
                SpawnCoin();
            }
        }
        else if (Data.PlayerDead == true && CoinsCount > 0)
        {
            DeleteAllCoins();

            CoinsText.SetActive(false);
        }

        if (Data.Score == last_maxcoins_update + 10)
        {
            last_maxcoins_update = Data.Score;
            MaxCoins++;
        }
        /*if(CoinsCount < MaxCoins)
        {
            Vector2 position = new Vector2(WallsController.Instance.ToNotSpawnNearToOtherWall(Random.Range(Data.Camera_minX, Data.Camera_maxX)), Data.Camera_maxY);
            GameObject tere2 = Instantiate(CoinObject, position, Quaternion.identity);
            coins.Add(tere2);

            CoinsCount++;
        }*/

        for (int i = 0; i < CoinsCount; i++)
        {
            // Siin lisab kiirust
            coins[i].transform.Translate(Vector2.down * 5 * Time.deltaTime);
            if (coins[i].transform.position.y <= Data.Camera_minY)
            {
                DeleteCoin(i);
            }
        }
    }

    void SpawnCoin()
    {
        Vector2 position = new Vector2(GameMechanics.Instance.ToNotSpawnNearToOtherObject(Random.Range(Data.Camera_minX, Data.Camera_maxX)), Data.Camera_maxY);
        GameObject coin = Instantiate(CoinObject, position, Quaternion.identity);
        coins.Add(coin);

        int i = Random.Range(0, RandomSprites.Count);
        coin.GetComponent<SpriteRenderer>().sprite = RandomSprites[i];

        //coin.name = "Coin" + CoinsCount;
        //print("Coin: " + coin.transform.position);

        CoinsCount++;
    }

    void MoveWallToTOP(GameObject toDelete)
    {
        Vector2 position = new Vector2(GameMechanics.Instance.ToNotSpawnNearToOtherObject(Random.Range(Data.Camera_minX, Data.Camera_maxX)), Data.Camera_maxY);
        toDelete.transform.position = position;
        // Teeb seina random pikkuseks
        int spawnornot = Random.Range(0, 5);
        //print("SPawn.. " + spawnornot);
        if (spawnornot == 0)
        {
            if (toDelete.transform.localScale.y >= 200) toDelete.transform.localScale = new Vector2(toDelete.transform.localScale.x, toDelete.transform.localScale.y - 50f);
        }
        else if (spawnornot == 1)
        {
            if (toDelete.transform.localScale.y >= 175) toDelete.transform.localScale = new Vector2(toDelete.transform.localScale.x, toDelete.transform.localScale.y - 25f);
        }
        else if (spawnornot == 2)
        {
            if (toDelete.transform.localScale.y <= 225) toDelete.transform.localScale = new Vector2(toDelete.transform.localScale.x, toDelete.transform.localScale.y + 25f);
        }
        else if (spawnornot == 3)
        {
            if (toDelete.transform.localScale.y <= 200) toDelete.transform.localScale = new Vector2(toDelete.transform.localScale.x, toDelete.transform.localScale.y + 50f);
        }
    }

    public void DeleteCoin_ByObject(GameObject coin)
    {
        Destroy(coin);
        CoinsCount--;
        coins.Remove(coin);
    }

    public void DeleteCoin(int id)
    {
        Destroy(coins[id]);
        CoinsCount--;
        coins.RemoveAt(id);
    }

    public void DeleteAllCoins()
    {
        for (int i = 0; i < coins.Count; i++)
        {
            Destroy(coins[i]);
        }
        CoinsCount = 0;
        coins.Clear();
    }

    public bool IsNearToCoin(float xpos)
    {
        for (int i = 0; i < CoinsCount; i++)
        {
            if (Mathf.Abs(xpos - coins[i].transform.position.x) <= 1f)
            {
                if (Mathf.Abs(Data.Camera_maxY - coins[i].transform.position.y) <= 1f)
                {
                    return true;
                }
            }
        }
        //print("IsNearToCoin: " + xpos);
        return false;
    }
}
