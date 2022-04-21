using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image image;
    [SerializeField] protected Text amountText;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnShiftRightClickEvent;

    protected Color normalColor = Color.white;
    protected Color disabledColor = new Color(1, 1, 1, 0);
    protected Item _item;

    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                image.color = disabledColor;
            }
            else
            {
                image.sprite = _item.icon;
                image.color = normalColor;
            }
        }
    }

    private int _amount;

    public int Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            if (_amount < 0) _amount = 0;
            if (_amount == 0) Item = null;

            if (amountText != null)
            {
                amountText.enabled = _item != null && _amount > 1;
                if (amountText.enabled)
                {
                    amountText.text = _amount.ToString();
                }
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (amountText == null)
            amountText = GetComponentInChildren<Text>();
    }

    public virtual bool CanAddStack(Item item, int amount = 1)
    {
        if (Item == null) return false;
        return Item != null && Item.ID == item.ID;
    }

    public virtual bool CanReceiveItem(Item item)
    {
        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        // {
        //     if (OnRightClickEvent != null)
        //         OnRightClickEvent(this);
        // }

        if (eventData != null && eventData.button == PointerEventData.InputButton.Right &&
            Input.GetKey(KeyCode.LeftShift))
        {
            if (OnShiftRightClickEvent != null)
                OnShiftRightClickEvent?.Invoke(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }

    public void OnShiftRightClick(PointerEventData eventData)
    {
    }
}