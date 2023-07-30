using System.Collections;
using System.Collections.Generic;
using GameScripts.Gladiators;
using TMPro;
using UnityEngine;

namespace GameScripts.UI
{
    public class GladiatorPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text gladiatorNameTMP;
        [SerializeField] private TMP_Text gladiatorHealthTMP;
        [SerializeField] private TMP_Text gladiatorAttackDamageTMP;
        [SerializeField] private TMP_Text gladiatorArmorTMP;
        [SerializeField] private TMP_Text gladiatorHitChance;
        [SerializeField] private TMP_Text gladiatorSalaryTMP;

        [SerializeField] private GameObject xpBar;

        [SerializeField] private GameObject statusPanelContent;
        [SerializeField] private GameObject tiredIcon;

        public void SetPanelProperties(Gladiator gladiator)
        {
            UIGenerator.SetGladiatorStats
                        (gladiator, gladiatorNameTMP, gladiatorHealthTMP, 
                            gladiatorAttackDamageTMP, gladiatorArmorTMP, gladiatorHitChance);
            gladiatorSalaryTMP.text = gladiator.salary.ToString();

            xpBar.GetComponent<XPBar>().SetBar(gladiator.currentXP, gladiator.maxXP);
            
            if (gladiator.tired)
            {
                Instantiate(tiredIcon, statusPanelContent.transform);
            }
            
        }

        public void ResetPanel()
        {
            gladiatorNameTMP.text = "";
            gladiatorHealthTMP.text = "";
            gladiatorAttackDamageTMP.text = "";
            gladiatorArmorTMP.text = "";
            
            xpBar.GetComponent<XPBar>().SetBar(0, 1);

            foreach (Transform child in statusPanelContent.transform)
            {
                Destroy(child.gameObject);
            }
        }

    }
}