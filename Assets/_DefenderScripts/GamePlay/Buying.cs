using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buying : MonoBehaviour {

    //cost of buying stuffs

    int missiles = 250;
    int explosions = 250;
    int buildings = 250;
    int launchers = 250;
    public int increaseMissileSpeed = 1;
    public int increaseExplosionSpeed = 1;
    public int increaseExplosionSize = 1;
    public int money = 0;

    //UI elements
    public Text missilesCost;
    public Text explosionsCost;
    public Text buildingsCost;
    public Text launcherCost;
    public Text missileLevel;
    public Text explosionLevel;
    public Text explosionSizeLevel;
    public Text moneyAmount;
    public Text missilesOwned;
    public Text displayInvalidMoneyAmount;
    private GameObject BuyMenu;
    private GameManager GameManager;
    bool booltimetodisplayNoMoney = false;
    float timetodisplayNoMoney = 1.0f;




    // Use this for initialization
    void Start () {
        BuyMenu = GameObject.Find("/MainUI/BuyMenu");
        GameManager = GameObject.Find("/GameManagerObject").GetComponent<GameManager>();
        missilesCost.text = missiles.ToString();
        explosionsCost.text = explosions.ToString();
        buildingsCost.text = buildings.ToString();
        launcherCost.text = launchers.ToString();
        missileLevel.text = increaseMissileSpeed.ToString();
        explosionLevel.text = increaseExplosionSpeed.ToString();
        explosionSizeLevel.text = increaseExplosionSize.ToString();
        moneyAmount.text = money.ToString();
        missilesOwned.text = GameManager.PlayerMissileAmount.ToString();

        displayInvalidMoneyAmount.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {

        missileLevel.text = increaseMissileSpeed.ToString();
        explosionLevel.text = increaseExplosionSpeed.ToString();
        explosionSizeLevel.text = increaseExplosionSize.ToString();
        moneyAmount.text = money.ToString();
        missilesOwned.text = GameManager.PlayerMissileAmount.ToString();

        if (booltimetodisplayNoMoney == true)
        {
            timetodisplayNoMoney -= Time.deltaTime;
            if (timetodisplayNoMoney <= 0)
            {
                displayInvalidMoneyAmount.enabled = false;
                booltimetodisplayNoMoney = false;
                timetodisplayNoMoney = 1.0f;
            }
        }
    }
    /*
    public void continueGame()
    {
        BuyMenu.SetActive(false);
    }

    */

    public void DisplayNoMoney()
    {
        booltimetodisplayNoMoney = true;
        displayInvalidMoneyAmount.enabled = true;
    }
}
