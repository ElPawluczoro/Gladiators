using System.Collections;
using System.Collections.Generic;
using GameScripts.Gladiators;
using TMPro;
using UnityEngine;

namespace GameScripts.UI
{
    public class GladiatorInCombat : MonoBehaviour
    {
        [SerializeField] private TMP_Text gladiatorNameTMP;
        [SerializeField] private TMP_Text healthTMP;
        [SerializeField] private TMP_Text attackDamageTMP;
        [SerializeField] private TMP_Text armorTMP;
        [SerializeField] private TMP_Text gladiatorHitChance;
        
        public void LoadGladiator(Gladiator gladiator)
        {
            UIGenerator.SetGladiatorStats(gladiator, gladiatorNameTMP, healthTMP, attackDamageTMP, armorTMP,
                gladiatorHitChance);
        }
        
    }
}