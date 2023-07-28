using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Arena;
using UnityEngine;

namespace GameScripts.UI
{
    public class DuelChoosePanel : MonoBehaviour, IGamePanel
    {
        [SerializeField] private ArenaDuelSO[] availableDuels;
        [SerializeField] private GameObject duelsContent;
        [SerializeField] private GameObject duelPrefab;

        private void Start()
        {
            InstantiateDuels();
        }

        public void OnPanelOpen()
        {
            
        }

        public void OnPanelClose()
        {
            
        }

        public void InstantiateDuels()
        {
            foreach (var duel in availableDuels)
            {
                var newDuel = Instantiate(duelPrefab, duelsContent.transform);
                newDuel.GetComponent<DuelInMenu>().SetProperties(duel);
            }
        }
        
        
    }
}













