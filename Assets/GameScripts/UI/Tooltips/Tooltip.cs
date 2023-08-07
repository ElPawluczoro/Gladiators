using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameScripts.UI.Tooltips
{
    public class Tooltip : MonoBehaviour, ITooltip
    {
        [TextArea(10,10)]
        [SerializeField] protected string toolTipText;
        
        protected GameObject toolTipBox;
        private TMP_Text toolTipTMP;
        
        [SerializeField]private float mod = 0.0091f;
        protected void Start()
        {
            toolTipBox = GameObject.FindGameObjectWithTag("ToolTipCanvas").GetComponent<TooltipCanvas>().tooltipBox;
            toolTipTMP = toolTipBox.transform.GetChild(0).GetComponent<TMP_Text>();
        }
        
        public void ShowToolTip()
        {
            toolTipBox.SetActive(true);
            SetTooltipPosition();
            SetTooltipDescription(toolTipText);
        }

        public void HideToolTip()
        {
            toolTipBox.SetActive(false); 
        }

        protected void SetTooltipPosition()
        {
            var transformPosition = transform.position;

            var rectTransformThis = GetComponent<RectTransform>().rect;
            var thisX = rectTransformThis.width / 2;
            var thisY = rectTransformThis.height / 2;

            var rectTransformBox = toolTipBox.GetComponent<RectTransform>().rect;
            var boxX = rectTransformBox.width / 2;
            var boxY = rectTransformBox.height / 2;

            var x = (thisX + boxX) * mod;
            var y = (thisY + boxY) * mod;
            toolTipBox.transform.position = transformPosition + new Vector3(x, y, 0);
        }

        protected void SetTooltipDescription(string txt)
        {
            toolTipTMP.text = txt;
        }
    }
}