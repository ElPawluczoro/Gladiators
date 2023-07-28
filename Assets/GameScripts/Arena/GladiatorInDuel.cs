using System.Collections;
using System.Collections.Generic;
using GameScripts.Gladiators;
using GameScripts.UI;
using TMPro;
using UnityEngine;

namespace GameScripts.Arena
{
    public class GladiatorInDuel : MonoBehaviour
    {
        [SerializeField] private TMP_Text gladiatorNameTMP;
        [SerializeField] private TMP_Text healthTMP;
        [SerializeField] private TMP_Text attackDamageTMP;
        [SerializeField] private TMP_Text armorTMP;

        private Gladiator _gladiator;
        
        public void SetProperties(Gladiator g)
        {
            _gladiator = g;
            
            UIGenerator.SetGladiatorStats
                (g, gladiatorNameTMP, healthTMP, attackDamageTMP, armorTMP);
        }

        public void Choose()
        {
            DuelPanel duelPanel = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasController>()
                .duelPanel.GetComponent<DuelPanel>();
            
            duelPanel.ChooseGladiator(_gladiator);
            duelPanel.LoadGladiator(_gladiator);
            duelPanel.ShowStartCancelPanel();
            
        }
        
        
        

    }
}
