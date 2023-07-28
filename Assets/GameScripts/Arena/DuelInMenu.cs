using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.UI;
using TMPro;
using UnityEngine;

namespace GameScripts.Arena
{
    public class DuelInMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text difficultyTMP;
        [SerializeField] private TMP_Text levelsTMP;
        [SerializeField] private TMP_Text rewardTMP;

        private GameObject duelPanel;

        private ArenaDuelSO duel;

        public void SetProperties(ArenaDuelSO d)
        {
            duel = d;
            difficultyTMP.text = d.difficultyName;
            levelsTMP.text = "Lv." + d.minEnemyLevel + " - " + "Lv." + d.maxEnemyLevel;
            rewardTMP.text = d.minReward + " - " + d.maxReward;
        }

        public void Duel()
        {
            CanvasController canvasController =
                GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasController>();
            duelPanel = canvasController.duelPanel;
            
            duelPanel.GetComponent<DuelPanel>().SetCurrentDuel(duel);
            canvasController.SwitchPanel(duelPanel);
        }
    
    
    
    }
}
