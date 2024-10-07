using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData item;
    public Image itemVisual;

    [SerializeField]
    private ItemActionSystem actionSystem;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            TooltipSystem.instance.Show(item.description, item.name);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }

    public void ClickOnSlot()
    {
        actionSystem.OpenActionPanel(item, GetComponent<RectTransform>());
    }

    public void ClickOnSlot(bool fromEquiped)
    {
        actionSystem.OpenActionPanel(item, GetComponent<RectTransform>(), fromEquiped);
    }
}
