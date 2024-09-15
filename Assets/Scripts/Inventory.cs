using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public GameObject dropPoint;

    [SerializeField]
    private Sprite transparent;

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventoryContent;

    [SerializeField]
    private List<ItemData> content = new List<ItemData>();

    const int InventoryMaxSize = 24;


    [Header("Action Panel References")]

    [SerializeField]
    private GameObject actionPanel;


    [SerializeField]
    private GameObject useItemButton;


    [SerializeField]
    private GameObject dropItemButton;


    [SerializeField]
    private GameObject equipItemButton;

    [SerializeField]
    private EquipmentLibrary equipmentLibrary;

    [SerializeField]
    private GameObject destroyItemButton;

    private ItemData itemCurrentlySelected;

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

    public void HideOrShowInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    private void RefreshContent()
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
    }

    public void OpenActionPanel(ItemData item, RectTransform currentSlot)
    {

        if (item == null)
        {
            actionPanel.SetActive(false);
            return;
        }

        itemCurrentlySelected = item;
        switch(item.type)
        {
            case ItemType.Ressource:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Equipment:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(true);
                break;
            case ItemType.Consumable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
        }
        Vector3 underSlotPos = new Vector3(0,0,0);
        underSlotPos.x = currentSlot.position.x;
        underSlotPos.y = currentSlot.position.y - currentSlot.sizeDelta.x / 2;

        actionPanel.transform.position = underSlotPos;
        actionPanel.SetActive(true);
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        itemCurrentlySelected = null;
    }

    public void DestroyItem()
    {
        content.Remove(itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }

    public void UseItem()
    {
        Debug.Log("use");
        CloseActionPanel();
    }

    public void EquipItem()
    {
        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemCurrentlySelected).First();
        if(equipmentLibraryItem != null)
        {
            for (int i = 0; i < equipmentLibraryItem.elementsToDisabled.Length; i++)
            {
                equipmentLibraryItem.elementsToDisabled[i].SetActive(false);
            }

            for (int i = 0; i < equipmentLibraryItem.elementsToAble.Length; i++)
            {
                equipmentLibraryItem.elementsToAble[i].SetActive(true);
            }
        }
        else
        {
            Debug.Log("sus mmmmh aucun visuel correspond");
        }
        CloseActionPanel();
    }

    public void DropItem()
    {
        Instantiate(itemCurrentlySelected.prefab, dropPoint.transform.position, dropPoint.transform.rotation);
        DestroyItem();
    }
}
