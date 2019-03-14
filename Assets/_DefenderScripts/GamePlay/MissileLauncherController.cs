using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherController : MonoBehaviour {

    public bool isPaused = false;
    public GameObject SpawnPoint;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool getisPaused()
    {
        return isPaused;
    }
}
