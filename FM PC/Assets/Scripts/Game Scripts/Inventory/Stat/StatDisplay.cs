using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CharacterStat _stat;
    public CharacterStat Stat
    {
        get { return _stat; }
        set
        {
            _stat = value;
            UpdateStatValue();
        }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            nameStat.text = _name.ToLower();
        }
    }

    [SerializeField] Text nameStat;
    [SerializeField] Text valueStat;
    [SerializeField] StatToolTip tooltip;
    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameStat = texts[0];
        valueStat = texts[1];

        if (tooltip == null)
            tooltip = FindObjectOfType<StatToolTip>();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowToolTip(Stat, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    public void UpdateStatValue()
    {
        valueStat.text = _stat.Value.ToString();
    }

}
