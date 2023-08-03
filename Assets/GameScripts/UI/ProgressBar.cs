using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject bar;

        public void SetBar(int value, int maxValue, TMP_Text valueText)
        {
            float xScale;
            if (value == 0)
            {
                xScale = 0.000001f;
            }
            else xScale = (float)value / (float)maxValue;
            bar.GetComponent<RectTransform>().localScale = new Vector3(xScale, 1, 0);
            valueText.text = value + "/" + maxValue;
        }


    }
}

































