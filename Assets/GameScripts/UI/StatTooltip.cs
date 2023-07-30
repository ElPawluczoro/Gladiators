using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace GameScripts.UI
{
    public class StatTooltip : MonoBehaviour, Tooltip
    {
        [TextArea(10,10)]
        [SerializeField] private string toolTipText;
        
        private GameObject toolTipBox;
        private TMP_Text toolTipTMP;

        private float mod = 0.0091f;

        private void Start()
        {
            toolTipBox = GameObject.FindGameObjectWithTag("ToolTipCanvas").GetComponent<TooltipCanvas>().statToolTip;
            toolTipTMP = toolTipBox.transform.GetChild(0).GetComponent<TMP_Text>();
        }

        public void ShowToolTip()
        {
            toolTipBox.SetActive(true);
            
            var transformThis = transform;
            var transformPosition = transformThis.position;

            var rectTransformThis = GetComponent<RectTransform>().rect;
            var thisX = rectTransformThis.width / 2;
            var thisY = rectTransformThis.height / 2;

            var rectTransformBox = toolTipBox.GetComponent<RectTransform>().rect;
            var boxX = rectTransformBox.width / 2;
            var boxY = rectTransformBox.height / 2;
            
            toolTipBox.transform.position = transformPosition + new Vector3((thisX + boxX) * mod, (thisY + boxY) * mod, 0);
            toolTipTMP.text = toolTipText;
        }

        public void HideToolTip()
        {
            toolTipBox.SetActive(false); 
        }
    }
}