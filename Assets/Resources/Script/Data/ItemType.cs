using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour {
	public string itemName;
	public Sprite itemSprite;

	/*
	itemDatabase cant access this I give up :c

	public enum Items{
		sword,
		shield,
		outfit
	};
	public Items item;
	
	*/

	public bool isEquipped;
	public bool isOwned;

	[Header("Item Stats")]
	public int damageBonus;
	public int healthBonus;
	public bool isShield;
}
