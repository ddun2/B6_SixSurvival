using UnityEngine;
using UnityEngine.AI;

public class KillerWanderingState : EnemyState
{
    public override void OnEnterState(Enemy enemy)
    {
        base.OnEnterState(enemy);

        curEnemy.EnemyAnimator.SetBool("isMove", true);

        EnemyData curData = curEnemy.EnemyData;

        curEnemy.Agent.speed = curData.walkSpeed;
        curEnemy.Agent.isStopped = false;

        curEnemy.EnemyAnimator.speed = curEnemy.Agent.speed / curData.walkSpeed;

        curEnemy.Agent.SetDestination(GetWanderLocation());
    }

    public override void OnUpdateState()
    {
        if (0.05f > curEnemy.Agent.remainingDistance)
            curEnemy.SetState((int)KillerState.Idle);

        if (curEnemy.EnemyData.detectDistance > curEnemy.PlayerDistance)
            curEnemy.SetState((int)KillerState.Attack);
    }

    public override void OnExitState()
    {
        curEnemy.EnemyAnimator.SetBool("isMove", false);
    }
}