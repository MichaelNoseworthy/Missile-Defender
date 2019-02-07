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


    Transform SpawnPoint;
    GameObject SpawnedMissile;


    //Time variables
    public float Timer;
    public float resetTimer = 0.5f;
    public float levelTime = 15f;

    public GameObject DirectionalMissile;

	// Use this for initialization
	void Start () {
        MidScreen = LeftScreen + ((RightScreen - LeftScreen) / 2);
        Timer = resetTimer;
        
    }
	
	// Update is called once per frame
	void Update () {
        Timer -= Time.deltaTime;
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
    }

    void FixedUpdate()
    {
        
    }

    public void RandomSpawnLocation()
    {

    }
}
