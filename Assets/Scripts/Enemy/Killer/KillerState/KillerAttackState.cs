using UnityEngine;
using UnityEngine.AI;

public class KillerAttackState : EnemyState, ITargetInFOV
{
    private Vector3 directionToPlayer;
    private EnemyData curData;

    public override void OnEnterState(Enemy enemy)
    {
        base.OnEnterState(enemy);

        curEnemy.EnemyAnimator.SetBool("isMove", true);

        curData = curEnemy.EnemyData;

        curEnemy.Agent.speed = curData.runSpeed;
        curEnemy.Agent.isStopped = false;

        curEnemy.EnemyAnimator.speed = curEnemy.Agent.speed / curData.walkSpeed;
    }

    public override void OnUpdateState()
    {
        Player player = CharacterManager.Instance.Player;

        if ((curData.attackDistance > curEnemy.PlayerDistance) && IsTargetInFieldOfView())
        {
            curEnemy.Agent.isStopped = true;

            if (curData.attackRate < (Time.time - curEnemy.LastAttackTime))
            {
                curEnemy.LastAttackTime = Time.time;

                curEnemy.EnemyAnimator.speed = 1f;

                curEnemy.EnemyAnimator.SetBool("isMove", false);
                curEnemy.EnemyAnimator.SetTrigger("Attack");
            }
        }
        else
        {
            curEnemy.EnemyAnimator.SetBool("isMove", true);

            if (curData.detectDistance > curEnemy.PlayerDistance)
            {
                curEnemy.Agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();

                if (curEnemy.Agent.CalculatePath(player.transform.position, path))
                    curEnemy.Agent.SetDestination(player.transform.position);
                else
                {
                    curEnemy.Agent.SetDestination(curEnemy.transform.position);
                    curEnemy.Agent.isStopped = true;
                    curEnemy.SetState((int)KillerState.Wandering);
                }
            }
            else
            {
                curEnemy.Agent.SetDestination(curEnemy.transform.position);
                curEnemy.Agent.isStopped = true;
                curEnemy.SetState((int)KillerState.Wandering);
            }
        }
    }

    public override void OnExitState()
    {
        curEnemy.EnemyAnimator.SetBool("isMove", false);
    }

    public bool IsTargetInFieldOfView()
    {
        directionToPlayer = CharacterManager.Instance.Player.transform.position - curEnemy.transform.position;

        float angle = Vector3.Angle(curEnemy.transform.forward, directionToPlayer);

        return angle < (curEnemy.EnemyData.fieldOfView * 0.5f);
    }
}