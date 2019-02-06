using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMouseClick : MonoBehaviour {

    public GameObject Missile;
    public GameObject SpawnPoint;
    public Camera MainCamera;
    private GameObject SpawnMissileGoToPoint;
    private GameObject SpawnedMissile;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = 1 << 9;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {

                string temp = "Mouse Hit: x:";
                temp = hit.point.x.ToString();
                temp += ", y:";
                temp += hit.point.y.ToString();
                temp += ", z:";
                temp += hit.point.z.ToString();
                Debug.Log(temp);
                //Add Network code

                SpawnMissileGoToPoint = Instantiate(SpawnPoint, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                SpawnedMissile = Instantiate(Missile, new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z), SpawnPoint.transform.rotation);
                SpawnedMissile.gameObject.GetComponent<MissileMovement>().MissileGoToPoint = SpawnMissileGoToPoint;
                SpawnedMissile.gameObject.GetComponent<MissileMovement>().Launch();

            }
        }
    }
}
