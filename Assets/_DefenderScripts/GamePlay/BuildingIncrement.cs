using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIncrement : MonoBehaviour {

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

        if (buildingType == BuildingType.Housing)
        {
            GameManager.buildingsCount += 1;
        }
        else if (buildingType == BuildingType.Launcher)
        {
            GameManager.launchersCount += 1;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
