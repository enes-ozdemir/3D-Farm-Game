using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : BaseItemSlot, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;
    public event Action<BaseItemSlot> OnBuyEvent;

    [SerializeField] public bool buyableSlot = false;

    private Color dragColor = new Color(1, 1, 1, 0.5f);


    public override bool CanAddStack(Item item, int amount = 1)
    {
        return base.CanAddStack(item, amount) && Amount + amount <= item.maximumStacks;
    }

    public override bool CanReceiveItem(Item item)
    {
        return true;
    }

    Vector2 originalPosition;

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item != null)
            image.color = dragColor;
        OnBeginDragEvent?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Item != null)
            image.color = normalColor;
        OnEndDragEvent?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(this);
    }

    public void OnBuy(PointerEventData eventData)
    {
        OnBuyEvent?.Invoke(this);
    }
}