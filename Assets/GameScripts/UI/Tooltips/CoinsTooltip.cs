using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameScripts.UI.Tooltips
{
    public class CoinsTooltip : MonoBehaviour, ITooltip
    {
        [SerializeField] private GameObject tooltipBox;
        [SerializeField] private TMP_Text salaryTMP;
        
        public void ShowToolTip()
        {
            tooltipBox.SetActive(true);
            salaryTMP.text = "Salary: " + PlayerPrefs.GetInt("salary", 0);
        }

        public void HideToolTip()
        {
            tooltipBox.SetActive(false);
        }
    }
}