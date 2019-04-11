using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour {

    public int buildingChildren;

    public GameObject highest;
    public GameObject high;
    public GameObject medium;
    public GameObject low;
    public GameObject lowest;

    GameObject tempObject1;
    GameObject tempObject2;
    GameObject tempObject3;
    GameObject tempObject4;
    GameObject tempObject5;


    // Use this for initialization
    void Start () {
        buildingChildren = this.gameObject.transform.childCount;
    }
	
	// Update is called once per frame
	void Update () {
        buildingChildren = this.gameObject.transform.childCount;

        if (Input.GetKeyDown(KeyCode.P))
        {
            BuyBuilding();
        }

    }

    public void BuyBuilding()
    {
        if (buildingChildren < 6)
        {
            if (buildingChildren == 5)
            {
                tempObject1 = Instantiate(highest, transform.position, transform.rotation);
                tempObject1.transform.parent = this.transform;
                tempObject1.transform.SetSiblingIndex(4);
            }
            if (buildingChildren == 4)
            {
                tempObject1 = Instantiate(high, transform.position, transform.rotation);
                tempObject1.transform.parent = this.transform;
                tempObject1.transform.SetSiblingIndex(3);
            }
            if (buildingChildren == 3)
            {
                tempObject1 = Instantiate(medium, transform.position, transform.rotation);
                tempObject1.transform.parent = this.transform;
                tempObject1.transform.SetSiblingIndex(2);
            }
            if (buildingChildren == 2)
            {
                tempObject1 = Instantiate(low, transform.position, transform.rotation);
                tempObject1.transform.parent = this.transform;
                tempObject1.transform.SetSiblingIndex(1);
            }
            if (buildingChildren == 1)
            {

                tempObject1 = Instantiate(lowest, transform.position, transform.rotation);
                tempObject1.transform.parent = this.transform;
                tempObject1.transform.SetSiblingIndex(0);
            }
        }
    }
}
