using System.Collections;
using System.Collections.Generic;
using GameScripts.Gladiators;
using GameScripts.Items;
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

        [SerializeField] private ItemSlot helmetSlot;
        [SerializeField] private ItemSlot weaponSlot;
        [SerializeField] private ItemSlot chestSlot;

        [SerializeField] private GameObject xpBar;
        [SerializeField] private TMP_Text xpBarText;

        [SerializeField] private GameObject statusPanelContent;
        [SerializeField] private GameObject tiredIcon;
        [SerializeField] private GameObject moreLikeFarmerIcon;

        public Gladiator currentGladiator;

        public void SetPanelProperties(Gladiator gladiator)
        {
            currentGladiator = gladiator;
            
            UIGenerator.SetGladiatorStats
                        (gladiator, gladiatorNameTMP, gladiatorHealthTMP, 
                            gladiatorAttackDamageTMP, gladiatorArmorTMP, gladiatorHitChance);
            gladiatorSalaryTMP.text = gladiator.salary.ToString();

            xpBar.GetComponent<ProgressBar>().SetBar(gladiator.currentXP, gladiator.maxXP, xpBarText);
            
            foreach (SOStatus status in gladiator._Statuses)
            {
                Instantiate(status._StatusGameObject, statusPanelContent.transform);
            }

            InitiateEquipment();
        }

        public void ResetPanel()
        {
            gladiatorNameTMP.text = "";
            gladiatorHealthTMP.text = "";
            gladiatorAttackDamageTMP.text = "";
            gladiatorArmorTMP.text = "";
            
            xpBar.GetComponent<ProgressBar>().SetBar(0, 1, xpBarText);

            foreach (Transform child in statusPanelContent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void InitiateEquipment()
        {
            helmetSlot.SetItem(currentGladiator.helmet);
            weaponSlot.SetItem(currentGladiator.weapon);
            chestSlot.SetItem(currentGladiator.chest);
        }

    }
}