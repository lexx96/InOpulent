using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : CharacterControllerParent
{

    [Header("Shield Data")]
    public bool shieldInHand;
    public GameObject shield;

    [Space(5)]

    [Header("Weapon Data")]
    public GameObject sword;
    public GameObject defaultSword;

    [Space(5)]

    [Header("Outfit Data")]
    public GameObject outfit;

    public GameObject defaultOutfit;

    [Space(5)]

    [SerializeField]
    float attackForce;

    //private data
    float moveX, moveZ;

    Vector3 moveDir;

    bool blocking;
    bool canAttack;

    int _originalDamage;
    float _originalMoveSpeed;

    //character attacking data
    Collider[] type;
    Rigidbody rb;
    CharacterController player;
    NavMeshAgent navMeshAgent;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _originalMoveSpeed = _moveSpeed;
        _originalDamage = _damage;
        canAttack = true;
        rb = GetComponent<Rigidbody>();
        player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        moveDir = new Vector3(0, 0, moveZ);
        moveDir = transform.TransformDirection(moveDir * Time.deltaTime * _moveSpeed);

        if (_currentHealth <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        Move();

        if (Input.GetMouseButtonDown(0))
        {
            if (canAttack)
            {
                _moveSpeed = 0;
                anim.SetBool("Attack", canAttack);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            canAttack = !canAttack;
            anim.SetBool("Attack", canAttack);
        }

        if (Input.GetMouseButton(1) && shieldInHand)
            blocking = true;

        if (Input.GetMouseButtonUp(1))
            blocking = false;

        if (moveZ != 0)
        {
            anim.SetBool("Move", true);
        }
        else if (moveZ == 0)
            anim.SetBool("Move", false);
    }

    private void FixedUpdate()
    {

    }

    void LateUpdate()
    {
        canAttack = !blocking;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 255, 0, 100);
        Gizmos.DrawSphere(transform.position, _attackRadius);
    }

    void Move()
    {
        anim.SetFloat("Speed", moveZ);
        transform.Rotate(0, _moveSpeed * 10 * Time.deltaTime * moveX, 0);
        player.Move(moveDir);
    }

    public override void Attack()
    {
        canAttack = !canAttack;
        anim.SetBool("Attack", canAttack);

        HashSet<GameObject> enemies = new HashSet<GameObject>();

        int i = 0;
        type = Physics.OverlapSphere(transform.position, _attackRadius, _hitLayer);
        while (type.Length > i)
        {
            if (type[i].GetComponent<CharacterControllerParent>() && !enemies.Contains(type[i].gameObject))
            {
                type[i].GetComponent<CharacterControllerParent>().TakeDamage(type[i].transform, true, _damage, _damageBonus);
                enemies.Add(type[i].gameObject);
            }
            ++i;
        }
        _moveSpeed = _originalMoveSpeed;
    }
    

    public void UpdateHealth(int amount)
    {
        _currentHealth += amount;
        healthBar.fillAmount += (1f * ((float)amount/(float)_maxHealth));

        if (_currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;
        
    }
}
