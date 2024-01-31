using System;
using UnityEngine;
public class Equipmentpanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    public EquipmentSlot[] EquipmentSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            EquipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            EquipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            EquipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
            EquipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            EquipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            EquipmentSlots[i].OnDragEvent += OnDragEvent;
            EquipmentSlots[i].OnDropEvent += OnDropEvent;
        }
    }
    private void OnValidate()
    {
        EquipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            if (EquipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                previousItem = (EquippableItem)EquipmentSlots[i].Item;
                EquipmentSlots[i].Item = item;
                EquipmentSlots[i].Amount = 1;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquippableItem item)
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            if (EquipmentSlots[i].Item == item)
            {
                EquipmentSlots[i].Item = null;
                EquipmentSlots[i].Amount = 0;
                return true;
            }
        }
        return false;
    }
}
