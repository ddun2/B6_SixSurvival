using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamagable, IDamageFlash
{
    public EnemyData EnemyData { get { return enemyData; } }
    public NavMeshAgent Agent { get { return agent; } }
    public Animator EnemyAnimator { get { return animator; } }
    public float LastAttackTime { get { return lastAttackTime; } set { lastAttackTime = value; } }
    public float PlayerDistance { get { return playerDistance; } }
    public EnemyState CurState { get { return curState; } }

    protected float curHealth;
    protected float playerDistance;
    protected float lastAttackTime;

    protected NavMeshAgent agent;
    protected Animator animator;
    protected SkinnedMeshRenderer[] meshRenderers;

    protected EnemyState curState;

    [SerializeField] protected EnemyData enemyData;

    protected Dictionary<int, EnemyState> enemyStates;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        enemyStates = new Dictionary<int, EnemyState>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        curHealth = enemyData.maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);
    }

    public void SetState(int indexKey)
    {
        if (null != curState)
            curState.OnExitState();

        curState = enemyStates[indexKey];

        curState.OnEnterState(this);
    }

    public void TakePhysicalDamage(int damage)
    {
        curHealth -= damage;

        if (0 >= curHealth)
            Die();
        else
            Hit();

        StartCoroutine(DamageFlash());
    }

    public IEnumerator DamageFlash()
    {
        for (int i = 0; i < meshRenderers.Length; ++i)
            meshRenderers[i].material.color = new Color(1f, 0.6f, 0.6f);

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < meshRenderers.Length; ++i)
            meshRenderers[i].material.color = Color.white;
    }

    public abstract void InitializeEnemy();

    protected abstract void Die();

    protected abstract void Hit();

    protected abstract void OnStartEnd();
    
    protected abstract void OnHitEnd();

    protected abstract void OnDeadEnd();
}
