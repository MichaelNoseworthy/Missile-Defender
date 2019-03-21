using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMoneyCounter : MonoBehaviour {

    public bool doOnce = false;

    private int worth = 50;

    private GameManager GameManager;
    private Buying BuyingMenu;

    // Use this for initialization
    void Start()
    {
        GameManager = GameObject.Find("/GameManagerObject").GetComponent<GameManager>();
        BuyingMenu = GameObject.Find("/MainUI/BuyMenu").GetComponent<Buying>();


    }

    // Update is called once per frame
    void Update()
    {

       if (GameManager.gameMode == GameManager.GameMode.GameBuying)
        {
            if (doOnce == false)
            {
                doOnce = true;
                BuyingMenu.money += worth;
            }
        }
    }
}
