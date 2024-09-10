using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instance;

    [SerializeField]
    private ToolTip tooltip;

    private void Awake()
    {
        instance = this;
    }

    public void Show(string description, string title = "")
    {
        tooltip.SetText(description, title);
        tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
    }
}
