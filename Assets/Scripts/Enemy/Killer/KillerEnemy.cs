using UnityEngine;

public enum KillerState
{
    Idle,
    Wandering,
    Attack,
    Hit,
    Dead
}

public class KillerEnemy : Enemy
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private KnifeData knifeData;

    private Vector3 ToPlayer;

    protected override void Awake()
    {
        base.Awake();

        enemyStates.Add((int)KillerState.Idle, new KillerIdleState());
        enemyStates.Add((int)KillerState.Wandering, new KillerWanderingState());
        enemyStates.Add((int)KillerState.Attack, new KillerAttackState());
        enemyStates.Add((int)KillerState.Hit, new KillerHitState());
        enemyStates.Add((int)KillerState.Dead, new KillerDeadState());
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (null != curState)
            curState.OnUpdateState();
    }

    protected override void Die()
    {
        SetState((int)KillerState.Dead);
    }

    protected override void Hit()
    {
        SetState((int)KillerState.Hit);
    }

    protected override void OnStartEnd()
    {
        SetState((int)KillerState.Idle);
    }

    protected override void OnHitEnd()
    {
        SetState((int)KillerState.Idle);
    }

    protected override void OnDeadEnd()
    {
        SetState((int)KillerState.Idle);

        gameObject.SetActive(false);
    }

    private void OnAttack()
    {
        GameObject knife = GameManager.Instance.ObjectPool.SpawnFromPool(knifeData.KnifeNameTag);

        knife.transform.position = attackTransform.position;

        Knife knifeController = knife.GetComponent<Knife>();

        ToPlayer = (CharacterManager.Instance.Player.transform.position + Vector3.up) - attackTransform.position;

        knifeController.InitializeAttack(ToPlayer, knifeData);
    }

    public override void InitializeEnemy()
    {
        curHealth = enemyData.maxHealth;
    }
}
