using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Arena;
using GameScripts.Core;
using GameScripts.Gladiators;
using GameScripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Jobs
{
    public class ChosenJobPanel : MonoBehaviour, IGamePanel
    {
        [SerializeField] private Image jobIcon;
        [SerializeField] private TMP_Text jobNameText;
        [SerializeField] private TMP_Text salaryInfoText;
        [SerializeField] private SOJob currentlyChosenJob;
        [SerializeField] private TMP_Text currentlyAssignedGladiator;

        [SerializeField] private GameObject gladiatorGo;
        [SerializeField] private GameObject gladiatorsContent;
        
        private PlayerGladiators _playerGladiators;
        [SerializeField] private Gladiator currentlyChosenGladiator;

        [SerializeField] private JobsController jobsController;

        private void Start()
        {
            _playerGladiators = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGladiators>();
        }

        public void chooseJob(SOJob job)
        {
            currentlyChosenJob = job;
        }


        public void ChooseGladiator(Gladiator gladiator)
        {
            currentlyChosenGladiator = gladiator;
        }

        private void InstantiatePlayerGladiators()
        {
            var assignedGladiators = jobsController.GetAssignedGladiators();
            foreach (var g in _playerGladiators.playerGladiatorsList)
            {
                if(assignedGladiators.Contains(g.GetComponent<Gladiator>())) continue;
                if(g.GetComponent<Gladiator>().tired) continue;
                var newGladiatorInJobPanel = Instantiate(gladiatorGo, gladiatorsContent.transform);
                newGladiatorInJobPanel.GetComponent<GladiatorInJobPanel>().SetProperties(g.GetComponent<Gladiator>());
            }
        }

        public void AssignGladiator()
        {
            jobsController.AssignGladiator(currentlyChosenJob, currentlyChosenGladiator);
            UpdateAssignedGladiator();
            
            ResetPlayerGladiatorsPanel();
            InstantiatePlayerGladiators();

            GameObject gladiatorObjectToDestroy = null;
            foreach (Transform gladiator in gladiatorsContent.transform)
            {
                if (gladiator.gameObject.GetComponent<Gladiator>() == currentlyChosenGladiator)
                {
                    gladiatorObjectToDestroy = gladiator.gameObject;
                }   
            }
            
            if (gladiatorObjectToDestroy != null)
            {
                Destroy(gladiatorObjectToDestroy);   
            }
        }
        public void OnPanelOpen()
        {
            UpdateAssignedGladiator();

            jobIcon.sprite = currentlyChosenJob._JobIcon;
            jobNameText.text = currentlyChosenJob._JobName;
            salaryInfoText.text = currentlyChosenJob._MinimumDefaultSalary + " - " +
                                  currentlyChosenJob._MaximumDefaultSalary;
            
            InstantiatePlayerGladiators();
        }

        private void UpdateAssignedGladiator()
        {
            var chosenGladiator = jobsController.GetCurrentlyChosenGladiator(currentlyChosenJob);
            if (chosenGladiator != null)
            {
                currentlyAssignedGladiator.text =
                    "Currently assigned: " + chosenGladiator.gladiatorName + " " + chosenGladiator.gladiatorLevel + "Lv.";
            }
            else
            {
                currentlyAssignedGladiator.text = "Currently assigned: ";
            }
        }
        
        private void ResetPlayerGladiatorsPanel()
        {
            foreach (Transform child in gladiatorsContent.transform)
            {
                Destroy(child.gameObject);
            }
        }
        

        public void OnPanelClose()
        {
            currentlyChosenJob = null;
            jobIcon.sprite = null;
            jobNameText.text = "";
            salaryInfoText.text = "";

            ResetPlayerGladiatorsPanel();
        }
    }
}