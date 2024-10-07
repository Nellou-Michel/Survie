using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("OTHER SCRIPTS REFERENCES")]

    [SerializeField]
    Equipment equipment;

    [SerializeField]
    ItemActionSystem itemActionSystem;

    [Header("Inventory Panel References")]

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventoryContent;

    [SerializeField]
    private List<ItemData> content = new List<ItemData>();

    public Sprite transparent;

    public GameObject dropPoint;

    public bool isOpen = false;

    const int InventoryMaxSize = 24;
    public static Inventory instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RefreshContent();        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (isOpen) 
            { 
                HideInventory();
            }
            else
            {
                ShowInventory();
                isOpen = !isOpen;
            }
        }
    }

    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }    
    
    public void RemoveItem(ItemData item)
    {
        content.Remove(item);
        RefreshContent();
    }

    public bool isFull()
    {
        if (content.Count == InventoryMaxSize)
            return true;
        return false;
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
        itemActionSystem.actionPanel.SetActive(false);
        TooltipSystem.instance.Hide();
        isOpen = !isOpen;
    }

    public void RefreshContent()
    {
        for (int i = 0; i < inventoryContent.childCount; i++)
        {
            inventoryContent.GetChild(i).GetComponent<Slot>().item = null;
            inventoryContent.GetChild(i).GetComponent<Slot>().itemVisual.sprite = transparent;
        }

        for (int i = 0; i < content.Count; i++) {
            Slot currentSlot = inventoryContent.GetChild(i).GetComponent<Slot>();
            currentSlot.item = content[i];
            currentSlot.itemVisual.sprite = content[i].visual;
        }
        equipment.UpdateEquipmentDesequipButton();
    }
}
