using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private float interactRange = 2.6f;

    public InteractBehavior interactBehavior;
    public MoveBehaviour moveBehavior;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject InteractTip;

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerMask))
        {
            InteractTip.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!moveBehavior.canMove) // to stop le cancel d'animation
                    return;

                if (hit.transform.CompareTag("Item"))
                {
                    interactBehavior.DoPickup(hit.transform.GetComponent<Item>());
                }
                
                if (hit.transform.CompareTag("Harvestable"))
                {
                    interactBehavior.DoHarvest(hit.transform.GetComponent<Harvestable>());
                    Debug.Log(hit.transform.name);
                }               
            }
        }
        else
        {
            InteractTip.SetActive(false);
        }
    }
}
