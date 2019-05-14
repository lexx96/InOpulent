using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour {

    public Animator anim;

	void Start ()
    {
        anim.GetComponent<Animator>();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    public void LoadGame()
    {
        gameObject.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
