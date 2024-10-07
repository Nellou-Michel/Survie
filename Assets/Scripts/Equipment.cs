using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class Equipment : MonoBehaviour
{
    [Header("OTHER SCRIPTS REFERENCES")]

    [SerializeField]
    ItemActionSystem itemActionSystem;

    [Header("Equipment Panel References")]

    [SerializeField]
    private EquipmentLibrary equipmentLibrary;

    [SerializeField]
    private GameObject headSlotDesequipButton, chestSlotDesequipButton, handSlotDesequipButton, legsSlotDesequipButton, feetSlotDesequipButton;

    [SerializeField]
    private Image headSlotImage, chestSlotImage, handSlotImage, pantSlotImage, feetSlotImage;

    private ItemData equipedHeadItem, equipedChestItem, equipedHandsItem, equipedLegsItem, equipedFeetItem;

    [SerializeField]
    private GameObject headSlot, chestSlot, handSlot, legsSlot, feetSlot;

    public void EquipEquipment()
    {
        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemActionSystem.itemCurrentlySelected).First();

        if (equipmentLibraryItem != null)
        {
            switch (itemActionSystem.itemCurrentlySelected.equipmentType)
            {
                case EquipmentType.Head:
                    ClearPreviousEquipedItem(equipedHeadItem);
                    headSlot.GetComponent<Slot>().item = itemActionSystem.itemCurrentlySelected;
                    equipedHeadItem = itemActionSystem.itemCurrentlySelected;
                    headSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual; break;

                case EquipmentType.Chest:
                    ClearPreviousEquipedItem(equipedChestItem);
                    chestSlot.GetComponent<Slot>().item = itemActionSystem.itemCurrentlySelected; ;
                    equipedChestItem = itemActionSystem.itemCurrentlySelected;
                    chestSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual; break;

                case EquipmentType.Hand:
                    ClearPreviousEquipedItem(equipedHandsItem);
                    handSlot.GetComponent<Slot>().item = itemActionSystem.itemCurrentlySelected;
                    equipedHandsItem = itemActionSystem.itemCurrentlySelected;
                    handSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual; break;

                case EquipmentType.Legs:
                    ClearPreviousEquipedItem(equipedLegsItem);
                    legsSlot.GetComponent<Slot>().item = itemActionSystem.itemCurrentlySelected;
                    equipedLegsItem = itemActionSystem.itemCurrentlySelected;
                    pantSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual; break;

                case EquipmentType.Feet:
                    ClearPreviousEquipedItem(equipedFeetItem);
                    feetSlot.GetComponent<Slot>().item = itemActionSystem.itemCurrentlySelected;
                    equipedFeetItem = itemActionSystem.itemCurrentlySelected;
                    feetSlotImage.sprite = itemActionSystem.itemCurrentlySelected.visual; break;
            }

            for (int i = 0; i < equipmentLibraryItem.elementsToDisabled.Length; i++)
            {
                equipmentLibraryItem.elementsToDisabled[i].SetActive(false);
            }

            for (int i = 0; i < equipmentLibraryItem.elementsToAble.Length; i++)
            {
                equipmentLibraryItem.elementsToAble[i].SetActive(true);
            }

            Inventory.instance.RemoveItem(itemActionSystem.itemCurrentlySelected);
            Inventory.instance.RefreshContent();
        }
        else
        {
            Debug.Log("sus mmmmh aucun visuel correspond");
        }
        itemActionSystem.CloseActionPanel();
    }

    public void DesequipEquipment()
    {
        if (Inventory.instance.isFull())
        {
            Debug.Log("inventaire plein");
            return;
        }

        Inventory.instance.AddItem(itemActionSystem.itemCurrentlySelected);
        Inventory.instance.RefreshContent();
        DestroyItemFromEquip();
    }

    public void DestroyItemFromEquip()
    {
        RemoveVisualEquipment();
        switch (itemActionSystem.itemCurrentlySelected.equipmentType)
        {
            case EquipmentType.Head:
                headSlot.GetComponent<Slot>().item = null;
                headSlotImage.sprite = Inventory.instance.transparent;
                equipedHeadItem = null; break;

            case EquipmentType.Chest:
                chestSlot.GetComponent<Slot>().item = null;
                chestSlotImage.sprite = Inventory.instance.transparent;
                equipedChestItem = null; break;

            case EquipmentType.Hand:
                handSlot.GetComponent<Slot>().item = null;
                handSlotImage.sprite = Inventory.instance.transparent;
                equipedHandsItem = null; break;

            case EquipmentType.Legs:
                legsSlot.GetComponent<Slot>().item = null;
                pantSlotImage.sprite = Inventory.instance.transparent;
                equipedLegsItem = null; break;

            case EquipmentType.Feet:
                feetSlot.GetComponent<Slot>().item = null;
                feetSlotImage.sprite = Inventory.instance.transparent;
                equipedFeetItem = null; break;
        }
        itemActionSystem.CloseActionPanel();
    }

    private void RemoveVisualEquipment()
    {
        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemActionSystem.itemCurrentlySelected).First();

        if (equipmentLibraryItem != null)
        {
            for (int i = 0; i < equipmentLibraryItem.elementsToDisabled.Length; i++)
            {
                equipmentLibraryItem.elementsToDisabled[i].SetActive(true);
            }

            for (int i = 0; i < equipmentLibraryItem.elementsToAble.Length; i++)
            {
                equipmentLibraryItem.elementsToAble[i].SetActive(false);
            }
        }
    }

    private void ClearPreviousEquipedItem(ItemData itemToDisable)
    {
        if (itemToDisable == null)
        {
            return;
        }

        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemToDisable).First();

        if (equipmentLibraryItem != null)
        {
            for (int i = 0; i < equipmentLibraryItem.elementsToDisabled.Length; i++)
            {
                equipmentLibraryItem.elementsToDisabled[i].SetActive(true);
            }

            for (int i = 0; i < equipmentLibraryItem.elementsToAble.Length; i++)
            {
                equipmentLibraryItem.elementsToAble[i].SetActive(false);
            }
        }
        Inventory.instance.AddItem(itemToDisable);
    }

    public void UpdateEquipmentDesequipButton()
    {
        //if null == false else = true
        headSlotDesequipButton.gameObject.SetActive(equipedHeadItem);
        chestSlotDesequipButton.gameObject.SetActive(equipedChestItem);
        handSlotDesequipButton.gameObject.SetActive(equipedHandsItem);
        legsSlotDesequipButton.gameObject.SetActive(equipedLegsItem);
        feetSlotDesequipButton.gameObject.SetActive(equipedFeetItem);
    }

    public void DropItemFromEquiped()
    {
        Instantiate(itemActionSystem.itemCurrentlySelected.prefab, itemActionSystem.dropPoint.transform.position, itemActionSystem.dropPoint.transform.rotation);
        DestroyItemFromEquip();
    }
}
