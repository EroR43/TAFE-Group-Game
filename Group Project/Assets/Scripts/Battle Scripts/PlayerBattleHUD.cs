using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleHUD : MonoBehaviour
{

    public Text nameText;
    public Text levelText;
    public Text hpText;
    public Text expText;
    public Slider hpSlider;
    public Slider expSlider;

    public void SetPlayerHUD(PlayerUnit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpText.text = unit.currentHP + " / " + unit.maxHP;
        expSlider.maxValue = unit.maxEXP;
        expSlider.value = unit.currentEXP;
        expText.text = unit.currentEXP + " / " + unit.maxEXP;

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetHPText(int curhp, int maxhp)
    {
        hpText.text = curhp + " / " + maxhp;
    }
}
