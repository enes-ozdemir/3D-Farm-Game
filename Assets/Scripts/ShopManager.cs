using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Inventory shopInventory;
    [SerializeField] private ShopPanel shopPanel;
    [SerializeField] private GameManager gameManager;


    public void BuyItem(Item item)
    {
        if (gameManager.inventory.IsFull()) return;
        if (gameManager.gold >= item.value)
        {
            gameManager.inventory.AddItem(item);
            shopPanel.shopInventory.RemoveItem(item);
            gameManager.gold -= item.value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}