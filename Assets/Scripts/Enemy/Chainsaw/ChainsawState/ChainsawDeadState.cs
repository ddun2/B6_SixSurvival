using UnityEngine;

public class ChainsawDeadState : EnemyState
{
    public override void OnEnterState(Enemy enemy)
    {
        base.OnEnterState(enemy);

        curEnemy.EnemyAnimator.SetTrigger("Dead");

        curEnemy.Agent.SetDestination(curEnemy.transform.position);

        curEnemy.gameObject.layer = LayerMask.GetMask("Invincibillity");
    }

    public override void OnUpdateState()
    {

    }

    public override void OnExitState()
    {
        curEnemy.gameObject.layer = LayerMask.GetMask("Enemy");
    }
}