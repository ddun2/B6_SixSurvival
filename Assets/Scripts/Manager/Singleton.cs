using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (null == instance)
            {
                GameObject singletonObject = GameObject.Find(typeof(T).Name);

                if (null == singletonObject)
                {
                    singletonObject = new GameObject(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }
                else
                    instance = singletonObject.AddComponent<T>();
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(transform.root.gameObject);
    }
}
