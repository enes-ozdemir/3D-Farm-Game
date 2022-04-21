using UnityEngine;

public class ShopPanel : Inventory
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] public Inventory shopInventory;

    public void BuyItem(Item item)
    {
        if (gameManager.inventory.IsFull()) return;
        if (gameManager.gold >= item.value)
        {
            //   gameManager.inventory.AddItem(item);
            //  shopInventory.RemoveItem(item);
            gameManager.gold -= item.value;
        }
    }

    public void SellItem(Item item)
    {
        if (shopInventory.IsFull()) return;

        // gameManager.inventory.RemoveItem(item);
        //  shopInventory.AddItem(item);
        gameManager.gold += item.value;
    }
}