using System.Collections;
using UnityEngine;

public class KillerIdleState : EnemyState
{
    CoroutineHandler corHandler;

    public override void OnEnterState(Enemy enemy)
    {
        base.OnEnterState(enemy);

        EnemyData curData = curEnemy.EnemyData;

        curEnemy.Agent.speed = curData.walkSpeed;
        curEnemy.Agent.isStopped = true;

        curEnemy.EnemyAnimator.speed = curEnemy.Agent.speed / curData.walkSpeed;

        corHandler = CoroutineHandler.StartCoroutineHandler(WanderToNewLocation(Random.Range(curData.minWanderWaitTime, curData.maxWanderWaitTime)));
    }

    public override void OnUpdateState()
    {
        if (curEnemy.EnemyData.detectDistance > curEnemy.PlayerDistance)
            curEnemy.SetState((int)KillerState.Attack);
    }

    public override void OnExitState()
    {
        if (null != corHandler)
            corHandler.Stop();
    }

    private IEnumerator WanderToNewLocation(float time)
    {
        yield return new WaitForSeconds(time);

        curEnemy.SetState((int)KillerState.Wandering);

        yield return null;
    }
}
