using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    private bool isPreviewActive = false;
    private GameObject previewObject;
    private Preview preview;
    public UIBuilding uiBuilding;

    private Transform player;

    private int buildingIndex;
    private RaycastHit hit;
    private Vector3 position;

    private void Start()
    {
        player = CharacterManager.Instance.Player.transform;
    }
   
    private void Update()
    {        
        if (isPreviewActive)
        {
            PreviewPosition();
        }

        // �ӽ÷� �����ϰ� �����ص� Ű �����ϱ�

        if (Input.GetMouseButton(0))
        {
            Build();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BuildCancel();
        }

        if(Input.GetKeyDown(KeyCode.B) && isPreviewActive)
        {
            BuildCancel();
        }

        if (Input.GetKey(KeyCode.Q) && isPreviewActive)
        {            
            RotateBuilding(false);
        }

        if (Input.GetKey(KeyCode.E) && isPreviewActive)
        {         
            RotateBuilding(true);
        }
    }

    // ���๰ Ŭ�� ��
    public void OnClickSlot(int index)
    {        
        buildingIndex = index;
        // ���� �޴� ����
        uiBuilding.Toggle();
        CharacterManager.Instance.Player.controller.ToggleCursor();
        // ������ ������Ʈ ���� (�÷��̾� ��ġ, Ŀ�� ���󰡵���)
        previewObject = Instantiate(uiBuilding.buildingSO[index].previewPrefab, player.position + player.forward, uiBuilding.buildingSO[index].previewPrefab.transform.rotation);
        preview = previewObject.GetComponent<Preview>();
        isPreviewActive = true;
    }

    private void PreviewPosition()
    {        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            position = hit.point;
            position.Set(Mathf.Round(position.x / 0.1f) * 0.1f, Mathf.Round(position.y / 0.1f) * 0.1f, Mathf.Round(position.z / 0.1f) * 0.1f);
            previewObject.transform.position = position;           
        }
    }

    private void Build()
    {
        if (isPreviewActive && preview.IsBuildable())
        {
            Instantiate(uiBuilding.buildingSO[buildingIndex].prefab, position, previewObject.transform.rotation);
            Destroy(previewObject);
            isPreviewActive = false;            
        }        
    }

    private void BuildCancel()
    {
        if (isPreviewActive)
        {
            Destroy(previewObject);
        }
        isPreviewActive= false;
    }

    private void RotateBuilding(bool right)
    {
        if(right)
        {
            previewObject.transform.Rotate(0.0f, 90.0f * Time.deltaTime, 0.0f);
        }
        else
        {
            previewObject.transform.Rotate(0.0f, -90.0f * Time.deltaTime, 0.0f);
        }
    }
}
