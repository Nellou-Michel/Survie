using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private MoveBehaviour moveBehaviour;

    private Item currentItem;

    private Harvestable currentHarvestable;
    private Tool currentTool;


    [Header("Tools Visual")]
    [SerializeField]
    private GameObject pickaxe;
    [SerializeField]
    private GameObject axe;

    public void DoPickup(Item item)
    {
        playerAnimator.SetTrigger("Pickup");
        moveBehaviour.canMove = false;
        currentItem = item;
    }

    public void DoHarvest(Harvestable harvestable) {
        currentTool = harvestable.tool;
        currentHarvestable = harvestable;
        DisplayTool(currentTool);

        playerAnimator.SetTrigger("Harvest");
        moveBehaviour.canMove = false;
    }

    //Appellé depuis l'animation
    IEnumerator BreakHarvestable() 
    {
        if(currentHarvestable.needDisableKinematicOnDestroy)
        {
            Rigidbody rigidbody = currentHarvestable.gameObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.AddForce(new Vector3(200f,5f,5f), ForceMode.Impulse);

        }


        yield return new WaitForSeconds(currentHarvestable.destroyDelay);

        for (int i = 0; currentHarvestable.ressources.Length > i; i++)
        {
            Ressource currentRessource = currentHarvestable.ressources[i];
            if (Random.Range(1,101) <= currentRessource.dropChance)
            {
                Vector3 newPos = currentHarvestable.transform.position;
                float offset = .5f;
                newPos.x += Random.Range(-offset, offset);
                newPos.z += Random.Range(-offset, offset);
                newPos.y += Random.Range(0f, offset+1);

                Quaternion newRot = currentHarvestable.transform.rotation;
                newRot.x += Random.Range(-90f, 90f);
                newRot.y += Random.Range(-90f, 90f);
                newRot.z += Random.Range(-90f, 90f);
                Debug.Log(newRot);

                Instantiate(currentRessource.harvestableItem.prefab, newPos, newRot);
            }
        }
        Destroy(currentHarvestable.gameObject);

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
    public void EndInteraction()
    {
        moveBehaviour.canMove = true;
        DisplayTool(currentTool, false);
    }

    public void DisplayTool(Tool tool, bool enabled = true)
    {
        switch (tool)
        {
            case Tool.axe:
                axe.SetActive(enabled);
                break;  
            case Tool.pickaxe:
                pickaxe.SetActive(enabled); 
                break;
            default:
                axe.SetActive(enabled);
                break;
        }
    }
}
