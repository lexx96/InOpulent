using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterControllerParent {
	public enum AIstate{
		IDLE,
		PATROL,
		ATTACK
	};

	public AIstate state;

    [Header("AI attack variables")]

	[SerializeField, Range(1,5)]
	float _attackCoolDown;

    [Space]

    [Header("AI variables")]
    [SerializeField]
	NavMeshAgent nma;
	[SerializeField]
	Transform[] wayPoints;
    [SerializeField, Range(2f, 5f), Tooltip("Distance to waypoint before changing")]
    float minDist;
    [SerializeField]
    Transform player;

    [SerializeField, Range(1,5)]
    float _waitTime;
    int currentWaypoint;

    bool isPatrolling;
    bool isAttacking;
	Transform moveTarget;
    int temp;


    private void Start()
    {
        if(!player)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        nma.speed = _moveSpeed;
        LookAForNextState(state);
    }

    private void Update()
    {
        if (isPatrolling)
            PatrolArea();

        if(isAttacking && state == AIstate.ATTACK){
            CallAttack();
        }

        if (_currentHealth <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    void LookAForNextState(AIstate currentState){
        state = currentState;
		switch(currentState){
			case AIstate.IDLE:
                StartCoroutine(Idle());
			break;
			case AIstate.PATROL:
                nma.speed = _moveSpeed;
                temp = currentWaypoint % wayPoints.Length;
                isPatrolling = true;
                isAttacking = false;
			break;
			case AIstate.ATTACK:
                nma.speed = 0;
                isPatrolling = false;
                break;
		}
	}

	void PatrolArea(){
        if (wayPoints.Length > 0)
        {
            nma.SetDestination(wayPoints[temp].position);

            if(transform.position == wayPoints[temp].position || Vector3.Distance(transform.position , wayPoints[temp].position) < minDist)
            {
                currentWaypoint++;
                LookAForNextState(AIstate.IDLE);
            }
        }
	}

    public void CallAttack(){
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        isAttacking = false;
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(_attackCoolDown);
        isAttacking = true;
    }

    void ChangeAttackAnim(){
        anim.SetBool("Attack", false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, 100);
        Gizmos.DrawSphere(transform.position, _attackRadius);
    }

    //if player moves out of range, go to the nearest stored waypoint
    Transform GetNearestWaypoints(){
		float distance;
		float nearestDistance = 0;
		for(int i =0; i<wayPoints.Length; ++i){
			distance = Vector3.Distance(transform.position, wayPoints[i].position);
			if(distance < nearestDistance)
			{
				nearestDistance = distance;
				moveTarget = wayPoints[i];
			}
		}
		return moveTarget;
	}

    IEnumerator Idle()
    {
        nma.isStopped = true;
        isPatrolling = false;
        isAttacking = false;
        yield return new WaitForSeconds(_waitTime);
        nma.isStopped = false;
        LookAForNextState(AIstate.PATROL);
    }

	private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
            moveTarget = GetNearestWaypoints();
            LookAForNextState(AIstate.PATROL);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            LookAForNextState(AIstate.ATTACK);
        }
    }
}
