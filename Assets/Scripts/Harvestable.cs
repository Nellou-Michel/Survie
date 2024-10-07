using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public Ressource[] ressources;
    public bool needDisableKinematicOnDestroy;
    public float destroyDelay;
    public Tool tool;
}

[System.Serializable]
public class Ressource
{
    public ItemData harvestableItem;

    [Range(0,100)]
    public int dropChance;
}

public enum Tool
{
    pickaxe,
    axe,

}
