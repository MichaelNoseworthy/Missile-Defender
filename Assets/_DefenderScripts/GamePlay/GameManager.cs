using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Screen width goes from 237 to 259.5
    private const float LeftScreen = 243.274f;
    private const float RightScreen = 252.662f;
    public float MidScreen;

    //Rotation limits for Enemy Missiles:
    private const float LeftRotation = 140f;
    private const float RightRotation = 40f;

    private GameSceneUI MainUIScript; //To be able to control it
    private GameObject MainUI; //To be able to control it
    private GameObject MainUIGameStartScreen;
    private GameObject BuyMenu;
    private GameObject ScreenInfoUI;
    private GameObject PauseMenu;
    public GameObject GameLevelPanel;
    private GameObject YDistanceErrorText;
    private Buying BuyMenuScript;
    private GameObject[] CountMoney; //Grabs gameobjects so the money count can happen
    private BuildingMoneyCounter[] CountMoneyScript; //Grabs gameobjects so the money count can happen

    public int GameLevel = 1;
    public bool showGameLevel = true;
    public int EnemyMissileCount = 0;

    public bool YDistanceBool = false;
    float YDistancetime = 1.0f;

    public float buildingsCount = 0;
    public float launchersCount = 0;
    private bool firststart = true; //Prevents game from ending prematurely

    //Settings
    //Enemy Missiles:
    public float EnemyMissileSpeed;

    //Player Missiles:
    public float PlayerMissileSpeed;
    private float PlayerMissileSpeedMax = 9.0f;//9;
    public float PlayerExplosionSize;
    public float PlayerExplosionSizeMax = 3;
    public float PlayerExplosionTime;
    public float PlayerExplosionTimeMax = 6;
    public int PlayerMissileAmount = 15;
    public int PlayerMissileAmountLeft = 15;
    private float tempRotationFloat = 0;

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
        YDistanceErrorText = GameObject.Find("/MainUI/YDistance");
        BuyMenuScript = GameObject.Find("/MainUI/BuyMenu").GetComponent<Buying>();
        MidScreen = LeftScreen + ((RightScreen - LeftScreen) / 2);
        Timer = resetTimer;
        gameMode = GameMode.GameRestart;

        YDistanceErrorText.SetActive(false);
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
            if (YDistanceBool == true)
            {
                YDistanceErrorText.SetActive(true);
                YDistancetime -= Time.deltaTime;
                if (YDistancetime <= 0)
                {
                    YDistanceErrorText.SetActive(false);
                    YDistanceBool = false;
                    YDistancetime = 1.0f;
                }
            }
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
                DirectionalMissile.transform.position = new Vector3(Random.Range(RightScreen, LeftScreen), 7f, 67.03f);
                if (DirectionalMissile.transform.position.x < LeftScreen)
                {
                    tempRotationFloat = MissileRotationPosition(0);
                }
                else if (DirectionalMissile.transform.position.x > RightScreen)
                {
                    tempRotationFloat = MissileRotationPosition(1);
                }
                else
                {
                    tempRotationFloat = MissileRotationPosition(2);
                }
                DirectionalMissile.transform.rotation = Quaternion.Euler(tempRotationFloat, -90f, -90f);
                SpawnedMissile = Instantiate(DirectionalMissile, DirectionalMissile.transform.position, DirectionalMissile.transform.rotation);
                //SpawnedMissile.GetComponent<EnemyMissileMovement>().directionalMissile = true;
                //LeftScreen = 237f;
                //RightScreen = 259.5f;
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
            showGameLevel = true;
            GameLevelPanel.SetActive(true);
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
        GameLevel += 1;

        //increase difficulty
        if (EnemyMissileSpeed < 9)
        EnemyMissileSpeed += 0.5f;
        if (levelTime <= 60)
        levelTime += 5;

        gameMode = GameMode.GameRestart;
    }

    public void PlayerIncreaseMissileSpeed()
    {
        if (PlayerMissileSpeedMax >= PlayerMissileSpeed)
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
        if (PlayerExplosionSizeMax >= PlayerExplosionSize)
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
        if (PlayerExplosionTimeMax >= PlayerExplosionTime)
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

    public float MissileRotationPosition(int value)
    {
        if (value == 0)
        {
            return Random.Range(90, RightRotation);
        }
        if (value == 1)
        {
            return Random.Range(LeftRotation, 90);
        }


            return Random.Range(LeftRotation, RightRotation);
    }
}
