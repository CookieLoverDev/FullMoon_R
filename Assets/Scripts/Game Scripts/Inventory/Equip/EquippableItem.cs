using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Potion,
    Boots,
    Chest,
    Helmet,
    Pants,
}
[CreateAssetMenu]
public class EquippableItem : Item
{
    public int HpBonus;
    public int ManaBonus;
    public int ArmorBonus;
    public int StrengthBonus;
    public int CritBonus;
    public int SpeedBouns;
    [Space]
    public float StrenghtPercentBonus;
    public float ManaPercentBonus;
    public float SpeedPercentBonus;
    public float ArmorPercentBonus;
    public float CritPercentBonus;
    public float HpPercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }

    public void Equip(Character c)
    {
        if (StrengthBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (ManaBonus != 0)
            c.Mana.AddModifier(new StatModifier(ManaBonus, StatModType.Flat, this));
        if (ArmorBonus != 0)
            c.Armor.AddModifier(new StatModifier(ArmorBonus, StatModType.Flat, this));
        if (HpBonus != 0)
            c.Hp.AddModifier(new StatModifier(HpBonus, StatModType.Flat, this));
        if (SpeedBouns != 0)
            c.Speed.AddModifier(new StatModifier(SpeedBouns, StatModType.Flat, this));
        if (CritBonus != 0)
            c.Crit.AddModifier(new StatModifier(CritBonus, StatModType.Flat, this));

        if (StrenghtPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrenghtPercentBonus, StatModType.PercentMult, this));
        if (ManaPercentBonus != 0)
            c.Mana.AddModifier(new StatModifier(ManaPercentBonus, StatModType.PercentMult, this));
        if (ArmorPercentBonus != 0)
            c.Armor.AddModifier(new StatModifier(ArmorPercentBonus, StatModType.PercentMult, this));
        if (HpPercentBonus != 0)
            c.Hp.AddModifier(new StatModifier(HpPercentBonus, StatModType.PercentMult, this));
        if (CritPercentBonus != 0)
            c.Crit.AddModifier(new StatModifier(CritPercentBonus, StatModType.PercentMult, this));
        if (SpeedPercentBonus != 0)
            c.Speed.AddModifier(new StatModifier(SpeedPercentBonus, StatModType.PercentMult, this));
    }

    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifierFromSource(this);
        c.Mana.RemoveAllModifierFromSource(this);
        c.Hp.RemoveAllModifierFromSource(this);
        c.Armor.RemoveAllModifierFromSource(this);
        c.Crit.RemoveAllModifierFromSource(this);
        c.Speed.RemoveAllModifierFromSource(this);
    }
}
