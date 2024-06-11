using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    IEnumerator enumerator;
    Coroutine curCor;

    private void Coroutine(IEnumerator cor)
    {
        enumerator = cor;
        curCor = StartCoroutine(enumerator);
    }

    private void Update()
    {
        if ((null != enumerator) && (null == enumerator.Current))
            Destroy(gameObject);
    }

    public void Stop()
    {
        StopCoroutine(curCor);
        Destroy(gameObject);
    }

    public static CoroutineHandler StartCoroutineHandler(IEnumerator cor)
    {
        GameObject obj = new GameObject("CoroutineHandler");
        CoroutineHandler handler = obj.AddComponent<CoroutineHandler>();

        if (handler)
            handler.Coroutine(cor);

        return handler;
    }
}
