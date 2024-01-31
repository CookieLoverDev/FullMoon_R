using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueOperatorScript : MonoBehaviour
{
    public Text healthT;
    public Text manaT;
    public Text armorT;
    public Text damageT;
    public Text critT;
    public Text movementSpeedT;


    private void Update()
    {
        healthT.text = PlayerHealthSystem.maxHealth.ToString();
        manaT.text = Combat.mana.ToString();
        armorT.text = PlayerHealthSystem.armor.ToString();
        damageT.text = Weapon.weaponDamage.ToString();
        critT.text = "0";
        movementSpeedT.text = "3";
    }
}
