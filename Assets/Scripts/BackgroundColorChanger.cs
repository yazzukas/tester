using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    public Color BaseColor = Color.red;
    private Color BaseColor2;

    public List<Color> Colors = new List<Color>();

    public float duration = 10.0F;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        this.GetComponent<Camera>().backgroundColor = BaseColor;

        int randomcolorrange = Random.Range(0, Colors.Count);
        BaseColor2 = Colors[randomcolorrange];
    }

    // Update is called once per frame
    void Update()
    {

        float t = Mathf.PingPong(Time.time, duration) / duration;
        this.GetComponent<Camera>().backgroundColor = Color.Lerp(BaseColor, BaseColor2, t);
        // Color changer - igakord kui jõuab algusesse tagasi vahetab teise värvi ära
        if (t < 0.001f)
        {
            int randomcolorrange = Random.Range(0, Colors.Count);
            BaseColor2 = Colors[randomcolorrange];
        }
    }
}
