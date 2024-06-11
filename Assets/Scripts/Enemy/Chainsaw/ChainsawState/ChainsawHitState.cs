using UnityEngine;

public class ChainsawHitState : EnemyState
{
    public override void OnEnterState(Enemy enemy)
    {
        base.OnEnterState(enemy);

        curEnemy.EnemyAnimator.SetTrigger("Hit");

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