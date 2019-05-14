using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public bool playTransition;

    public float lerpSpeed = 1.25f;

    public GameObject creditsObj;
    public GameObject mainObj;
    public GameObject cutsceneObj;

    public Transform cameraTransform;
    public Transform startPos;
    public Transform originalPos;

    public float offset = .2f;
    public float breathingSpeed = .3f;

    Vector3 randomPosition;
    bool inPlace;
	void Start ()
    {
        cameraTransform = Camera.main.transform;
        cameraTransform.position = startPos.position;
        randomPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(4.5f, 5.5f), cameraTransform.position.z);
    }
	
	void Update ()
    {
        if (playTransition)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, originalPos.position, Time.deltaTime * lerpSpeed);
        }
        if (Vector3.Distance(cameraTransform.position, originalPos.position) < 0.025f)
        {
            playTransition = false;
            if (!inPlace)
            {
                inPlace = true;
                cameraTransform.position = originalPos.position;
            }
        }

        if (inPlace)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, randomPosition, Time.deltaTime * breathingSpeed);

            if (Vector3.Distance(cameraTransform.position, randomPosition) < offset)
            {
                randomPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(4.66f, 5.1f), cameraTransform.position.z);
            }
        }
	}

    public void OnStartPressed()
    {
        cutsceneObj.SetActive(true);
    }

    public void OnReturnPressed()
    {
        mainObj.SetActive(true);
        creditsObj.SetActive(false);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    public void OnCreditPressed()
    {
        mainObj.SetActive(false);
        creditsObj.SetActive(true);
    }
}
