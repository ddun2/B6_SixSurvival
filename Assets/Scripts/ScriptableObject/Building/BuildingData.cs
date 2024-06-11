using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuildingType
{
    Building,
    Furniture,
    Module
}

[CreateAssetMenu(fileName = "Building", menuName = "New Building")]

public class BuildingData : ScriptableObject
{
    [Header("Info")]
    public int index;
    public string displayName;
    public string description;
    public BuildingType type;
    public Sprite icon;
    public GameObject prefab;
    public GameObject previewPrefab;
}
