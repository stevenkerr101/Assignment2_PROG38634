﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAI : MonoBehaviour
{
    // General state machine variables 
    private GameObject player;
    private Animator animator;
    private Ray ray;
    private RaycastHit hit;
    private float maxDistanceToCheck = 6.0f;
    private float currentDistance;
    private Vector3 checkDirection;

    // Patrol state variables 
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    public NavMeshAgent navMeshAgent;

    private int currentTarget;
    private float distanceFromTarget;
    private Transform[] waypoints = null;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();

        //COMMENTED THIS OUT SO I COULD ASSIGN DIFFERENT POINTS TO EACH FRIENDLY
        //pointA = GameObject.Find("Friendly1Patrol1").transform;
        //pointB = GameObject.Find("Friendly1Patrol2").transform;
        //pointC = GameObject.Find("Friendly1Patrol3").transform;
        //pointD = GameObject.Find("Friendly1Patrol4").transform;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        waypoints = new Transform[4] {
            pointA,
            pointB,
            pointC,
            pointD
        };
        currentTarget = 0;
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }

    private void FixedUpdate()
    {
        //First we check distance from the player  
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", currentDistance);

        //Then we check for visibility 
        checkDirection = player.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);
        if (Physics.Raycast(ray, out hit, maxDistanceToCheck))
        {
            if (hit.collider.gameObject == player)
            {
                animator.SetBool("isPlayerVisible", true);
            }
            else
            {
                animator.SetBool("isPlayerVisible", false);
            }
        }
        else
        {
            animator.SetBool("isPlayerVisible", false);
        }

        //Lastly, we get the distance to the next waypoint target 
        distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceFromTarget);
    }

    public void SetNextPoint()
    {
        switch (currentTarget)
        {
            case 0:
                currentTarget = 2;
                break;
            case 1:
                currentTarget = 3;
                break;
            case 2:
                currentTarget = 1;
                break;
            case 3:
                currentTarget = 0;
                break;
        }
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }
}