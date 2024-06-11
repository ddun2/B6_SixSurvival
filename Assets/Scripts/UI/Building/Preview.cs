using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    // 충돌한 오브젝트들을 저장
    private List<Collider> colliders = new List<Collider>();

    private int groundLayer = 3;
    private int buildingLayer = 30;


    [SerializeField]
    private Material[] materials = new Material[2];

    private void Update()
    {
        ChangeColor();        
    }

    private void ChangeColor()
    {        
        if (colliders.Count > 0)
        {            
            SetColor(0);
        }
        else
        {
            SetColor(1);
        }
    }

    private void SetColor(int color)
    {
        Renderer[] obj = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < obj.Length; i++)
        {            
            obj[i].material = materials[color];         
        }        
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.layer != groundLayer && other.gameObject.layer != buildingLayer)
        {
            colliders.Add(other);            
        }
    }

    private void OnTriggerExit(Collider other)
    {   
        if (other.gameObject.layer != groundLayer && other.gameObject.layer != buildingLayer)
        {
            colliders.Remove(other);
        }
    }

    public bool IsBuildable()
    {
        if (colliders.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
