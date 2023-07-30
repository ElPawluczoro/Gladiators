using System.Collections;
using System.Collections.Generic;
using GameScripts.Gladiators;
using TMPro;
using UnityEngine;

namespace GameScripts.UI
{
    public class GladiatorInMenu : MonoBehaviour
    {
        public TMP_Text gladiatorText;
        [HideInInspector] public GameObject gladiator;

        private GameObject gladiatorsPanelGO;
        private GameObject gladiatorPanelGO;

        public void SetGladiatorText(string text)
        {
            gladiatorText.text = text;
        }

        public void LoadGladiator()
        {
            if(gladiatorsPanelGO == null) gladiatorsPanelGO = GameObject.FindGameObjectWithTag("GladiatorsPanel");
            gladiatorsPanelGO.GetComponent<GladiatorsPanel>().OpenGladiatorPanel();
            
            if(gladiatorPanelGO == null) gladiatorPanelGO = GameObject.FindGameObjectWithTag("GladiatorPanel");

            var gladiatorPanel = gladiatorPanelGO.GetComponent<GladiatorPanel>();
            gladiatorPanel.ResetPanel();
            gladiatorPanel.SetPanelProperties(gladiator.GetComponent<Gladiator>());
        }

    }
}