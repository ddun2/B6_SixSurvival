using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool ObjectPool { get { return objPool; } }

    private ObjectPool objPool;

    protected override void Awake()
    {
        base.Awake();

        objPool = GetComponent<ObjectPool>();
    }
}