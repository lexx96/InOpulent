using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLayer : MonoBehaviour {

    private GameObject Player;
    private bool playOnce = true;
    private Color sprite_Color;

	void Start ()
    {
        Player = GameObject.Find("Player");
        sprite_Color = GetComponent<SpriteRenderer>().color;

    }
	
	void Update ()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < 3.6f)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 2;
            GetComponent<Animator>().enabled = true;

            if (sprite_Color.a > 0.5f)
            {
                sprite_Color.a -= Time.deltaTime * 2f;
                GetComponent<SpriteRenderer>().color = sprite_Color;
            }

            if (playOnce)
            {
                playOnce = false;
                GetComponent<Animator>().SetTrigger("Touched");
            }
        }
        else
        {
            playOnce = true;
            GetComponent<SpriteRenderer>().sortingOrder = 1;

            if (sprite_Color.a < 1)
            {
                sprite_Color.a += Time.deltaTime * 2f;
                GetComponent<SpriteRenderer>().color = sprite_Color;
            }

            if (GetComponent<Animator>().isActiveAndEnabled)
            {
                GetComponent<Animator>().enabled = false;
            }
        }
	}
}
