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

        // 임시로 간단하게 지정해둔 키 수정하기

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

    // 건축물 클릭 시
    public void OnClickSlot(int index)
    {        
        buildingIndex = index;
        // 건축 메뉴 종료
        uiBuilding.Toggle();
        CharacterManager.Instance.Player.controller.ToggleCursor();
        // 프리뷰 오브젝트 생성 (플레이어 위치, 커서 따라가도록)
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
