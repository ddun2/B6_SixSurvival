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

    // ���� Ȱ��ȭ ���¿� ���� ���� ��ȯ
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
                        
            // Ÿ���� ��ġ�ϸ� ���� Ȱ��ȭ
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
        // ���� ������ �ε��� ���� �޾Ƽ�
        // �ε��� ���� �´� ���� Ȱ��ȭ
        // Ÿ�Ժ��� ���� �ε����� �´� ���๰ ���� �ҷ�����
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
