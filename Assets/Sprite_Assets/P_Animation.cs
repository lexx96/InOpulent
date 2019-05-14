using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Animation : MonoBehaviour {
    /// <summary>
    /// This script is only for testing purposes.
    /// The differences in timing for "Swing" sprite sheet animation is 1 : 0.2. If "Swing" is 0.5 then "Weapon will be" 0.1.
    /// </summary>
    [Header("For Testing Purposes")]
    [Tooltip("Script for testing only.")]
    public GameObject weaponController;
    public Animator playerAnimator;
    public Animator thisAnimator;

	void Start () {}
	
	void Update ()
    {
        //print(playerAnimator.GetAnimatorTransitionInfo(0).IsName("Idle -> Sword"));
        //print(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));

        if (Input.GetMouseButtonDown(0) && (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            //weaponController.SetActive(true);
            thisAnimator.SetBool("Swing", true);
        }

        if (playerAnimator.GetNextAnimatorStateInfo(0).IsName("Idle") || playerAnimator.GetNextAnimatorStateInfo(0).IsName("Walk"))
        {
            thisAnimator.SetBool("Swing", false);
        }
	}

    public void SetSwordFalse()
    {
        //gameObject.SetActive(false);
    }

    public void SetAttackFalse()
    {
        //playerAnimator.SetBool("Attack", false);
    }
}
