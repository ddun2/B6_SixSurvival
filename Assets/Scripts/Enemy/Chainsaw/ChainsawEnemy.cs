public enum ChainsawState
{
    Idle,
    Wandering,
    Attack,
    Hit,
    Dead
}

public class ChainsawEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        enemyStates.Add((int)ChainsawState.Idle, new ChainsawIdleState());
        enemyStates.Add((int)ChainsawState.Wandering, new ChainsawWanderingState());
        enemyStates.Add((int)ChainsawState.Attack, new ChainsawAttackState());
        enemyStates.Add((int)ChainsawState.Hit, new ChainsawHitState());
        enemyStates.Add((int)ChainsawState.Dead, new ChainsawDeadState());
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
        SetState((int)ChainsawState.Dead);
    }

    protected override void Hit()
    {
        SetState((int)ChainsawState.Hit);
    }

    protected override void OnStartEnd()
    {
        SetState((int)ChainsawState.Idle);
    }

    protected override void OnHitEnd()
    {
        SetState((int)ChainsawState.Idle);
    }

    protected override void OnDeadEnd()
    {
        SetState((int)ChainsawState.Idle);

        gameObject.SetActive(false);
    }

    public override void InitializeEnemy()
    {
        curHealth = enemyData.maxHealth;
    }
}