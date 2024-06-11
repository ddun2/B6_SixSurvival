using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    protected Enemy curEnemy { get; private set; }

    public virtual void OnEnterState(Enemy enemy)
    {
        curEnemy = enemy;
    }

    public abstract void OnUpdateState();
    public abstract void OnExitState();

    protected Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        Vector3 curPos = curEnemy.transform.position;
        EnemyData curData = curEnemy.EnemyData;

        NavMesh.SamplePosition(curPos + (Random.onUnitSphere * Random.Range(curData.minWanderDistance, curData.maxWanderDistance)),
            out hit, curData.maxWanderDistance, NavMesh.AllAreas);

        int i = 0;

        do
        {
            NavMesh.SamplePosition(curPos + (Random.onUnitSphere * Random.Range(curData.minWanderDistance, curData.maxWanderDistance)),
                out hit, curData.maxWanderDistance, NavMesh.AllAreas);

            ++i;

        } while ((curData.detectDistance > Vector3.Distance(curPos, hit.position)) && (30 > i));

        return hit.position;
    }
}