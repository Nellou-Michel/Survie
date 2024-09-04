using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private float pickupRange = 2.6f;
    public PickupBehavior pickupBehavior;
    public MoveBehaviour moveBehavior;
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            if (hit.transform.CompareTag("Item"))
            {
                Debug.Log("There is an item in front of us, its a " + hit.transform.GetComponent<Item>().itemData.name.ToString());

                if(Input.GetKeyDown(KeyCode.E))
                {
                    // CancelInvoke le spam d'aniamtion'
                    if (!moveBehavior.canMove)
                        return;

                    pickupBehavior.DoPickup(hit.transform.GetComponent<Item>());
                }
            }
        }
    }
}
