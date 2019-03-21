using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissileMovement : MonoBehaviour
{

    public GameObject MissileGoToPoint;
    public GameObject Explosion;
    public float moveSpeed = 1.0f;
    public float distanceFromTarget = 0.0f;
    bool isLaunched = false;
    public float distance;
    private bool doOnce = false;
    private bool DestinationReached = false;
    private GameManager GameManager;

    // Use this for initialization
    void Start()
    {
        GameManager = GameObject.Find("/GameManagerObject").GetComponent<GameManager>();
        moveSpeed = GameManager.PlayerMissileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, MissileGoToPoint.transform.position);
        if (distance < 0.1f)
        {
            if (!doOnce)
            Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            doOnce = true;
            DestinationReached = true;
            Destroy(gameObject);
        }
        if (isLaunched && !DestinationReached)
        MoveToward(MissileGoToPoint.transform);
        Destroy(gameObject, 5);
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
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, targetDirection.y, targetDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        transform.transform.position += transform.transform.forward * moveSpeed * Time.deltaTime;
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyMissile")
        {
            if (!doOnce)
                Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            doOnce = true;
            DestinationReached = true;
            Destroy(gameObject);
        }
    }
    

}