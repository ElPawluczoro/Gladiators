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
        [SerializeField] private TMP_Text gladiatorSalaryTMP;

        [SerializeField] private GameObject statusPanelContent;
        [SerializeField] private GameObject tiredIcon;

        public void SetPanelProperties(Gladiator gladiator)
        {
            /*gladiatorNameTMP.text = gladiator.gladiatorName + " Lv." + gladiator.gladiatorLevel;
            gladiatorHealthTMP.text = gladiator.healthPoints.ToString();
            gladiatorAttackDamageTMP.text = gladiator.attackDamage.ToString();
            gladiatorArmorTMP.text = gladiator.armor.ToString();*/
            UIGenerator.SetGladiatorStats
                        (gladiator, gladiatorNameTMP, gladiatorHealthTMP, gladiatorAttackDamageTMP, gladiatorArmorTMP);
            gladiatorSalaryTMP.text = gladiator.salary.ToString();

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

            foreach (Transform child in statusPanelContent.transform)
            {
                Destroy(child.gameObject);
            }
        }

    }
}