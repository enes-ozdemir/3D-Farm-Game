using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer
{
    [SerializeField] private List<Item> startingItems;
    [SerializeField] private Transform itemParent;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;
    public event Action<BaseItemSlot> OnShiftRightClickEvent;
    public event Action<BaseItemSlot> onBuyEvent;

    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnShiftRightClickEvent += OnShiftRightClickEvent;
        }
        SetStartingItems();
    }

    private void OnValidate()
    {
        if (itemParent != null)
        {
            itemSlots = itemParent.GetComponentsInChildren<ItemSlot>();
        }

        //SetStartingItems();
    }

    private void SetStartingItems()
    {
        int i = 0;
        for (; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i].GetCopy();
            itemSlots[i].Amount = 1;
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
            itemSlots[i].Amount = 0;
        }
    }


    // public bool ContainsItem(Item item)
    // {
    //     for (int i = 0; i < itemSlots.Length; i++)
    //     {
    //         if (itemSlots[i].Item == item)
    //         {
    //             return true;
    //         }
    //     }
    //
    //     return false;
    // }
}