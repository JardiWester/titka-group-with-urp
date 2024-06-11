using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class npcMovement : MonoBehaviour
{
    
    [SerializeField] private Transform waypointParent;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private int waypointIndex = 1;
    [SerializeField]private float speed;
    [SerializeField] private Vector3 oldPos;


    void Start()
    {
        foreach (Transform waypoint in waypointParent)
        {
            if (waypoint.tag == "waypoint")
            {
                waypoints.Add(waypoint);
            }
        }
    }


    void Update()
    {
        if (Vector3.Distance (gameObject.transform.position, waypoints[waypointIndex].transform.position) < (speed * Time.deltaTime))
        {
            

            if (waypointIndex > waypoints.Count - 2)
            {
                transform.position = Vector3.MoveTowards (gameObject.transform.position, waypoints[0].transform.position, speed*Time.deltaTime - Vector3.Distance (gameObject.transform.position, waypoints[waypointIndex].transform.position));
                waypointIndex = 0;
            }else
            {
                transform.position = Vector3.MoveTowards (gameObject.transform.position, waypoints[waypointIndex + 1].transform.position, speed*Time.deltaTime - Vector3.Distance (gameObject.transform.position, waypoints[waypointIndex].transform.position));
                waypointIndex++;
            }
            
            
        } else{
            transform.position = Vector3.MoveTowards (gameObject.transform.position, waypoints[waypointIndex].transform.position, speed*Time.deltaTime);
        }
        
        if(gameObject.transform.position != oldPos)
        {
            transform.LookAt(transform.position - (oldPos - transform.position));
            oldPos = transform.position;
        }
        
    }
}
