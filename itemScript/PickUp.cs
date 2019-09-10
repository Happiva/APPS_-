using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;

    private void Start() {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    /*
    public void PutInInventory(GameObject item) {
        if (item.CompareTag("Potato"))
        {
            inventory.amount[0]++;
        }
        else if (item.CompareTag("Crab"))
        {
            inventory.amount[1]++;
        }
        else if (item.CompareTag("Tree"))
        {
            inventory.amount[2]++;
        }
    }
    */

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            inventory.amount[0]++;
            inventory.amountText[0].text = inventory.amount[0] + "";
        }
    }
}
