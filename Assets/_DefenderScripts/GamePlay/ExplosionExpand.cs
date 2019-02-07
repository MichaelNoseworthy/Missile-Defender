using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionExpand : MonoBehaviour {

    public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.localScale += new Vector3(0.1F * Time.deltaTime * moveSpeed, 0.1F * Time.deltaTime * moveSpeed, 0.1F* Time.deltaTime * moveSpeed);
        if (transform.localScale.x >= 1.0f)
            Destroy(gameObject);
    }
}
