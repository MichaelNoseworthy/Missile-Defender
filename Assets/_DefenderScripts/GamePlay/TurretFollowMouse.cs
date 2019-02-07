using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFollowMouse : MonoBehaviour {


    public GameObject Turret;
    private Transform target;
    public Camera cam;
    public float rotationSpeed = 45;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        target.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        Turret.gameObject.transform.LookAt(target.transform);
        */
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        
        RaycastHit hit;
        int layerMask = 1 << 9;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
