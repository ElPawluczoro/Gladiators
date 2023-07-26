using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Core;
using GameScripts.Gladiators;
using UnityEngine;

namespace GameScripts.UI
{
    public class GladiatorsPanel : MonoBehaviour, IGamePanel
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<GameObject> lastPlayerGladiatorsList = new List<GameObject>();
        [SerializeField] private GameObject gladiatorsListContent;
        [SerializeField] private GameObject gladiatorInMenuPrefab;
        [SerializeField] private GameObject gladiatorPanel;
        
        private PlayerGladiators playerGladiators;

        public GameObject glad;
        
        
        private void Start()
        {
            playerGladiators = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGladiators>();
        }

        public void OnPanelOpen()
        {
            if (!IsGladiatorsListStillSame())
            {
                InstantiateGladiatorsList();
            }
        }

        public void OnPanelClose()
        {
            CloseGladiatorPanel();
        }

        public void InstantiateGladiatorsList()
        {
            UpdateLastGladiatorsList();
            var gladiatorsCount = gladiatorsListContent.transform.childCount;
            for (int i = 0; i < gladiatorsCount; i++)
            {
                Destroy(gladiatorsListContent.transform.GetChild(gladiatorsCount - (1 + i)).gameObject);
            }
            
            foreach (var g in playerGladiators.playerGladiatorsList)
            {
                var newGladiator = Instantiate(gladiatorInMenuPrefab, gladiatorsListContent.transform);
                var gladiatorInMenu = newGladiator.GetComponent<GladiatorInMenu>();
                gladiatorInMenu.gladiator = g;
                var gladiator = g.GetComponent<Gladiator>();
                gladiatorInMenu.SetGladiatorText(gladiator.gladiatorName + "Lv." + gladiator.gladiatorLevel);
            }
        }

        private bool IsGladiatorsListStillSame()
        {
            var playerGladiatorsList = playerGladiators.playerGladiatorsList;
            if (lastPlayerGladiatorsList.Count != playerGladiatorsList.Count){ return false;}
            foreach (var g in playerGladiatorsList)
            {
                if (!lastPlayerGladiatorsList.Contains(g)) return false;
            }
            return true;
        }

        private void UpdateLastGladiatorsList()
        {
            foreach (var g in playerGladiators.playerGladiatorsList)
            {
                if (!lastPlayerGladiatorsList.Contains(g))
                {
                    lastPlayerGladiatorsList.Add(g);
                }
            }

            var gladiatorsToRemove = new List<GameObject>();
            foreach (var g in lastPlayerGladiatorsList)
            {
                if (!playerGladiators.playerGladiatorsList.Contains(g))
                {
                    gladiatorsToRemove.Add(g);
                }
            }

            foreach (var g in gladiatorsToRemove)
            {
                lastPlayerGladiatorsList.Remove(g);
            }
        }

        public void OpenGladiatorPanel()
        {
            gladiatorPanel.SetActive(true);
        }

        public void CloseGladiatorPanel()
        {
            gladiatorPanel.SetActive(false);
        }
        
    }
}
















