using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIBuilding : MonoBehaviour
{
    [Header("UISetting")]

    public GameObject slotPrefab;
    public Transform slotPanel;
    public BuildingData[] buildingSO;

    private GameObject buildingWindow;    
    private PlayerController controller;
    private BuildingSystem buildingSystem;

    [HideInInspector]
    public BuildingSlot[] slots;

    private BuildingType currentType;
    private int maxPage = 3;
    private int currentPage;
    private int slotsPerPage = 6;
    

    void Start()
    {        
        controller = CharacterManager.Instance.Player.GetComponent<PlayerController>();
        buildingSystem = GetComponentInParent<BuildingSystem>();
        buildingWindow = gameObject;        
        slots = new BuildingSlot[maxPage * slotsPerPage];
        currentType = BuildingType.Furniture;

        controller.building += Toggle;

        for (int i = 0; i < maxPage * slotsPerPage; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotPanel);
            slot.SetActive(false);
            slots[i] = slot.GetComponent<BuildingSlot>();
            slots[i].index = i;
            slots[i].building = this;
        }
        
        buildingWindow.SetActive(false);
    }

    private void Update()
    {
        
    }

    // 현재 활성화 상태에 따라 상태 전환
    public void Toggle()
    {
        buildingWindow.SetActive(!buildingWindow.activeInHierarchy);
        if (buildingWindow.activeInHierarchy)
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {        
        for (int i = 0; i < buildingSO.Length; i++)
        {            
            slots[i].Set(i);
            int tmp = i;
                        
            // 타입이 일치하면 슬롯 활성화
            if (slots[i].buildingType == currentType)
            {
                slots[i].gameObject.SetActive(true);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }

            if (!slots[i].hasButtonEvent)
            {
                slots[i].button.onClick.AddListener(() => buildingSystem.OnClickSlot(tmp));
                slots[i].hasButtonEvent = true;
            }            
        }
        // 현재 페이지 인덱스 값을 받아서
        // 인덱스 값에 맞는 슬롯 활성화
        // 타입별로 슬롯 인덱스에 맞는 건축물 정보 불러오기
    }

    public void SetType(int typeNumber)
    {
        switch (typeNumber)
        {
            case 0:
                currentType = BuildingType.Furniture;
                UpdateUI();
                break;
            case 1:
                currentType = BuildingType.Module;
                UpdateUI();
                break;
            case 2:
                currentType = BuildingType.Building;
                UpdateUI();
                break;
        }
    }


}
