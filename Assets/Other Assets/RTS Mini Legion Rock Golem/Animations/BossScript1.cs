using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using EZCameraShake;

public class BossScript1 : CharacterControllerParent {

    public enum BossBehaviour
    {
        IDLE,
        TAUNT,
        FALLINGROCKS,
        ATTACK01,
        ATTACK02
    };

    public BossBehaviour state;

    [Header("Boss attack variables")]
    [SerializeField, Range(1, 5)]
    float _attackCoolDown;
    [SerializeField, Range(1, 5)]
    float _waitTime;

    [Header("Reference")]
    Transform player;
    public GameObject bossCanvas;
    public GameObject fallingRocks;
    [SerializeField]
    int amountOfRocks;
    [SerializeField]
    float offset;

    [Header("WarningUI")]
    public Image tauntingUI;
    public Transform attack1BG;
    public Transform attack1UI;

    #region Private Variables
    bool isTaunting;
    bool inRange;
    bool isReady;
    bool runOnce;

    float distanceToPlayer;
    float cooldownTimer;
    #endregion

    #region Start & Awake
    void Start()
    {
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        LookAForNextState(state);
    }
    #endregion

    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer < 10)
        {
            runOnce = false;
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        #region Death Trigger
        if (_currentHealth <= 0)
        {
            bossCanvas.SetActive(false);
            anim.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
            StartCoroutine(EndGame());
        }
        #endregion
    }

    void LookAForNextState(BossBehaviour currentState)
    {
        state = currentState;
        switch (currentState)
        {
            case BossBehaviour.IDLE:
                StartCoroutine(Idle()); 
                break;
            case BossBehaviour.TAUNT:
                StartCoroutine(TauntWait());
                break;
            case BossBehaviour.FALLINGROCKS:
                if (!runOnce)
                {
                    runOnce = true;
                    StartCoroutine(SummonFallingRock());
                }
                break;
            case BossBehaviour.ATTACK01:
                StartCoroutine(Attack1());
                break;
            case BossBehaviour.ATTACK02:
                StartCoroutine(Attack2());
                break;
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    #region Idle
    IEnumerator Idle()
    {
        yield return new WaitForSeconds(_waitTime);
        if (inRange && state != BossBehaviour.FALLINGROCKS)
        {
            LookAForNextState(BossBehaviour.ATTACK01);  
        }
        else if (!inRange && state == BossBehaviour.IDLE)
        {
            LookAForNextState(BossBehaviour.FALLINGROCKS);
        }
    }
    #endregion

    #region Taunt
    IEnumerator TauntWait()
    {
        isTaunting = true;
        anim.SetTrigger("Taunt");
        while (isTaunting)
        {
            if (tauntingUI.transform.localScale.x <= 9)
            {
                tauntingUI.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 5;
            }
            else
            {
                tauntingUI.transform.localScale = new Vector3(0, 0, 0);
                isTaunting = false;
            }
            yield return null;
        }
    }
    #endregion

    #region Summon Falling Rock
    IEnumerator SummonFallingRock()
    {
        while (!inRange)
        {
            if (cooldownTimer <= .5f)
            {
                cooldownTimer += Time.deltaTime;
            }
            else
            {
                cooldownTimer = 0;
                anim.SetTrigger("Jump");
            }
            yield return null;
        }
        yield return new WaitForSeconds(_waitTime);
        LookAForNextState(BossBehaviour.IDLE);
    }
    #endregion

    #region Attack01
    IEnumerator Attack1()
    {
        attack1BG.localScale = new Vector3(1, 1, 1);
        while (inRange && state == BossBehaviour.ATTACK01)
        {
            if (attack1UI.localScale.x <= 1)
            {
                attack1UI.localScale += new Vector3(1, 1, 1) * Time.deltaTime * (_attackCoolDown / 10);
            }
            else
            {
                attack1UI.localScale = new Vector3(0, 0, 0);
                anim.SetTrigger("Attack1");
            }
            yield return null;
        }
        attack1BG.localScale = new Vector3(0, 0, 0);
        attack1UI.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(_waitTime);
        LookAForNextState(BossBehaviour.IDLE);
    }
    #endregion

    IEnumerator Attack2()
    {
        yield return null;
    }

    #region Ref
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0, 0, 100);
        Gizmos.DrawSphere(transform.position, _attackRadius);
    }

    //IEnumerator waitFunction2()
    //{
    //    const float waitTime = 3f;
    //    float counter = 0f;

    //    Debug.Log("Hello Before Waiting");
    //    while (counter < waitTime)
    //    {
    //        Debug.Log("Current WaitTime: " + counter);
    //        counter += Time.deltaTime;
    //        yield return null; //Don't freeze Unity
    //    }
    //    Debug.Log("Hello After waiting for 3 seconds");
    //}

    public void ShakeCameraOnce()
    {
        CameraShaker.Instance.ShakeOnce(5, 5, 0.1f, 7.5f);
        for(int i=0; i< amountOfRocks; ++i){
            float randX = Random.Range(player.transform.position.x - offset, player.transform.position.x + offset);
            float randZ = Random.Range(player.transform.position.z - offset, player.transform.position.z + offset);

            Instantiate(fallingRocks, new Vector3(randX, 1.45f, randZ), bossCanvas.transform.rotation, bossCanvas.transform);
        }
    }

    public void IsReady()
    {
        LookAForNextState(BossBehaviour.IDLE);
    }
    #endregion
}