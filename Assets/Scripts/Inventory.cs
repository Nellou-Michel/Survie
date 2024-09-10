using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventoryContent;

    [SerializeField]
    private List<ItemData> content = new List<ItemData>();

    const int InventoryMaxSize = 24;

    private void Start()
    {
        RefreshContent();        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            HideOrShowInventory();
        }
    }

    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }

    public bool isFull()
    {
        if (content.Count == InventoryMaxSize)
            return true;
        return false;
    }

    // ======= Visuel/UI

    public void HideOrShowInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    private void RefreshContent()
    {
        for (int i = 0; i < content.Count; i++) {
            Slot currentSlot = inventoryContent.GetChild(i).GetComponent<Slot>();
            currentSlot.item = content[i];
            currentSlot.itemVisual.sprite = content[i].visual;
        }
    }
}
