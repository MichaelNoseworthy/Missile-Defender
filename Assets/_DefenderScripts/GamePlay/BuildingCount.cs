using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCount : MonoBehaviour {

    public bool doOnce = false;

    private GameManager GameManager;
    public enum BuildingType
    {
        Housing,
        Launcher
    }
    public BuildingType buildingType;

    // Use this for initialization
    void Start () {
        GameManager = GameObject.Find("/GameManagerObject").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {

        if (buildingType == BuildingType.Housing)
        {
            if (transform.childCount <= 0 && doOnce == false)
            {
                doOnce = true;
                GameManager.buildingsCount -= 1;
            }
        }
        else if (buildingType == BuildingType.Launcher)
        {
            if (transform.childCount <= 0 && doOnce == false)
            {
                doOnce = true;
                GameManager.launchersCount -= 1;
            }
        }
    }
}
