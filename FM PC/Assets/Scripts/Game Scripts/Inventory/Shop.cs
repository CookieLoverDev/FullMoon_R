using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    public Inventory playerInventory;
    public ItemDatabase itemDatabase;
    public ItemSaveManager itemSaveManager;
    public int playerMoney;
    public Text moneyText;
    public event Action<int> OnPlayerMoneyChanged;

    public void Start()
    {
        UpdateShopUI();

    }



    public void BuyItem(string itemID)
    {
        Item item = itemDatabase.GetItemReference(itemID);

        if (item != null && playerMoney >= item.Price)
        {
            playerMoney -= item.Price;
            OnPlayerMoneyChanged?.Invoke(playerMoney);

            playerInventory.AddItem(item);
            UpdateMoneyText();
            UpdateShopUI();

            AddItemToStartingItems(item);
        }
    }
    private void AddItemToStartingItems(Item item)
    {
        List<Item> startingItemsList = new List<Item>(playerInventory.StartingItems);
        startingItemsList.Add(item);
        playerInventory.startingItems = startingItemsList.ToArray();
        playerInventory.UpdateStartingItems();
    }


    private void UpdateMoneyText()
    {
        moneyText.text = playerMoney.ToString();
    }
    private void UpdateShopUI()
    {
        playerMoney.ToString();
    }


}