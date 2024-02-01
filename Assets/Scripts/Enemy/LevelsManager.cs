using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public Shop shop;

    public void OnValidate()
    {
        if (shop == null)
        {
            shop = FindObjectOfType<Shop>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && EnemiesManager.levelClear)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("playermoney", shop.playermoney);
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
