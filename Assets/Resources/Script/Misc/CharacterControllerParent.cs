using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class CharacterControllerParent : MonoBehaviour {


	[Header("Player Stats")]
	[Header("Health")]
	public int _maxHealth;
	public int _currentHealth;

    [Space]
    public Image healthBar;

	[Space(5)]

	[Header("Damage")]
	public int _damage;
	public int _damageBonus;

	[Space(5)]

	[Header("Player Animation")]
	public Animator anim;
	//public List<ItemType> _inventory;

	[Space(10)]

	[Header("Movement Speed Data")]
	public float _moveSpeed;

	[Space(10)]

	[Header("Character Attack Data")]
	public LayerMask _hitLayer;
	public float _attackRadius;

    [Header("Spawned Prefab")]
    public GameObject hitPrefab;

    //private vars
    //character attacking data
    RaycastHit hitInfo;

	public void TakeDamage(Transform hitTransform, bool isPlayer, int _damage, int _damageBonus){
		if(_currentHealth > 0)
        {
			int totalDmg = _damage + _damageBonus;
            _currentHealth -= totalDmg;

            if(healthBar)
                healthBar.fillAmount -= (1f * (float)(totalDmg/(float)_maxHealth));

            GameObject temp = Instantiate(hitPrefab, hitTransform.position + new Vector3(0, 2.5f, 0), Quaternion.identity);

            TMP_Text tempText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
            tempText.text = totalDmg.ToString();

            if (isPlayer)
                temp.transform.GetChild(0).GetComponent<TextMeshPro>().faceColor = new Color(0, 255, 0, 1);
            else
                temp.transform.GetChild(0).GetComponent<TextMeshPro>().faceColor = new Color(255, 0, 0, 1);
        }else{
            if(isPlayer){
                GameManager.instance.SpawnItem(hitTransform);
                Destroy(hitTransform.gameObject);
            }
        }
	}

    public virtual void Attack()
    {
        int i = 0;
        Collider[] type = Physics.OverlapSphere(transform.position, _attackRadius, _hitLayer);

        HashSet<GameObject> enemies = new HashSet<GameObject>();

        while (type.Length > i)
        {
            if (type[i].GetComponent<CharacterControllerParent>() && !enemies.Contains(type[i].gameObject))
            {
                type[i].GetComponent<CharacterControllerParent>().TakeDamage(type[i].transform, false, _damage, _damageBonus);
                enemies.Add(type[i].gameObject);
            }
            ++i;
        }
    }
}
