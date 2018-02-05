using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanics : MonoBehaviour {

	public Transform[] patrolPoints; //container for patrol points
	public float speed;
	Transform currentPatrolPoint;
	int currentPatrolIndex;

	// Use this for initialization
	void Start () {
		currentPatrolIndex = 0;
		currentPatrolPoint = patrolPoints [currentPatrolIndex];
	}
	
	// Update is called once per frame
	void Update () {
		Patrol ();
	}

	// Class that controls the direction of enemy patrolling
	void Patrol () {
		// Check to see if we reached the patrol point
		if(Vector2.Distance(transform.position,currentPatrolPoint.position) < 1f){
			// reached the patrol point - get the next one
			// check to see if we have anymore patrol points - if not go back to beginning
			if (currentPatrolIndex + 1 < patrolPoints.Length) {
				currentPatrolIndex++;
			} else {
				currentPatrolIndex = 0;
			}
			currentPatrolPoint = patrolPoints [currentPatrolIndex];
		}

		// turn to face the current patrol point
		Vector2 patrolPointDir = currentPatrolPoint.position - transform.position;

		//figure out if the patrol point is left or right of the enemy
		if(patrolPointDir.x < 0f) {
			transform.Translate (Vector2.left * Time.deltaTime * speed);	
		}
		if(patrolPointDir.x > 0f) {
			transform.Translate (Vector2.right * Time.deltaTime * speed);	
		}
	}
}
