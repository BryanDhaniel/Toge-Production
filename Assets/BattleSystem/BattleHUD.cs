// BattleHUD.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    public Slider hpSlider;

    public void SetHUD(BattleUnit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpText.text = unit.currentHP + " / " + unit.maxHP;
    }

    public void UpdateHP(int currentHP)
    {
        hpSlider.value = currentHP;
        hpText.text = currentHP + " / " + (int)hpSlider.maxValue;
    }
}