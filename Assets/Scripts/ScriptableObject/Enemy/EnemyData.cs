using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEnemy", menuName = "Data/Enemy/Default", order = 0)]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public int maxHealth;
    public float walkSpeed;
    public float runSpeed;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    public float attackDistance;
    public LayerMask targetLayer;

    [Header("AI")]
    public float detectDistance;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("FieldOfView")]
    [Range(0f, 360f)] public float fieldOfView;
}