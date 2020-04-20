using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleHUD : MonoBehaviour
{

    public Text nameText;
    public Text levelText;
    public Text hpText;
    public Slider hpSlider;

    public void SetEnemyHUD(EnemyUnit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpText.text = unit.currentHP + " / " + unit.maxHP;
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
