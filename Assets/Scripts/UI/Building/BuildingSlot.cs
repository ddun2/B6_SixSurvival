using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour
{
    public BuildingData buildingData;

    public TextMeshProUGUI displayName;
    public TextMeshProUGUI description;
    public Image icon;
    public Button button;
    public BuildingType buildingType;

    public int index;
    public bool hasButtonEvent = false;

    public UIBuilding building;

    public void Set(int index)
    {
        buildingData = building.buildingSO[index];
        button = GetComponent<Button>();
       
        icon.sprite = building.buildingSO[index].icon;
        displayName.text = building.buildingSO[index].displayName;
        description.text = building.buildingSO[index].description;
        buildingType = building.buildingSO[index].type;
    }
}
