using System.Collections;
using System.Collections.Generic;
using GameScripts.Gladiators;
using TMPro;
using UnityEngine;

namespace GameScripts.UI
{
    public class UIGenerator : MonoBehaviour
    {
        public static void SetGladiatorStats
                        (Gladiator gladiator, 
                        TMP_Text gladiatorNameTMP, TMP_Text gladiatorHealthTMP, 
                        TMP_Text gladiatorAttackDamageTMP, TMP_Text gladiatorArmorTMP)
        {
            gladiatorNameTMP.text = gladiator.gladiatorName + " Lv." + gladiator.gladiatorLevel;
            gladiatorHealthTMP.text = gladiator.healthPoints.ToString();
            gladiatorAttackDamageTMP.text = gladiator.attackDamage.ToString();
            gladiatorArmorTMP.text = gladiator.armor.ToString();
        }
        
        
        
    }
}