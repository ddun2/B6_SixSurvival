using UnityEngine;

[CreateAssetMenu(fileName = "KnifeData", menuName = "Data/Knife", order = 0)]
public class KnifeData : ScriptableObject
{
    [Header("Knife Info")]
    public string KnifeNameTag;
    public int power;
    public float speed;
    public float duration;
    public LayerMask target;
}