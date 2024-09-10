using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField]
    private float pickupRange = 2.6f;

    public PickupBehavior pickupBehavior;
    public MoveBehaviour moveBehavior;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject pickupTip;

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, layerMask))
        {
            if (hit.transform.CompareTag("Item"))
            {
                pickupTip.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // CancelInvoke le spam d'aniamtion'
                    if (!moveBehavior.canMove)
                        return;

                    pickupBehavior.DoPickup(hit.transform.GetComponent<Item>());
                }
            }
        }
        else
        {
            pickupTip.SetActive(false);
        }
    }
}
