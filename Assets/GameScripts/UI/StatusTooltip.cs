using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameScripts.UI
{
    public class StatusTooltip : MonoBehaviour, Tooltip
    {
        private GameObject tooltipBox;
        private TMP_Text descriptionTMP;

        [SerializeField] private string description;

        private void Start()
        {
            tooltipBox = GameObject.FindGameObjectWithTag("ToolTipCanvas").GetComponent<TooltipCanvas>().statusTooltip;
            descriptionTMP = tooltipBox.transform.GetChild(0).GetComponent<TMP_Text>();
        }

        public void ShowToolTip()
        {
            tooltipBox.SetActive(true);
            
            var transformThis = transform;
            var transformPosition = transformThis.position;
            var thisX = transformPosition.x;
            var thisY = transformPosition.y;

            tooltipBox.transform.position = transformPosition + new Vector3(1.1f, 0.5f, 0);
            descriptionTMP.text = description;
        }

        public void HideToolTip()
        {
            tooltipBox.SetActive(false);
        }
    }
}

















