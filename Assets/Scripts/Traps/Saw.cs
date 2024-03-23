using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the waypoints
    public float speed = 5f; // Speed at which the SawTrap moves
    private int waypointIndex = 0; // Current waypoint index
    public bool isMoving = true; // Flag to control movement

    void Start()
    {
        if(isMoving){
            transform.position = waypoints[waypointIndex].position; // Start at the position of the first waypoint
        }
    }

    void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        // Move towards the next waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);

        // Check if the SawTrap has reached the waypoint
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            // Increment the waypoint index, looping back to the start if necessary
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
    }
}
