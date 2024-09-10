using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField]
    private Text title;

    [SerializeField]
    private Text description;

    [SerializeField]
    private LayoutElement layoutElement;

    [SerializeField]
    private int maxCharacter;

    [SerializeField]
    RectTransform rectTransform;

    public void SetText(string d, string t = "")
    {
        if (t == "")
        {
            title.gameObject.SetActive(false);
        }
        else
        {
            title.gameObject.SetActive(true);
            title.text = t;
        }
        description.text = d;

        int titleLenghth = title.text.Length;
        int descLength = description.text.Length;
        layoutElement.enabled = (titleLenghth > maxCharacter || descLength > maxCharacter) ? true : false;
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;


        if(mousePosition.x > Screen.width/2)
            rectTransform.pivot = new Vector2(1, 0.2f);
        else
            rectTransform.pivot = new Vector2(0, 0.2f);

        
        transform.position = mousePosition;
    }
}
