using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionExpand : MonoBehaviour {

    public float moveSpeed = 1.0f;
    public float timer = 1.0f;
    public float time = 0;
    private GameManager GameManager;

    // Use this for initialization
    void Start () {
        GameManager = GameObject.Find("/GameManagerObject").GetComponent<GameManager>();
        moveSpeed = GameManager.PlayerExplosionSize;
        timer = GameManager.PlayerExplosionTime;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        transform.localScale += new Vector3(0.1F * Time.deltaTime * moveSpeed, 0.1F * Time.deltaTime * moveSpeed, 0.1F* Time.deltaTime * moveSpeed);
        //if (transform.localScale.x >= 1.0f)
        if (time >= timer)
            Destroy(gameObject);

        if (GameManager.gameMode == GameManager.GameMode.GameBuying)
        {
            Destroy(gameObject);
        }
    }
}
