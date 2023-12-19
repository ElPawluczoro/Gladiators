using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Arena;
using GameScripts.Core;
using GameScripts.Gladiators;
using GameScripts.Jobs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.UI
{
    public class DuelPanel : MonoBehaviour, IGamePanel
    {
        private ArenaDuelSO currentDuel;
        private Gladiator currentGladiator;

        //canvasses
        private CanvasController canvasController;
        
        [SerializeField] private GameObject arenaPanel;
        [SerializeField] private GameObject combatPanel;
        
        //gladiator
        [SerializeField] private GameObject gladiatorGo;
        [SerializeField] private GameObject gladiatorsContent;

        private PlayerGladiators _playerGladiators;
        
        //duel panel
        [SerializeField] private GameObject duelInfoPanel;
        
        [SerializeField] private TMP_Text duelDifficultyTMP;
        [SerializeField] private TMP_Text duelLevelsTMP;
        [SerializeField] private TMP_Text duelRewardTMP;
        
        //gladiator panel
        [SerializeField] private GameObject gladiatorPanel;
        
        [SerializeField] private TMP_Text gladiatorNameTMP;
        [SerializeField] private TMP_Text healthTMP;
        [SerializeField] private TMP_Text attackDamageTMP;
        [SerializeField] private TMP_Text armorTMP;
        [SerializeField] private TMP_Text gladiatorHitChance;
        
        //start cancel panel
        [SerializeField] private GameObject startCancelPanel;
        [SerializeField] private Button startButton;
        [SerializeField] private GameObject tiredText;
        
        private void Start()
        {
            canvasController = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasController>();
            _playerGladiators = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGladiators>();
            HideGladiatorPanel();
            HideStartCancelPanel();
            duelInfoPanel.SetActive(false);
        }

        public void OnPanelOpen()
        {
            duelInfoPanel.SetActive(true);
            InstantiatePlayerGladiators();
            SetDuelProperties();
        }

        public void OnPanelClose()
        {
            HideStartCancelPanel();
            ResetPlayerGladiatorsPanel();
            ResetDuel();
            ResetGladiator();
            HideGladiatorPanel();
            duelInfoPanel.SetActive(false);
        }

        private void InstantiatePlayerGladiators()
        {
            var assignedGladiators = 
                GameObject.FindGameObjectWithTag("JobsController").GetComponent<JobsController>().
                                                                                                GetAssignedGladiators();
            foreach (var g in _playerGladiators.playerGladiatorsList)
            {
                if(assignedGladiators.Contains(g.GetComponent<Gladiator>())) continue;
                var newGladiatorInDuel = Instantiate(gladiatorGo, gladiatorsContent.transform);
                newGladiatorInDuel.GetComponent<GladiatorInDuel>().SetProperties(g.GetComponent<Gladiator>());
            }
        }

        private void ResetPlayerGladiatorsPanel()
        {
            foreach (Transform child in gladiatorsContent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void SetCurrentDuel(ArenaDuelSO duel)
        {
            currentDuel = duel;
        }

        public void ResetDuel()
        {
            currentDuel = null;
        }

        private void SetDuelProperties()
        {
            duelDifficultyTMP.text = currentDuel.difficultyName + " Duel";
            duelLevelsTMP.text = "Lv." + currentDuel.minEnemyLevel + " - Lv." + currentDuel.maxEnemyLevel;
            duelRewardTMP.text = currentDuel.minReward + " - " + currentDuel.maxReward;
        }

        public void ChooseGladiator(Gladiator gladiator)
        {
            currentGladiator = gladiator;
            if (currentGladiator.tired)
            {
                startButton.interactable = false;
                tiredText.SetActive(true);
            }
            else
            {
                startButton.interactable = true;
                tiredText.SetActive(false);
            }
        }

        public void ResetGladiator()
        {
            currentGladiator = null;
        }

        public void HideGladiatorPanel()
        {
            gladiatorPanel.SetActive(false);
        }

        public void ShowStartCancelPanel()
        {
            startCancelPanel.SetActive(true);
        }

        public void HideStartCancelPanel()
        {
            startCancelPanel.SetActive(false);
        }
        
        public void LoadGladiator(Gladiator gladiator)
        {
            gladiatorPanel.SetActive(true);

            UIGenerator.SetGladiatorStats
                (gladiator, gladiatorNameTMP, healthTMP, attackDamageTMP, armorTMP, gladiatorHitChance);
        }

        public void StartDuel()
        {            
            var combatPanelScript = combatPanel.GetComponent<CombatPanel>();
            combatPanelScript.SetDuel(currentDuel);
            combatPanelScript.SetGladiator(currentGladiator);
            canvasController.SwitchPanel(combatPanel);
        }

        public void CancelDuel()
        {
            canvasController.SwitchPanel(arenaPanel);
        }
        
        
    }
}
