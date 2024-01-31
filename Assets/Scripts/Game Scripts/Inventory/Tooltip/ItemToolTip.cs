using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemStatsText;

    private StringBuilder sb = new StringBuilder();
    public void ShowToolTip(EquippableItem item)
    {
        ItemNameText.text = item.ItemName;
        ItemSlotText.text = item.EquipmentType.ToString();

        sb.Length = 0;
        AddStat(item.StrengthBonus, "Strength");
        AddStat(item.ManaBonus, "Mana");
        AddStat(item.ArmorBonus, "Armor");
        AddStat(item.HpBonus, "Hp");
        AddStat(item.CritBonus, "Crit");
        AddStat(item.SpeedBouns, "Speed");

        AddStat(item.SpeedPercentBonus, "Strength", isPercent: true);
        AddStat(item.ManaPercentBonus, "Mana", isPercent: true);
        AddStat(item.ArmorPercentBonus, "Armor", isPercent: true);
        AddStat(item.HpPercentBonus, "Hp", isPercent: true);
        AddStat(item.CritPercentBonus, "Crit", isPercent: true);
        AddStat(item.SpeedPercentBonus, "Speed", isPercent: true);

        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
