using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.UI
{
    public class XPBar : MonoBehaviour
    {
        [SerializeField] private GameObject xpBar;

        public void SetBar(int xp, int maxXp)
        {
            float xScale;
            if (xp == 0)
            {
                xScale = 0.000001f;
            }
            else xScale = (float)xp / (float)maxXp;
            print(xScale + "(" + maxXp + "/" + xp + ")");
            xpBar.GetComponent<RectTransform>().localScale = new Vector3(xScale, 1, 0);
        }


    }
}

































