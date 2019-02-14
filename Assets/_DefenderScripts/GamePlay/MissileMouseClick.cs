using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMouseClick : MonoBehaviour {

    public GameObject Missile;
    public GameObject SpawnPoint;
    public Camera MainCamera;
    private GameObject SpawnMissileGoToPoint;
    private GameObject SpawnedMissile;
    public Vector3 minDistance;
    public GameObject[] MissileLaunchers;

    // Use this for initialization
    void Start () {
        MissileLaunchers = GameObject.FindGameObjectsWithTag("Launcher");
        minDistance = Vector3.zero;
        minDistance.x = MissileLaunchers[0].transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        MissileLaunchers = GameObject.FindGameObjectsWithTag("Launcher");
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

                //find a launch point
                
                for (int i = 0; i < MissileLaunchers.Length; i++)
                {
                    float dist = 0.0f;
                    float dist2 = 0.0f;
                    //hit.point.x;
                    /*
                     * turret1 = 54 
                     * turret2 = 58
                     * turret3 = 59
                     * point = 55
                     * dist = turret1 - point
                     * dist = turret2 - point
                     * 
                     */
                    if (MissileLaunchers[i].transform.position.x - hit.point.x > dist 
                        && dist2 > hit.point.x - MissileLaunchers[i].transform.position.x)
                    {
                        dist = MissileLaunchers[i].transform.position.x;
                        dist2 = MissileLaunchers[i].transform.position.x;
                            SpawnPoint.transform.position = MissileLaunchers[i].transform.position;
                    }

                    //if (minDistance.x - MissileLaunchers[i].transform.position.x < hit.point.x)

                }

                SpawnMissileGoToPoint = Instantiate(SpawnPoint, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                SpawnedMissile = Instantiate(Missile, new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z), SpawnPoint.transform.rotation);
                SpawnedMissile.gameObject.GetComponent<MissileMovement>().MissileGoToPoint = SpawnMissileGoToPoint;
                SpawnedMissile.gameObject.GetComponent<MissileMovement>().Launch();

            }
        }
    }
}
