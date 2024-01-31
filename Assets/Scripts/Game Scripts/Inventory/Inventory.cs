using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Inventory : MonoBehaviour
{
    [SerializeField] public Item[] startingItems;
    public Item[] StartingItems => startingItems;
    [SerializeField] protected Transform itemsParent;
    public List<ItemSlot> ItemSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;



    private void Start()
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            ItemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            ItemSlots[i].OnRightClickEvent += OnRightClickEvent;
            ItemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            ItemSlots[i].OnEndDragEvent += OnEndDragEvent;
            ItemSlots[i].OnDragEvent += OnDragEvent;
            ItemSlots[i].OnDropEvent += OnDropEvent;
        }

        SetStartingItems();
        UpdateStartingItems();
    }


    private void OnValidate()
    {
        if (itemsParent != null)
            itemsParent.GetComponentsInChildren<ItemSlot>(results: ItemSlots);
        SetStartingItems();
    }

    private void SetStartingItems()
    {
        int i = 0;
        for (; i < startingItems.Length && i < ItemSlots.Count; i++)
        {
            ItemSlots[i].Item = startingItems[i].GetCopy();
            ItemSlots[i].Amount = 1;
        }

        for (; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].Item = null;
            ItemSlots[i].Amount = 0;
        }
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].Item == null || (ItemSlots[i].CanAddStack(item)))
            {
                ItemSlots[i].Item = item;
                ItemSlots[i].Amount++;
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].Item == item)
            {
                ItemSlots[i].Amount--;
                return true;
            }
        }
        return false;
    }


    public Item RemoveItem(string itemID)
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            Item item = ItemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                ItemSlots[i].Amount--;
                return item;
            }
        }
        return null;
    }
    public bool isFull()
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }

    public void Clear()
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].Item != null && Application.isPlaying)
            {
                ItemSlots[i].Item.Destroy();
            }
            ItemSlots[i].Item = null;
            ItemSlots[i].Amount = 0;
        }
    }

    public void UpdateStartingItems()
    {
        SetStartingItems(); ;
    }

}