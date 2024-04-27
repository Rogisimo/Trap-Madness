using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public Transform[] wayPoints;
    public float speed = 5f;
    public bool isMoving = true;
    private int wayPointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        if(isMoving){
            transform.position = wayPoints[wayPointIndex].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            Move();
        }
    }


    void Move(){
        transform.position 
        = Vector2.MoveTowards(transform.position,wayPoints[wayPointIndex].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, wayPoints[wayPointIndex].position) < 0.1f){
            wayPointIndex = (wayPointIndex + 1) % wayPoints.Length;
        }
    }
}
