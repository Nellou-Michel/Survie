using UnityEngine;

public class ItemActionSystem : MonoBehaviour
{
    [Header("Action Panel References")]

    public GameObject actionPanel;

    public GameObject dropPoint;

    [SerializeField]
    private GameObject useItemButton, disquipItemButton, destroyFromEquipedButton, dropFromEquipedButton, dropItemButton, equipItemButton, destroyItemButton;

    [HideInInspector]
    public ItemData itemCurrentlySelected;


    public void OpenActionPanel(ItemData item, RectTransform currentSlot, bool isFromEquiped = false)
    {
        if (item == null)
        {
            actionPanel.SetActive(false);
            return;
        }

        itemCurrentlySelected = item;
        if (isFromEquiped)
        {
            useItemButton.SetActive(false);
            equipItemButton.SetActive(false);
            dropItemButton.SetActive(false);
            destroyItemButton.SetActive(false);

            disquipItemButton.SetActive(true);
            destroyFromEquipedButton.SetActive(true);
            dropFromEquipedButton.SetActive(true);
        }
        else
        {
            switch (item.type)
            {
                case ItemType.Ressource:
                    useItemButton.SetActive(false);
                    equipItemButton.SetActive(false);
                    disquipItemButton.SetActive(false);
                    destroyFromEquipedButton.SetActive(false);
                    dropFromEquipedButton.SetActive(false);

                    dropItemButton.SetActive(true);
                    destroyItemButton.SetActive(true);
                    break;
                case ItemType.Equipment:
                    useItemButton.SetActive(false);
                    disquipItemButton.SetActive(false);
                    destroyFromEquipedButton.SetActive(false);
                    dropFromEquipedButton.SetActive(false);

                    equipItemButton.SetActive(true);
                    dropItemButton.SetActive(true);
                    destroyItemButton.SetActive(true);
                    break;
                case ItemType.Consumable:
                    equipItemButton.SetActive(false);
                    disquipItemButton.SetActive(false);
                    destroyFromEquipedButton.SetActive(false);
                    dropFromEquipedButton.SetActive(false);

                    useItemButton.SetActive(true);
                    dropItemButton.SetActive(true);
                    destroyItemButton.SetActive(true);
                    break;
            }
        }

        Vector3 underSlotPos = new Vector3(0, 0, 0);
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
        Inventory.instance.RemoveItem(itemCurrentlySelected);
        Inventory.instance.RefreshContent();
        CloseActionPanel();
    }

    public void UseItem()
    {
        Debug.Log("use");
        CloseActionPanel();
    }

    public void DropItem()
    {
        Instantiate(itemCurrentlySelected.prefab, dropPoint.transform.position, dropPoint.transform.rotation);
        DestroyItem();
    }
}
