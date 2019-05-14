using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

    public static bool caveBGM;
    public static bool bossBGM;

    public AudioClip[] clips;

    AudioSource aSource;

	void Start ()
    {
        aSource = GetComponent<AudioSource>();
        aSource.Play();
    }

    void Update()
    {
        if (caveBGM)
        {
            caveBGM = false;
            aSource.clip = clips[0];
            aSource.Play();
        }
        else if (bossBGM)
        {
            bossBGM = false;
            aSource.clip = clips[1];
            aSource.Play();
        }
    }
}
