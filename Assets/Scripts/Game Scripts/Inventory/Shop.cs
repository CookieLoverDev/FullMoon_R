using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

public class Shop : MonoBehaviour
{
    public Inventory playerInventory;
    public ItemDatabase itemDatabase;
    public ItemSaveManager itemSaveManager;
    public int playermoney;
    public Text moneyText;
    public event Action<int> OnPlayerMoneyChanged;

    public void Start()
    {
        UpdateShopUI();

        if (!PlayerPrefs.HasKey("playermoney"))
        {
            PlayerPrefs.SetInt("playermoney", 0);
        }
        else
        {

        }
        playermoney = PlayerPrefs.GetInt("playermoney");
    }



    public void BuyItem(string itemID)
    {
        Item item = itemDatabase.GetItemReference(itemID);

        if (item != null && playermoney >= item.Price)
        {
            playermoney -= item.Price;
            OnPlayerMoneyChanged?.Invoke(playermoney);

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
        moneyText.text = playermoney.ToString();
    }
    private void UpdateShopUI()
    {
        playermoney.ToString();
    }


}