using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Screen width goes from 237 to 259.5
    private float LeftScreen = 237f;
    private float RightScreen = 259.5f;
    public float MidScreen;

    //Rotation limits for Enemy Missiles:
    private float LeftRotation = 140f;
    private float RightRotation = 40f;

    private GameSceneUI MainUIScript; //To be able to control it
    private GameObject MainUI; //To be able to control it
    private GameObject MainUIGameStartScreen;

    public int MissileAmountLeft = 15;
    public int GameLevel = 1;
    public int EnemyMissileCount = 0;

    public enum GameMode
    {
        GameRunning = 0,
        GamePaused,
        GameEnding,
        GameBuying,
        GameStart,
        GameRestart
    }
    public GameMode gameMode = 0;


    Transform SpawnPoint;
    GameObject SpawnedMissile;
    


    //Time variables
    public float Timer;
    public float resetTimer = 0.5f;
    public float levelTime = 300f;

    public GameObject DirectionalMissile;

	// Use this for initialization
	void Start () {
        MainUIScript = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GameSceneUI>();
        MainUI = GameObject.FindGameObjectWithTag("MainUI");
        MainUIGameStartScreen = GameObject.Find("/MainUI/GameStartScreen");
        MidScreen = LeftScreen + ((RightScreen - LeftScreen) / 2);
        Timer = resetTimer;
        gameMode = GameMode.GameStart;
}
	
	// Update is called once per frame
	void Update () {
        //Game is playing
        if (gameMode == GameMode.GameRunning)
        {
            Timer -= Time.deltaTime;
            levelTime -= Time.deltaTime;
            if (Timer <= 0)
            {
                //
                DirectionalMissile.transform.position = new Vector3(Random.Range(LeftScreen, RightScreen), 7f, 67.03f);
                if (DirectionalMissile.transform.position.x < MidScreen)
                {
                    LeftScreen = 90f;
                }
                if (DirectionalMissile.transform.position.x > MidScreen)
                {
                    RightScreen = 90f;
                }
                DirectionalMissile.transform.rotation = Quaternion.Euler(Random.Range(LeftRotation, RightRotation), -90f, -90f);
                SpawnedMissile = Instantiate(DirectionalMissile, DirectionalMissile.transform.position, DirectionalMissile.transform.rotation);
                //SpawnedMissile.GetComponent<EnemyMissileMovement>().directionalMissile = true;
                LeftScreen = 237f;
                RightScreen = 259.5f;
                Timer = resetTimer;
            }

            if (levelTime <=0)
            {
                gameMode = GameMode.GameEnding;
            }
        }

        //Game is paused
        if (gameMode == GameMode.GamePaused)
        {

        }

        //Game is inbetween levels
        if (gameMode == GameMode.GameBuying)
        {

        }

        //Game is at ending of level
        if (gameMode == GameMode.GameEnding)
        {

        }

        //Game is at start
        if (gameMode == GameMode.GameStart)
        {
            if (Input.anyKey)
            {
                MainUIGameStartScreen.SetActive(false);

                //GetComponent("GameStartScreen").enabled = false
                gameMode = GameMode.GameRunning;
            }
        }

        //Game restarting all settings
        if (gameMode == GameMode.GameRestart)
        {
            resetTimer = 0.5f;
            levelTime = 15f;
            gameMode = GameMode.GameStart;
        }

    }

    void FixedUpdate()
    {
        
    }

    public void RandomSpawnLocation()
    {

    }
    public void LowerMissileCount()
    {
        MissileAmountLeft--;
    }

    public string getMissileCount()
    {
        return MissileAmountLeft.ToString();
    }
    public int getMissileCountInt()
    {
        return MissileAmountLeft;
    }

    public void MissileCountAdd()
    {
        EnemyMissileCount++;
    }
    public void MissileCountRemove()
    {
        EnemyMissileCount--;
    }

    public string getEnemyMissileCount()
    {
        return EnemyMissileCount.ToString();
    }
}
