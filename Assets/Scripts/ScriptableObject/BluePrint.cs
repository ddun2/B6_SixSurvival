using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BluePrintType
{
    Weapon,
    Tool
}

[CreateAssetMenu(fileName = "BluePrint", menuName = "New BluePrint")]
[Serializable]
public class BluePrint : ScriptableObject
{
    [Header("info")]
    public BluePrintType type;
    public string displayName;
    public string description;
    public Sprite icon;
    public ItemData makeItem;

    [Header("Stack")]
    public string[] needItemNames;
    public int[] needIngredients;
}
