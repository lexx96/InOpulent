using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LoadCave : MonoBehaviour {

    public Transform spawnTransform;

    public GameObject BossObj;

    public GameObject[] loadBlockLights;

    void Start()
    {
        loadBlockLights[0].SetActive(true);
        loadBlockLights[1].SetActive(true);
        loadBlockLights[2].SetActive(true);
        loadBlockLights[3].SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.name == "LoadCave")
        {
            PlayAudio.caveBGM = true;
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.transform.position = spawnTransform.position;
            other.GetComponent<NavMeshAgent>().enabled = true;
        }
        else if (other.CompareTag("Player") && gameObject.name == "LoadBoss")
        {
            PlayAudio.bossBGM = true;
            BossObj.SetActive(true);
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.transform.position = spawnTransform.position;
            other.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
