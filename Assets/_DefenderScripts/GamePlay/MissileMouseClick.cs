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
    List<float> turretDistance = new List<float>();
    List<bool> activeTurrets = new List<bool>();
    MissileLauncherController tempLauncher;

    private GameManager GameManager;

    // Use this for initialization
    void Start() {
        //MissileLaunchers = GameObject.FindGameObjectsWithTag("Launcher");
        //minDistance = Vector3.zero;
        //minDistance.x = MissileLaunchers[0].transform.position.x;
        GameManager = GameObject.Find("/GameManagerObject").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (GameManager.getMissileCountInt() > 0)
            if (GameManager.gameMode == GameManager.GameMode.GameRunning || GameManager.gameMode == GameManager.GameMode.GameEnding)
            {
                MissileLaunchers = GameObject.FindGameObjectsWithTag("Launcher");
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


                    /*
                     * Measure distance between each turret from the mouse point
                     * convert distance from negative to positive
                     * Compare all turrets with distance.  Least distance == to turret#
                     * Activate turret within least distance (UNLESS..)
                     * If turret is inactive, invalidate turret and repeat measurements with other turrets
                     * If turret is still active, fire projectile.
                     * mouse = 55x
                     * turret1 = 56x
                     * turret2 = 60x
                     * turret3 = 70x
                     * turretdist1 = -6x
                     * turretdist2 = 4x
                     * turretdist3 = 14x
                     * 
                     * dist = turret1 - point
                     * dist = turret2 - point
                     * 
                     */

                    float dist1;
                    float dist2;

                    for (int i = 0; i < MissileLaunchers.Length; i++)
                    {
                        //hit.point.x;
                        if (MissileLaunchers[i].gameObject.GetComponent<MissileLauncherController>().isPaused == true)
                        {
                            turretDistance.Add(10000);
                        }
                        else
                        {
                            turretDistance.Add(NegativeToPositive(MissileLaunchers[i].transform.position.x - hit.point.x));
                        }


                        /*
                        if (i == 0)
                            dist1 = NegativeToPositive(MissileLaunchers[i].transform.position.x - hit.point.x);

                        if (i == 1)
                            dist2 = NegativeToPositive(MissileLaunchers[i].transform.position.x - hit.point.x);

                        if (MissileLaunchers[i].GetComponent<MissileLauncherController>().isPaused == false)
                        {
                            activeTurrets.Add(false);
                        }
                        else
                            activeTurrets.Add(true);
                            */



                        //old code
                        /*
                       if (MissileLaunchers[i].transform.position.x - hit.point.x > dist 
                           && dist2 > hit.point.x - MissileLaunchers[i].transform.position.x)
                       {
                           dist = MissileLaunchers[i].transform.position.x;
                           dist2 = MissileLaunchers[i].transform.position.x;
                               SpawnPoint.transform.position = MissileLaunchers[i].transform.position;
                       }

                       //if (minDistance.x - MissileLaunchers[i].transform.position.x < hit.point.x)
                       */



                    }
                    if (AreAnyTurretsInactive() == false)
                    {
                        int fireturret = -1;
                        //int availableturret = -1;
                        float minDist = 0;// MissileLaunchers[0].transform.position.x;

                        for (int i = 0; i < MissileLaunchers.Length; i++)
                        {


                            if (MissileLaunchers[i].gameObject.GetComponent<MissileLauncherController>().isPaused == true)
                            {
                                //continue;
                            }
                            else
                            {

                                if (minDist == 0)
                                {
                                    minDist = turretDistance[i];
                                }

                                if (turretDistance[i] <= minDist)
                                {
                                    minDist = turretDistance[i];
                                    fireturret = i;
                                }
                            }
                        }

                        if (fireturret >= 0)
                        {
                            SpawnPoint.transform.position = MissileLaunchers[fireturret].transform.position;
                            SpawnMissileGoToPoint = Instantiate(SpawnPoint, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            SpawnedMissile = Instantiate(Missile, new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z), SpawnPoint.transform.rotation);
                            SpawnedMissile.gameObject.GetComponent<MissileMovement>().MissileGoToPoint = SpawnMissileGoToPoint;
                            SpawnedMissile.gameObject.GetComponent<MissileMovement>().Launch();
                            GameManager.LowerMissileCount();
                        }
                        else
                        {
                            Debug.Log("Fire turret error");
                        }
                    }
                    else
                    {
                        //say all turrets are busy
                        Debug.Log("All turrets are busy");
                    }
                }


                turretDistance.Clear();
                activeTurrets.Clear();
            }
        }
    }



    float NegativeToPositive(float value)
    {
        float zero = 0;

        if (value < zero)
        {
            value *= -1;
        }
        return value;
    }
    bool AreAnyTurretsInactive()
    {
        for (int i = 0; i < MissileLaunchers.Length; i++)
        {
            
            if (MissileLaunchers[i].gameObject.GetComponent<MissileLauncherController>().getisPaused() == true)
            {
                return true;
            }
        }
        return false;
    }

}
