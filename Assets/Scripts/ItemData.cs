using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/New item")]
public class ItemData : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite visual;
    public GameObject prefab;
    public ItemType type;
    public EquipmentType equipmentType;
}

public enum ItemType
{
    Ressource,
    Equipment,
    Consumable
}

public enum EquipmentType
{
    None,
    Head,
    Chest,
    Hand,
    Legs,
    Feet
}