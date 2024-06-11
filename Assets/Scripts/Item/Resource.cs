using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ItemData itemToGive;
    [SerializeField] private int quantityPerHit = 1;
    [SerializeField] private int capacy;

    public ResourceSpawn spawn;
    private float respawnTime = 5f;
    private int curCapacy;

    private void Start()
    {
        curCapacy = capacy;
    }

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for(int i = 0; i < quantityPerHit; i++)
        {
            if (curCapacy <= 0)
            {
                CoroutineHandler.StartCoroutineHandler(DestroyResource());
                break;
            }
            curCapacy -= 1;
            Instantiate(itemToGive.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up));
        }
    }

    private IEnumerator DestroyResource()
    {
        gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnTime);
        curCapacy = capacy;
        gameObject.SetActive(true);


    }
}
