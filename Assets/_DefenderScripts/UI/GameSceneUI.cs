using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour {

    public Text MissileCount;
    public Text HousingCount;
    public Text LauncherCount;
    public Text EnemyMissileCount;
    public Text GameLevel;
    public Text GameEndingLevel;
    public GameObject GameOverPanel;
    public GameObject GameLevelPanel;
    public GameObject GameDebugInfoPanel;
    private GameManager GM;
    private bool debugging = true;
    public float time = 1.0f;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("/GameManagerObject").GetComponent<GameManager>();
        GameLevel.text = GM.GameLevel.ToString();
        GameLevelPanel.SetActive(false);
        GameDebugInfoPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        MissileCount.text = GM.getMissileCount();
        EnemyMissileCount.text = GM.getEnemyMissileCount();
        GameLevel.text = GM.GameLevel.ToString();
        HousingCount.text = GM.buildingsCount.ToString();
        LauncherCount.text = GM.launchersCount.ToString();
        GameEndingLevel.text = GM.GameLevel.ToString();

        if (GM.gameMode == GameManager.GameMode.GameLost)
        {
            GameOverPanel.SetActive(true);
        }

        if (GM.gameMode == GameManager.GameMode.GameRunning)
        {
            if (debugging == true)
            {
                GameDebugInfoPanel.SetActive(true);
            }
            else
            {
                GameDebugInfoPanel.SetActive(false);
            }
            if (GM.showGameLevel == true)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    GameLevelPanel.SetActive(false);
                    GM.showGameLevel = false;
                    time = 1.0f;
                }
            }
        }

        if (GM.gameMode != GameManager.GameMode.GameRunning && GM.gameMode != GameManager.GameMode.GameEnding)
        {
            GameDebugInfoPanel.SetActive(false);
        }

    }
}
