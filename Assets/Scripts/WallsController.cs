using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WallsController : MonoBehaviour
{

    public GameObject WallObject = null;

    // Kokku palju võib korraga ekraanil seinasid olla ja palju on praegu spawnitud
    public int MAXTotalWallsSpawned = 10;
    private int TotalWallsSpawned = 0;

    // Kaamera servad
    private float minX = 0f;
    private float minY = 0f;
    private float maxX = 18f;
    private float maxY = 11f;

    private List<GameObject> walls = new List<GameObject>();

    public GameObject TestText = null;

    static public WallsController Instance;
    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        minX = Data.Camera_minX;
        minY = Data.Camera_minY - 1f;
        maxX = Data.Camera_maxX;
        maxY = Data.Camera_maxY + 1f;


        SpawnWalls();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < walls.Count - 1; i++)
        {
            if(Mathf.Abs(walls[i].transform.position.x - walls[i + 1].transform.position.x) <= 1f)
            {
                if (Mathf.Abs(walls[i].transform.position.y - walls[i + 1].transform.position.y) <= 1f)
                {
                    print(walls[i].name + " - " + walls[i + 1].name);
                }
            }
        }

        TestText.GetComponent<TextMeshProUGUI>().text = TotalWallsSpawned.ToString();
        if (Data.PlayerDead == false /*|| PlayerPrefs.GetInt("FirstTime") == 1*/)
        {
            MoveWalls();
        }
        else if (Data.PlayerDead == true && TotalWallsSpawned > 0)
        {
            DeleteAllWalls();
        }
    }

    void SpawnWalls()
    {
        StartCoroutine(SomeCoroutine());
    }

    private IEnumerator SomeCoroutine()
    {
        while(true)
        {
            if((TotalWallsSpawned < MAXTotalWallsSpawned && Data.PlayerDead == false) /*|| PlayerPrefs.GetInt("FirstTime") == 1*/)
            {
                SpawnWall();
            }
            // Spawnib koheselt 3 seina mängu alguses
            if(TotalWallsSpawned > 3) yield return new WaitForSeconds(1);
            else yield return new WaitForSeconds(0.5f); // millisekundid
        }
    }

    void SpawnWall()
    {
        //Vector2 position = new Vector2(ToNotSpawnNearToOtherWall(Random.Range(minX, maxX)), maxY);
        Vector2 position = new Vector2(GameMechanics.Instance.ToNotSpawnNearToOtherObject(Random.Range(Data.Camera_minX, Data.Camera_maxX)), Data.Camera_maxY);
        GameObject tere2 = Instantiate(WallObject, position, Quaternion.identity);
        walls.Add(tere2);

        //tere2.name = "Wall" + TotalWallsSpawned;
        //print("Wall: " + tere2.transform.position);

        TotalWallsSpawned++;
    }

    void MoveWalls()
    {
        for (int i = 0; i < TotalWallsSpawned; i++)
        {
            // Siin lisab kiirust
            walls[i].transform.Translate(Vector2.down * 5 * Time.deltaTime, Space.World);
            //MoveTowardsTarget(walls[i]);
            if (walls[i].transform.position.y <= minY)
            {
                if(TotalWallsSpawned == MAXTotalWallsSpawned)
                {
                    MoveWallToTOP(walls[i]);
                }
                else
                {
                    // 50% tõenäosus, et spawnib kohe objecti ülesse tagasi (kui kõik objectid ei ole spawnitud)
                    int spawnornot = Random.Range(0, 4);
                    //print("SPawn.. " + spawnornot);
                    if (spawnornot == 1 || spawnornot == 2 || spawnornot == 3)
                    {
                        MoveWallToTOP(walls[i]);
                    }
                    else if (spawnornot == 0)
                    {
                        DeletelWall(i);
                    }
                }
            }
        }

    }

    private void MoveTowardsTarget(GameObject go)
    {
        //the speed, in units per second, we want to move towards the target
        float speed = 5;
        //move towards the center of the world (or where ever you like)
        Vector3 targetPosition = new Vector3(go.transform.position.x, go.transform.position.y - 1f, go.transform.position.z);

        Vector3 currentPosition = go.transform.position;
        //first, check to see if we're close enough to the target
        if (Vector3.Distance(currentPosition, targetPosition) > .1f)
        {
            Vector3 directionOfTravel = targetPosition - currentPosition;
            //now normalize the direction, since we only want the direction information
            directionOfTravel.Normalize();
            //scale the movement on each axis by the directionOfTravel vector components

            go.transform.Translate(
                (directionOfTravel.x * speed * Time.deltaTime),
                (directionOfTravel.y * speed * Time.deltaTime),
                (directionOfTravel.z * speed * Time.deltaTime),
                Space.World);
        }
    }

    void MoveWallToTOP(GameObject toDelete)
    {
        //Vector2 position = new Vector2(ToNotSpawnNearToOtherWall(Random.Range(minX, maxX)), maxY);
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
            if(toDelete.transform.localScale.y >= 175) toDelete.transform.localScale = new Vector2(toDelete.transform.localScale.x, toDelete.transform.localScale.y - 25f);
        }
        else if (spawnornot == 2)
        {
            if (toDelete.transform.localScale.y <= 325) toDelete.transform.localScale = new Vector2(toDelete.transform.localScale.x, toDelete.transform.localScale.y + 25f);
        }
        else if (spawnornot == 3)
        {
            if (toDelete.transform.localScale.y <= 300) toDelete.transform.localScale = new Vector2(toDelete.transform.localScale.x, toDelete.transform.localScale.y + 50f);
        }
    }

    public void DeletelWall(int id)
    {
        Destroy(walls[id]);
        TotalWallsSpawned--;
        walls.RemoveAt(id);
    }

    public void DeleteAllWalls()
    {
        for(int i = 0; i < walls.Count; i++)
        {
            Destroy(walls[i]);        
        }
        TotalWallsSpawned = 0;
        walls.Clear();
    }

    public bool IsNearToWall(float xpos)
    {
        for (int i = 0; i < TotalWallsSpawned; i++)
        {
            if (Mathf.Abs(xpos - walls[i].transform.position.x) <= 1f)
            {
                if(Mathf.Abs(Data.Camera_maxY - walls[i].transform.position.y) <= 1f)
                {
                    return true;
                }
            }
        }
        //print("IsNearToWall: " + xpos);
        return false;
    }

    /*public float ToNotSpawnNearToOtherWall(float xpos)
    {
        for (int i = 0; i < TotalWallsSpawned; i++)
        {
            if (Mathf.Abs(xpos - walls[i].transform.position.x) <= 0.5f)
            {
                return ToNotSpawnNearToOtherWall(Random.Range(minX, maxX));
            }
        }
        return xpos;
    }*/
}
