using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public Image[] healthPoints;
    public Sprite FullPoint;
    public Sprite EmptyPoint;

    public Text damageText;
    private int damageValue;

    public Text enemiesCount;
    private int enemiesCountValue;

    private void Start()
    {

    }

    private void Update()
    {
        for (float i = 0; i < healthPoints.Length; i++)
        {
            if (i < PlayerHealthSystem.currentHealth)
            {
                healthPoints[(int)Math.Round(i)].sprite = FullPoint;
            }
            else
            {
                healthPoints[(int)Math.Round(i)].sprite = EmptyPoint;
            }

            if (i < PlayerHealthSystem.maxHealth)
            {
                healthPoints[(int)Math.Round(i)].enabled = true;
            }
            else
            {
                healthPoints[(int)Math.Round(i)].enabled = false;
            }
        }

        damageValue = Weapon.weaponDamage;
        damageText.text = damageValue.ToString();

        enemiesCountValue = EnemiesManager.enemiesOnLvl;
        enemiesCount.text = enemiesCountValue.ToString();
    }
}
