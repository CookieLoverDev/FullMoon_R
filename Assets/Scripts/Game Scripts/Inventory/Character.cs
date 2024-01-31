using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public CharacterStat Hp;
    public CharacterStat Mana;
    public CharacterStat Armor;
    public CharacterStat Strength;
    public CharacterStat Crit;
    public CharacterStat Speed;

    public Inventory Inventory;
    public Equipmentpanel EquipmentPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemToolTip itemToolTip;
    [SerializeField] Image draggableItem;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] QuestionDialog questionDialog;
    [SerializeField] ItemSaveManager itemSaveManager;
    private ItemSlot dragItemSlot;
    public Shop shop;
    private void OnValidate()
    {
        if (itemToolTip == null)
            itemToolTip = FindObjectOfType<ItemToolTip>();
    }

    private void Awake()
    {
        statPanel.SetStats(Hp, Mana, Armor, Strength, Crit, Speed);
        statPanel.UpdateStatValues();

        //Setup Events:
        //Right Click
        Inventory.OnRightClickEvent += Equip;
        EquipmentPanel.OnRightClickEvent += Unequip;
        //Pointer Enter
        Inventory.OnPointerEnterEvent += ShowTooltip;
        EquipmentPanel.OnPointerEnterEvent += ShowTooltip;
        //Pointer Exit
        Inventory.OnPointerExitEvent += HideTooltip;
        EquipmentPanel.OnPointerExitEvent += HideTooltip;
        //Begin Drag
        Inventory.OnBeginDragEvent += BeginDrag;
        EquipmentPanel.OnBeginDragEvent += BeginDrag;
        //End Drag
        Inventory.OnEndDragEvent += EndDrag;
        EquipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        Inventory.OnDragEvent += Drag;
        EquipmentPanel.OnDragEvent += Drag;
        //Drop
        Inventory.OnDropEvent += Drop;
        EquipmentPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;

        itemSaveManager.LoadEquipment(this);
        itemSaveManager.LoadInventory(this);
    }


    private void Start()
    {
        itemSaveManager.LoadEquipment(this);
        itemSaveManager.LoadInventory(this);
    }

    private void OnDestroy()
    {
        itemSaveManager.SaveEquipment(this);
        itemSaveManager.SaveInventory(this);
    }
    private void Equip(ItemSlot itemSlot)
    {
        bool fromLoad;
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            EquipFromInventory(equippableItem);
        }
    }

    private void Unequip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            itemToolTip.ShowToolTip(equippableItem);
        }
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        itemToolTip.HideTooltip();
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.gameObject.SetActive(false);
    }

    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanAddStack(dragItemSlot.Item))
        {
            AddStacks(dropItemSlot);
        }

        else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);
        }

    }

    private void DropItemOutsideUI()
    {
        if (dragItemSlot == null) return;

        questionDialog.Show();
        ItemSlot itemSlot = dragItemSlot;
        questionDialog.OnYesEvent += () => DestroyItemInSlot(itemSlot);
    }

    private void DestroyItemInSlot(ItemSlot itemSlot)
    {
        itemSlot.Item.Destroy();
        itemSlot.Item = null;
    }

    private void SwapItems(ItemSlot dropItemSlot)
    {
        EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
        EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

        if (dragItemSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.Unequip(this);
            if (dropItem != null) dropItem.Equip(this);
        }
        if (dropItemSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.Equip(this);
            if (dropItem != null) dropItem.Unequip(this);
        }
        statPanel.UpdateStatValues();

        Item draggedItem = dragItemSlot.Item;
        int draggedItemAmount = dragItemSlot.Amount;

        dragItemSlot.Item = dropItemSlot.Item;
        dragItemSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void AddStacks(ItemSlot dropItemSlot)
    {
        int numAddableStacks = dropItemSlot.Item.MaximumStacks - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

        dropItemSlot.Amount += stacksToAdd;
        dragItemSlot.Amount -= stacksToAdd;
    }
    public void Equip(EquippableItem item)
    {
        Equip(item, false);
    }

    public void Equip(EquippableItem item, bool fromLoad)
    {
        if (fromLoad)
        {
            EquippableItem previousItem;
            if (EquipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    Inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            // Case when you cant equip item
            else
            {
                Inventory.AddItem(item);
            }
        }
        else if (Inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (EquipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    Inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                Inventory.AddItem(item);
            }
        }

    }

    public void Unequip(EquippableItem item)
    {
        if (!Inventory.isFull() && EquipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            Inventory.AddItem(item);
        }
    }
    public void EquipFromLoad(EquippableItem item)
    {
        Equip(item, true);
    }

    public void EquipFromInventory(EquippableItem item)
    {
        Equip(item, false);
    }

}