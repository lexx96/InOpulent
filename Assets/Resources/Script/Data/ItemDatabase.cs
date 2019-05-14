using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDatabase : MonoBehaviour {

	public static ItemDatabase instance;

	[Header("Player Controller Reference")]
	[Tooltip("You do not need to drag and drop the player unless u did not name your player as Player")]
	public PlayerController player;

	[Space(10)]
	[Header("List of all weapons in game")]
	public List<ItemType> items;

	public ItemType selectedItem;

	[Header("Inventory UI")]

	[SerializeField]
	Transform inventoryPanel;

	[Header("Item text")]

	[SerializeField]
	TextMeshProUGUI itemNameTxt;
	[SerializeField]
	TextMeshProUGUI damageBonusTxt;
	[SerializeField]
	TextMeshProUGUI healthBonusTxt;

	[Space(5)]

	[Header("Items that are not owned")]

	[SerializeField]
	Sprite notOwned;

	[SerializeField]
	GameObject itemButton; //button gameObject
	[SerializeField]
	Transform itemButtonParent;

	void Awake()
	{
		instance = this; //hopefully I got a reason to use this else I remove it tmr
		if(!player)
			player= GameObject.Find("Player").GetComponent<PlayerController>();
	}

	public void GetInventoryItemData(){
		SpawnItemButton();

		for(int i=0; i<items.Count; ++i){
			if(items[i].isOwned){
				items[i].GetComponent<Image>().sprite = items[i].itemSprite;
				items[i].GetComponent<Button>().interactable = true;
			}
			else{
				items[i].GetComponent<Image>().sprite = notOwned;
				items[i].GetComponent<Button>().interactable = false;
			}
		}	
	}

	void SpawnItemButton(){
		for(int i=0; i< items.Count; ++i){
			GameObject newItemButon = Instantiate(itemButton, itemButtonParent) as GameObject;
			ItemType type = newItemButon.GetComponent<ItemType>();
			type = items[i];

			if(type.isShield)
				newItemButon.GetComponent<Button>().onClick.AddListener( delegate { EquipItem(type.isShield); });
			else
				newItemButon.GetComponent<Button>().onClick.AddListener( delegate { EquipItem(type.damageBonus, type.healthBonus); });
		}
	}

	public void SelectItem(){
		selectedItem = GetComponent<ItemType>();
		itemNameTxt.text = selectedItem.itemName;
		damageBonusTxt.text = selectedItem.damageBonus.ToString();
		healthBonusTxt.text = selectedItem.healthBonus.ToString();
	}

	public void EquipItem(bool hasShield){
		player.shieldInHand = hasShield;
		//player.shieldSprite = selectedItem.itemSprite;
		selectedItem.isEquipped = true;
	}


	public void EquipItem(int damageBonus, int healthBonus){
		player._damageBonus = damageBonus;
		player._maxHealth = healthBonus;
		//player.weaponSprite = selectedItem.itemSprite;
		selectedItem.isEquipped = true;
	}

	/*
	public void UnEquipAllItem(){
		player._damageBonus -= selectedItem.damageBonus;
		player.weaponSprite = player.defaultWeaponSprite;

		player._maxHealth -= selectedItem.healthBonus;
		player.outfitSprite = player.defaultOutfitSprite;

		if(selectedItem.isShield){
			player.shieldSprite = null;
			player.shieldInHand = false;
		}

		selectedItem.isEquipped = false;
	}

	public void UnEquipWeapon(){
		player._damageBonus -= selectedItem.damageBonus;
		player.weaponSprite = player.defaultWeaponSprite;
		selectedItem.isEquipped = false;
	}
	public void UnEquipShield(){
		if(selectedItem.isShield){
			player.shieldSprite = null;
			player.shieldInHand = false;
		}
		selectedItem.isEquipped = false;
	}
	public void UnEquipOutfit(){
		player._maxHealth -= selectedItem.healthBonus;
		player.outfitSprite = player.defaultOutfitSprite;
		selectedItem.isEquipped = false;
	}
	*/
}
