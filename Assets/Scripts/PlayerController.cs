using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float SpeedUpOrDown = 3f; // Positive UP | Negative DOWN
    public float SpeedRightOrLeft = 3f; // Positive RIGHT | Negative LEFT

    public GameObject MainCamera = null;

    public float Player_StartX = 0f;
    public float Player_StartY = 0f;

    public AudioClip Jump_Sound;
    public AudioClip Death_Sound;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    static public PlayerController Instance;

    public void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = Data.PlayerColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2 && touch.position.x > Screen.width / 2)
            {
                    MovePlayerUp();
            }
            else if (touch.position.x < Screen.width / 2)
            {
                    MovePlayerLeft();
            }
            else if (touch.position.x > Screen.width / 2)
            {
                    MovePlayerRight();
            }
        }
        if (Input.GetKeyDown("left") && Input.GetKeyDown("right"))
        {
            MovePlayerUp();
        }
        else if(Input.GetKeyDown("left"))
        {
            MovePlayerLeft();
        }

        else if(Input.GetKeyDown("right"))
        {
            MovePlayerRight();
        }
    }

    private void MovePlayerLeft()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(-SpeedRightOrLeft, SpeedUpOrDown);
        //this.transform.Translate(Vector2.down * 5 * Time.deltaTime);

        Play_JumpSound();
    }

    private void MovePlayerRight()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(SpeedRightOrLeft, SpeedUpOrDown);
        Play_JumpSound();
    }

    private void MovePlayerUp()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, SpeedUpOrDown*1.5f);
        Play_JumpSound();
    }

    public void MoveToStartPosition()
    {
        Vector2 position = new Vector2(Player_StartX, Player_StartY);
        this.transform.position = position;
    }

    private void Play_JumpSound()
    {
        this.GetComponent<AudioSource>().clip = Jump_Sound;
        this.GetComponent<AudioSource>().Play();
    }

    private void Play_DeathSound()
    {
        this.GetComponent<AudioSource>().clip = Death_Sound;
        this.GetComponent<AudioSource>().Play();
    }

    // OnCollisionEnter2D
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Spawn_Wall" || coll.gameObject.name == "Wall(Clone)" || coll.gameObject.name == "Wall Variant(Clone)")
        {
            Play_DeathSound();
            //MainCamera.GetComponent<GameMechanics>().Game_Over();
            GameObject.Find("Main Camera").GetComponent<GameMechanics>().Game_Over();
            print("SEINA COLLISION!");
        }
    }
    
}
