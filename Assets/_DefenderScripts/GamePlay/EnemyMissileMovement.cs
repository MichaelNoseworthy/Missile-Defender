using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMissileMovement : MonoBehaviour {

    private GameObject[] Buildings;
    public GameObject MissileSetDestination;
    public GameObject MissileGoToPoint;
    public GameObject Explosion;
    public float moveSpeed = 1.0f;
    public float distanceFromTarget = 0.0f;
    bool isLaunched = false;
    public float distance;
    private bool doOnce = false;
    private bool DestinationReached = false;
    public static Collider[] colliders;//Array to place where the closest enemy AI is
    public float checkRadius = 100f;//Range to check for enemy AI
    public LayerMask checkLayers;//Which layer to check for the enemy.
    public bool guidedMissile = false;
    private bool MissileCollided = false;

    //testing stuff below
    public bool enter = true;
    public bool stay = true;
    public bool exit = true;

    // Use this for initialization
    void Start()
    {
        if (guidedMissile)
        {
            colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            Buildings = GameObject.FindGameObjectsWithTag("Building");
            Array.Sort(colliders, new DistanceComparer(transform));
            MissileGoToPoint = colliders[0].GetComponent<GameObject>();
        }
        else
        {
            MissileGoToPoint = MissileSetDestination;
        }
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, MissileGoToPoint.transform.position);
        if (distance < 0.2f)
        {
            DestinationReached = true;

            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (isLaunched && !DestinationReached && !MissileCollided)
        {
            MoveToward(MissileGoToPoint.transform);
            //transform.Translate(Vector3.down * Time.deltaTime * Input.GetAxis("Horizontal") * moveSpeed);
        }
    }

    public void Launch()
    {
        isLaunched = true;
    }


    //Used to move the AI towards a destination
    public void MoveToward(Transform destination)
    {
        
        // rotate and move towards object

        Vector3 targetDirection = destination.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, targetDirection.y, 0));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        transform.transform.position += transform.transform.forward * moveSpeed * Time.deltaTime;
        
        
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit Something");
        if (collision.gameObject.tag == "Explosion")
            Destroy(gameObject);

        if (collision.gameObject.tag == "Building")
            Destroy(gameObject);
    }
    */

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
        {
            Destroy(collision.gameObject);
        }
        MissileCollided = true;
        Debug.Log("Hit Something");
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(Explosion, pos, rot);
        Destroy(gameObject);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (enter)
        {
            Debug.Log("entered");
        }
    }

    // stayCount allows the OnTriggerStay to be displayed less often
    // than it actually occurs.
    private float stayCount = 0.0f;
    private void OnTriggerStay(Collider other)
    {
        if (stay)
        {
            if (stayCount > 0.25f)
            {
                Debug.Log("staying");
                stayCount = stayCount - 0.25f;
            }
            else
            {
                stayCount = stayCount + Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (exit)
        {
            Debug.Log("exit");
        }
    }

    /*
    // Destroy everything that enters the trigger
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
    */

}
