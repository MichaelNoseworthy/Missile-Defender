using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Screen width goes from 237 to 259.5
    private float LeftScreen = 240.274f;
    private float RightScreen = 250.662f;
    public float MidScreen;

    //Rotation limits for Enemy Missiles:
    private float LeftRotation = 140f;
    private float RightRotation = 40f;

    private GameSceneUI MainUIScript; //To be able to control it
    private GameObject MainUI; //To be able to control it
    private GameObject MainUIGameStartScreen;
    private GameObject BuyMenu;
    private GameObject ScreenInfoUI;
    private GameObject PauseMenu;
    private GameObject GameLevelPanel;
    private Buying BuyMenuScript;
    private GameObject[] CountMoney; //Grabs gameobjects so the money count can happen
    private BuildingMoneyCounter[] CountMoneyScript; //Grabs gameobjects so the money count can happen

    public int GameLevel = 1;
    public bool showGameLevel = true;
    public int EnemyMissileCount = 0;


    public float buildingsCount = 0;
    public float launchersCount = 0;
    private bool firststart = true; //Prevents game from ending prematurely

    //Settings
    //Enemy Missiles:
    public float EnemyMissileSpeed;

    //Player Missiles:
    public float PlayerMissileSpeed;
    public float PlayerExplosionSize;
    public float PlayerExplosionTime;
    public int PlayerMissileAmount = 15;
    public int PlayerMissileAmountLeft = 15;

    public enum GameMode
    {
        GameRunning = 0,
        GamePaused,
        GameEnding,
        GameBuying,
        GameStart,
        GameRestart,
        GameLost
    }
    public GameMode gameMode = 0;


    Transform SpawnPoint;
    GameObject SpawnedMissile;
    


    //Time variables
    public float Timer;
    public float resetTimer = 0.5f;
    public float levelTime = 15f;

    public GameObject DirectionalMissile;

	// Use this for initialization
	void Start () {
        MainUIScript = GameObject.FindGameObjectWithTag("MainUI").GetComponent<GameSceneUI>();
        MainUI = GameObject.FindGameObjectWithTag("MainUI");
        //GameLevelPanel = GameObject.FindGameObjectWithTag("/MainUI/GameLevel");
        MainUIGameStartScreen = GameObject.Find("/MainUI/GameStartScreen");
        ScreenInfoUI = GameObject.Find("/MainUI/ScreenInfo");
        BuyMenu = GameObject.Find("/MainUI/BuyMenu");
        PauseMenu = GameObject.Find("/MainUI/PausedMenu");
        BuyMenuScript = GameObject.Find("/MainUI/BuyMenu").GetComponent<Buying>();
        MidScreen = LeftScreen + ((RightScreen - LeftScreen) / 2);
        Timer = resetTimer;
        gameMode = GameMode.GameRestart;

        PauseMenu.SetActive(false);

        //Settings
        EnemyMissileSpeed = 0.5f;
        PlayerMissileSpeed = 3.0f;
        PlayerExplosionSize = 1.0f;
        PlayerExplosionTime = 6.0f;
        PlayerMissileAmountLeft = PlayerMissileAmount;

        showGameLevel = true;
    }
	
	// Update is called once per frame
	void Update () {
        //Game is playing
        if (gameMode == GameMode.GameRunning)
        {
            ScreenInfoUI.SetActive(true);
            Timer -= Time.deltaTime;
            levelTime -= Time.deltaTime;

            if (buildingsCount <= 0)// || launchersCount <= 0)
            {
                gameMode = GameMode.GameLost;
            }

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

        //Game is lost
        if (gameMode == GameMode.GameLost)
        {

        }

        //Game is inbetween levels
        if (gameMode == GameMode.GameBuying)
        {
            BuyMenu.SetActive(true);
        }

        //Game is at ending of level
        if (gameMode == GameMode.GameEnding)
        {
            CountMoney = GameObject.FindGameObjectsWithTag("Building");
            for (int i = 0; i < CountMoney.Length; i++)
            {
                CountMoney[i].gameObject.GetComponent<BuildingMoneyCounter>().doOnce = false;
            }
            if (buildingsCount <= 0 || launchersCount <= 0)
            {
                gameMode = GameMode.GameLost;
            }

            if (EnemyMissileCount <= 0)
            {
                ScreenInfoUI.SetActive(false);
                BuyMenu.SetActive(true);
                //BuyMenuScript.money += 1000;
                gameMode = GameMode.GameBuying;
            }
        }

        //Game is at start
        if (gameMode == GameMode.GameStart)
        {
            ScreenInfoUI.SetActive(false);
            BuyMenu.SetActive(false);
            if (Input.GetKeyUp(KeyCode.Space))
            {
                MainUIGameStartScreen.SetActive(false);

                //GetComponent("GameStartScreen").enabled = false
                gameMode = GameMode.GameRunning;
            }
        }

        //Game restarting all settings
        if (gameMode == GameMode.GameRestart)
        {
            Timer = resetTimer;
            levelTime = 15f;
            PlayerMissileAmountLeft = PlayerMissileAmount;
            gameMode = GameMode.GameStart;
            showGameLevel = true;
            //GameLevelPanel.SetActive(true);
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
        PlayerMissileAmountLeft--;
    }

    public string getMissileCount()
    {
        return PlayerMissileAmountLeft.ToString();
    }
    public int getMissileCountInt()
    {
        return PlayerMissileAmountLeft;
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

    public void GoToGameState()
    {
        BuyMenu.SetActive(false);
        MainUIGameStartScreen.SetActive(true);
        ScreenInfoUI.SetActive(true);
        gameMode = GameMode.GameRestart;
    }

    public void PlayerIncreaseMissileSpeed()
    {
        if (BuyMenuScript.money >= 250)
        {
            BuyMenuScript.money -= 250;
            BuyMenuScript.increaseMissileSpeed += 1;
            PlayerMissileSpeed += 0.5f;
        }
        else
        {
            BuyMenuScript.DisplayNoMoney();
        }
    }

    public void PlayerIncreaseMissileStock()
    {
        if (BuyMenuScript.money >= 250)
        {
            BuyMenuScript.money -= 250;
            PlayerMissileAmount += 1;
        }
        else
        {
            BuyMenuScript.DisplayNoMoney();
        }
    }
    public void PlayerIncreaseExplosionSpeed()
    {
        if (BuyMenuScript.money >= 250)
        {
            BuyMenuScript.money -= 250;
            BuyMenuScript.increaseExplosionSpeed += 1;
            PlayerExplosionSize += 0.5f;
        }
        else
        {
            BuyMenuScript.DisplayNoMoney();
        }
    }

    public void PlayerIncreaseExplosionTime()
    {
        if (BuyMenuScript.money >= 250)
        {
            BuyMenuScript.money -= 250;
            BuyMenuScript.increaseExplosionSize += 1;
            PlayerExplosionTime += 0.5f;
        }
        else
        {
            BuyMenuScript.DisplayNoMoney();
        }
    }

    public void ActivateArmageddon()
    {
        EnemyMissileSpeed = 10;
        resetTimer = 0.6f;
        Timer = resetTimer;
        levelTime = 15000f;
        PlayerMissileAmountLeft = 0;
    }
}
