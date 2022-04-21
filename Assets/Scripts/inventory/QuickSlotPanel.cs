using System;
using inventory;
using UnityEngine;

public class QuickSlotPanel : MonoBehaviour
{
    [SerializeField] private Transform quickSlotParent;
    [SerializeField] private QuickSlot[] quickSlots;
    [SerializeField] public Item currentItem;
    [SerializeField] private PlayerManager playerManager;


    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;
    public event Action<BaseItemSlot> OnShiftRightClickEvent;

    private KeyCode[] _inputlist =
    {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
    };

    private void Start()
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            quickSlots[i].OnRightClickEvent += OnRightClickEvent;
            quickSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            quickSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            quickSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            quickSlots[i].OnEndDragEvent += OnEndDragEvent;
            quickSlots[i].OnDropEvent += OnDropEvent;
            quickSlots[i].OnDragEvent += OnDragEvent;
            quickSlots[i].OnShiftRightClickEvent += OnShiftRightClickEvent;
        }

        // SelectSlot();
    }

    private void OnValidate()
    {
        quickSlots = quickSlotParent.GetComponentsInChildren<QuickSlot>();
    }

    private void Update()
    {
        if (playerManager.isPlayerNotBlocked) SelectSlot();
    }

    private void SelectSlot()
    {
        int key = 0;
        for (int inputIndex = 1; inputIndex <= _inputlist.Length; inputIndex++)
        {
            if (Input.GetKey(inputIndex.ToString()))
            {
                key = inputIndex;

                for (int quickSlotsIndex = 0; quickSlotsIndex < quickSlots.Length; quickSlotsIndex++)
                {
                    quickSlots[quickSlotsIndex].DeSelectSlot();
                }

                quickSlots[key - 1].SelectSlot();
                currentItem = quickSlots[key - 1].Item;
                //if (quickSlots[key - 1].Item is EquippableItem) EquipItem(quickSlots[key - 1].Item);
                playerManager.SetItemToHand();
            }
        }
    }

    // private void EquipItem(Item item)
    // {
    //     
    //     Instantiate(((EquippableItem) item).itemPrefab,)
    //     
    // }
    // private void UnEquipItem()
    // {
    //     
    // }

    /**
     * Add Item to the slot
    */
    public bool AddItem(Item item, out Item previousItem)
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (true)
            {
                previousItem = quickSlots[i].Item;
                quickSlots[i].Item = item;
                //_quickSlots[i].Amount = _quickSlots[i].Amount;
                return true;
            }
        }

        previousItem = null;
        return false;
    }

    /**
     * Remove Item from the slot
    */
    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].Item == item)
            {
                if (quickSlots[i].Amount > 0)
                {
                    quickSlots[i].Amount--;
                    if (quickSlots[i].Amount == 0)
                    {
                        quickSlots[i].Item = null;
                        playerManager.currentItem = null;
                    }
                }
                else
                {
                    quickSlots[i].Item = null;
                }

                return true;
            }
        }

        return false;
    }
}