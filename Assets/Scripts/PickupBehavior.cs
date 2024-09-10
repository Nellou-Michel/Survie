using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private MoveBehaviour moveBehaviour;

    private Item currentItem;

    public void DoPickup(Item item)
    {
        playerAnimator.SetTrigger("Pickup");
        moveBehaviour.canMove = false;
        currentItem = item;
    }

    // called during Pickup animation
    public void PickObject()
    {
        if (inventory.isFull())
            return;
        inventory.AddItem(currentItem.itemData);
        Destroy(currentItem.gameObject);
        currentItem = null;
    }

    // called during Pickup animation
    public void EndPickUp()
    {
        moveBehaviour.canMove = true;
    }
}
