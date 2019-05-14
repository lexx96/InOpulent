using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public enum PowerUpType
    {
        health,
        diamond
    };
    public PowerUpType powerType;

    [SerializeField]
    int amount;

    [SerializeField]
    float rotateSpeed;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (powerType)
            {
                case PowerUpType.diamond:
                    GameManager.instance.UpdateDiamond(amount);
                    break;
                case PowerUpType.health:
                    other.GetComponent<PlayerController>().UpdateHealth(amount);
                    break;
            }
        }
        Destroy(this.gameObject);
    }
}
