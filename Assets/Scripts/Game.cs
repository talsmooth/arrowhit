using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public Level[] levels = new Level[0];

    public int levelNumber;

    public static int LevelNumber;

    public int nagla;

    public float waitSecondsBeforeLevel;

    public bool restartLevelWhenLose;

    public float fruitSize;

    public int fruitSlideSpeed;

    public static float _fruitSlideSpeed;

    public bool arrowOnOff;

    public static bool _arrowOnOff;

    public int arrowForce;

    public static int _arrowForce;

    public float arrowPercentageDecreasing;

    public static float losingVelocity;

    public int arrowDecreasingForHowManyFrames;

    public static float losingVelocityLength;

    public static float _arrVelocityFruit;

    public float arrVelocityFruit;

    //public float camMaxSpeed;

    public static float _camMaxSpeed;

    //public float camEase;

    public static float _camIncreasingSpeed;

    //public float camIncreasingSpeed;

    public static float _camEase;

    public GameObject trainer;

    public GameObject text3d;

    public static GameObject text3D;

    public Collider floor;

    public GameObject stairs;

    public GameObject stairs1;

    public bool stairsBool;

    public static float floorDelta = 3.3f;

    public static bool textB;

    static public int l;

    GameObject prefab; 

    public static int hits = 0;

    public static int arrows;

    public static int money = 0;

    public GUIStyle style;

    public static bool gameOver;

    bool fixedFrameCount;

    int frameCount;

    public float time;

    // Use this for initialization
    void Start () 
    {
        text3D = text3d;

        text3D = GameObject.Find("guiText");

        LevelNumber = levelNumber;

        arrows = 1;

        _arrVelocityFruit = arrVelocityFruit;

        _fruitSlideSpeed = 600 - fruitSlideSpeed;

        floor.enabled = restartLevelWhenLose;

        _arrowOnOff = arrowOnOff;

        gameOver = false;

        hits = 0;

        money = 0;

        l = levelNumber - 1;

        Player.nextHeight = 2.52f;

        Trainer.nextHeight = 6.02f;

        Trainer.throwB = false;

        Trainer.climb = false;

        Trainer.turn = false;

        Player.climb = false;

        Player.turn = false;

        ///////////////////////////////

        _camIncreasingSpeed = 15;     //100 - camIncreasingSpeed; 

        _camEase = 0.013f;                 //camEase;

        _camMaxSpeed = 0.037f;               //camMaxSpeed;


        ////////////////////////////

        _arrowForce = arrowForce;

        losingVelocity = (100 - arrowPercentageDecreasing) /100;

        losingVelocityLength = (1-losingVelocity) / arrowDecreasingForHowManyFrames;

        StartCoroutine(Loop());
    }

    void Update () 
    {
        Time.timeScale = time;
    }



    IEnumerator Loop ()
    {
        while (l < levels.Length)
        {

            Text("Level " + (l+1) + "\n Go!", 4);

            yield return new WaitForSeconds(waitSecondsBeforeLevel);

            yield return StartCoroutine(NextLevel());

            yield return StartCoroutine(Climbing());

            l = l + 1;  

            hits = 0;         
        }
    }



    IEnumerator NextLevel ()
    {
        int s = 0;
        int e = levels[l].fruits.Count;
        int i = 0;
        int t = 0;
        int countNagla = 0;
        bool repeat = false;

        if (nagla != 0)
        {
            foreach (GameObject fruit in levels[l].fruits)
            {
                if (fruit.tag == "endOfRound")
                {
                    countNagla++;
                    if (countNagla == nagla)
                    {
                        s = t;
                        e = i;
                        break;
                    }
                    else
                    {
                        t = i+1;
                    }
                }
                i++;
                s = t;
            }
            repeat = true;
        }
            
        for (int f = s; f < e; f++)
        {
            if (levels[l].fruits[f].tag == "fruit" || levels[l].fruits[f].tag == "bomb")
            {
                yield return StartCoroutine(Frames(levels[l].fruitDelaysInFrames[f]));
                prefab = Instantiate(levels[l].fruits[f], new Vector3 (trainer.transform.position.x, trainer.transform.position.y, 0), Quaternion.identity);
                prefab.transform.localScale = Vector3.Scale(prefab.transform.localScale, new Vector3 (fruitSize, fruitSize, fruitSize));
                prefab.GetComponent<Rigidbody>().AddForce(levels[l].fruitVelocity[f]);
                prefab.GetComponent<Rigidbody>().AddTorque(Random.Range(510, 740.0f), Random.Range(180f, 240.6f), Random.Range(270f, 390.6f));
                Trainer.throwB = true;
            }
            else if (levels[l].fruits[f].tag == "endOfRound")
            {
                yield return StartCoroutine(finishRound());
                yield return new WaitForSeconds(levels[l].delayRoundInSeconds);
                Text("Good Job!", 4);
            }
            if (repeat && f+1 == e)
            {
                yield return StartCoroutine(finishRound());
                f = s-1;
            }
        }
        yield return StartCoroutine(finishRound());
    }

    void FixedUpdate()
    {
        if (fixedFrameCount)
        {
            frameCount--;
        }
    }

    IEnumerator Frames(int frameCounter)
    {
        frameCount = frameCounter;
        fixedFrameCount = true;
        while (frameCount > 0)
        {
            if (gameOver)
            {
                Text("Game Over", 4);
                Application.LoadLevel(Application.loadedLevel);
            }
            yield return null;
        }
        fixedFrameCount = false;
    }
        
    IEnumerator finishRound ()
    {
        while (prefab != null && prefab.transform.position.y > -1 && prefab.transform.position.x < 11.5f)
        {
            if (gameOver)
            {
                Text("Game Over", 4);
                Application.LoadLevel(Application.loadedLevel);
            }
            yield return null;
        }
        yield return null;
    }

    public static void Text(string str, int height)
    {
        textB = false;
        text3D.GetComponent<TextMesh>().text = str;
        text3D.GetComponent<MeshRenderer>().enabled = true;
        float textY;

        if (LevelNumber > 1)
        {
            int x = l - LevelNumber + 1;
            textY = 8 + (x * floorDelta); 
        }
            
        else
        {
            textY = 8 + (l * floorDelta);
        }
        text3D.transform.position = new Vector3(-2.5f, textY, -1);

        while (!textB)
        {
            text3D.transform.Translate(0,2.5f * Time.deltaTime ,0);
            if (text3D.transform.position.y > textY + height)
            {
                textB = true;
                text3D.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    IEnumerator Climbing ()
    {
        Player.turn = true;
        Trainer.climb = true;
        Trainer.turn = true;
        Cam.climb = true;
        if (levelNumber % 2 == 1)
        {
            if (l % 2 == 0)
            {
                Player.nextHeight += floorDelta;
                Trainer.nextHeight += floorDelta;
            }
            else
            {
                Player.nextHeight += floorDelta;
                Trainer.nextHeight += floorDelta;
            }
        }
        else
        {
            if (l % 2 == 0)
            {
                Player.nextHeight += floorDelta;
                Trainer.nextHeight += floorDelta;
            }
            else
            {
                Player.nextHeight += floorDelta;
                Trainer.nextHeight += floorDelta;
            }
        }

        while (Cam.climb)
        {
            yield return null;
        }

        if ((l - 3) % 6 == 0 && l != 3)
        {
            if (!stairsBool)
            {
                stairs.transform.position = new Vector3(stairs.transform.position.x, stairs.transform.position.y + 39.6f, stairs.transform.position.z);
                stairsBool = true;
            }
            else
            {
                stairs1.transform.position = new Vector3(stairs.transform.position.x, stairs1.transform.position.y + 39.6f, stairs.transform.position.z);
                stairsBool = false;
            }
        }
    }


    void OnGUI()
    {

        GUI.Label(new Rect(Screen.width-300, 20, 100, 20), "ARROWS: " + arrows, style);


        if (GUI.Button(new Rect(2, 10, 120, 80), "Restart"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (GUI.Button(new Rect(2, 100, 120, 80), "minimize"))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        if (GUI.Button(new Rect(2, 190, 120, 80), "Quit"))
        {
            Application.Quit();
        }
    }

}
