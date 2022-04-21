using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Inventory inventory;
    [SerializeField] private QuickSlotPanel quickSlotPanel;
    [SerializeField] private ShopPanel shopPanel;
    private BaseItemSlot draggedSlot;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] private Image draggableItem;
    [SerializeField] public int gold;
    [SerializeField] public UIManager UIManager;

    private void Update()
    {
        UIManager.goldText.text = gold.ToString();
    }

    private void OnValidate()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();
    }

    private void Awake()
    {
        //Set Events
        // inventory.OnRightClickEvent += Equip;
        //Pointer Enter
        inventory.OnShiftRightClickEvent += DivideItems;
        quickSlotPanel.OnShiftRightClickEvent += DivideItems;

        inventory.OnPointerEnterEvent += ShowTooltip;
        quickSlotPanel.OnPointerEnterEvent += ShowTooltip;
        shopPanel.OnPointerEnterEvent += ShowTooltip;
        //Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        quickSlotPanel.OnPointerExitEvent += HideTooltip;
        shopPanel.OnPointerExitEvent += HideTooltip;
        //On Begin Drag Event
        inventory.OnBeginDragEvent += BeginDrag;
        quickSlotPanel.OnBeginDragEvent += BeginDrag;
        shopPanel.OnBeginDragEvent += BeginDrag;
        //On Begin Drag Event
        inventory.OnEndDragEvent += EndDrag;
        quickSlotPanel.OnEndDragEvent += EndDrag;
        shopPanel.OnEndDragEvent += EndDrag;
        //Drag
        inventory.OnDragEvent += Drag;
        quickSlotPanel.OnDragEvent += Drag;
        shopPanel.OnDragEvent += Drag;
        //Drop
        inventory.OnDropEvent += Drop;
        quickSlotPanel.OnDropEvent += Drop;
        shopPanel.OnDropEvent += Drop;
        //Buy
    }

    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        Item equippableItem = itemSlot.Item;
        if (equippableItem != null)
        {
            itemTooltip.ShowTooltip(equippableItem);
        }
    }

    private void HideTooltip(BaseItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(BaseItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(BaseItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(BaseItemSlot dropItemSlot)
    {
        if (draggedSlot == null) return;

        var isDraggedBuyable = ((ItemSlot) draggedSlot).buyableSlot;
        var isDroppedBuyable = ((ItemSlot) dropItemSlot).buyableSlot;

        if (Input.GetKey(KeyCode.LeftShift) && !isDroppedBuyable && !isDraggedBuyable)
        {
            DivideItems(dropItemSlot);
        }
        else if (dropItemSlot.CanAddStack(draggedSlot.Item))
        {
            if (isDraggedBuyable && !isDroppedBuyable)
            {
                ShopBuy(draggedSlot);
            }
            else if (isDroppedBuyable && !isDraggedBuyable)
            {
                ShopSell(draggedSlot);
            }

            AddStacks(dropItemSlot);
        }
        else if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {
            if (isDraggedBuyable && !isDroppedBuyable && dropItemSlot.Item == null)
            {
                ShopBuy(dropItemSlot);
                SwapItems(dropItemSlot);
            }
            else if (!isDraggedBuyable && isDroppedBuyable && dropItemSlot.Item == null)
            {
                ShopSell(dropItemSlot);
                SwapItems(dropItemSlot);
            }
            else if (!isDraggedBuyable && !isDroppedBuyable)
            {
                SwapItems(dropItemSlot);
            }
        }
    }

    private void ShopBuy(BaseItemSlot dropItemSlot)
    {
        if (draggedSlot == null) return;

        Item draggedItem = draggedSlot.Item;
        int draggedItemAmount = draggedSlot.Amount;

        for (int i = 0; i < draggedItemAmount; i++)
        {
            shopPanel.BuyItem(draggedItem);
        }
    }

    private void ShopSell(BaseItemSlot dropItemSlot)
    {
        if (draggedSlot == null) return;

        Item draggedItem = draggedSlot.Item;
        int draggedItemAmount = draggedSlot.Amount;


        for (int i = 0; i < draggedItemAmount; i++)
        {
            shopPanel.SellItem(draggedItem);
        }
    }

    private void SwapItems(BaseItemSlot dropItemSlot)
    {
        Item draggedItem = draggedSlot.Item;
        int draggedItemAmount = draggedSlot.Amount;

        draggedSlot.Item = dropItemSlot.Item;
        draggedSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void DivideItems(BaseItemSlot dropItemSlot)
    {
        if (inventory.IsFull()) return;
        if (draggedSlot.Amount == 1) return;
        Item draggedItem = draggedSlot.Item;
        if (dropItemSlot.Item != null && draggedSlot.Item != dropItemSlot.Item)
        {
            Debug.Log("Dividing Items");
            return;
        }

        int draggedItemAmount;


        //Handle if item can be directly divided to 2
        if (draggedSlot.Amount % 2 == 0)
        {
            draggedItemAmount = draggedSlot.Amount / 2;
            draggedSlot.Amount = draggedItemAmount;
        }
        else
        {
            draggedItemAmount = (draggedSlot.Amount - 1) / 2;
            draggedSlot.Amount = draggedItemAmount;
            dropItemSlot.Amount++;
        }

        draggedSlot.Item = draggedItem;
        dropItemSlot.Item = draggedItem;

        if (draggedSlot.Item == dropItemSlot.Item)
        {
            draggedItemAmount += dropItemSlot.Amount;
            if (draggedItemAmount <= dropItemSlot.Item.maximumStacks)
            {
                dropItemSlot.Amount = draggedItemAmount;
            }
            else
            {
                int tempAmount = draggedSlot.Amount;
                draggedSlot.Amount = (draggedSlot.Amount * 2 - (dropItemSlot.Item.maximumStacks - dropItemSlot.Amount));
                dropItemSlot.Amount = dropItemSlot.Item.maximumStacks;
            }
        }
        else dropItemSlot.Amount = draggedItemAmount;
    }

    private void AddStacks(BaseItemSlot dropItemSlot)
    {
        int numAddableStacks = dropItemSlot.Item.maximumStacks - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, draggedSlot.Amount);

        dropItemSlot.Amount += stacksToAdd;
        draggedSlot.Amount -= stacksToAdd;
    }

    public void RemoveStack(BaseItemSlot currentItemSlot)
    {
        if (currentItemSlot.Item != null) currentItemSlot.Amount--;
    }
}