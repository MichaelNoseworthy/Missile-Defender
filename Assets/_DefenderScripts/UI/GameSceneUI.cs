using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour {

    public Text MissileCount;
    public Text EnemyMissileCount;
    private GameManager GM;


	// Use this for initialization
	void Start () {
        GM = GameObject.Find("/GameManagerObject").GetComponent<GameManager>();

    }
	
	// Update is called once per frame
	void Update () {
        MissileCount.text = GM.getMissileCount();
        EnemyMissileCount.text = GM.getEnemyMissileCount();

    }
}
