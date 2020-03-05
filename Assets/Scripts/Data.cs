using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    private static Color playercolor;
    public static Color PlayerColor { get { return playercolor; } set { playercolor = value; } }

    private static bool dead = false;
    public static bool PlayerDead { get { return dead; } set { dead = value; } }

    private static int score = 0;
    public static int Score { get { return score; } set { score = value; } }

    private static int coins = 0;
    public static int Coins { get { return coins; } set { coins = value; } }

    private static float camera_minX = 0f;
    public static float Camera_minX { get { return camera_minX; } set { camera_minX = value; } }

    private static float camera_minY = 0f;
    public static float Camera_minY { get { return camera_minY; } set { camera_minY = value; } }

    private static float camera_maxX = 0f;
    public static float Camera_maxX { get { return camera_maxX; } set { camera_maxX = value; } }

    private static float camera_maxY = 0f;
    public static float Camera_maxY { get { return camera_maxY; } set { camera_maxY = value; } }

}
