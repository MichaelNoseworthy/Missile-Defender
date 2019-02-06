using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehavior : MonoBehaviour {

    public GameObject missile;
    public Transform Placement;
    public Camera MainCamera;

    // Use this for initialization
    void Start () {
        Debug.Log("Game Begins");
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Instantiate(missile, Placement.transform.position, Quaternion.identity);
            Debug.Log("Mouse0");
            Ray ray;
            RaycastHit hit;
            ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("First");
                if (hit.collider.name == "BackWall")
                {
                    Instantiate(missile, hit.point, Quaternion.identity);
                    //Instantiate(missile, Vector3(hit.point.x, hit.point.y, hit.point.z) , Quaternion.identity)
                    Debug.Log("Hit");
                }

            }
        }
    }
}
