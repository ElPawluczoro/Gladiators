using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GameScripts.Gladiators;
using GameScripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScripts.Market
{
    public class SlavesStoreController : MonoBehaviour, IGamePanel
    {
        public List<GameObject> availableSlaves;

        [SerializeField] private GameObject gladiatorInStorePrefab;
        [SerializeField] private GameObject storeContent;
        
        [SerializeField] private GameObject gladiatorsGO;
        [SerializeField] private GameObject gladiatorPrefab;


        private void OnEnable()
        {
            Core.ToursController.onTourEnd += SetNewSlaves;
        }
        
        private void OnDisable()
        {
            Core.ToursController.onTourEnd -= SetNewSlaves;
        }

        public void OnPanelOpen()
        {
            InstantiateStore();
        }

        public void OnPanelClose()
        {
            
        }
        
        private void SetNewSlaves()
        {
            ResetAvailableSlaves();

            InstantiateNewSlaves();
        }

        private void InstantiateNewSlaves()
        {
            var numberOfNewSlaves = Random.Range(4, 10);
            for (int i = 0; i < numberOfNewSlaves; i++)
            {
                var newGladiator = Instantiate(gladiatorPrefab, gladiatorsGO.transform);
                newGladiator.GetComponent<Gladiator>()
                    .SetGladiatorProperties(GladiatorsGenerator.GenerateGladiatorName(), Random.Range(80, 140), Random.Range(6, 12), 1);
                availableSlaves.Add(newGladiator);
            }
        }

        private void ResetAvailableSlaves()
        {
            while (availableSlaves.Count > 0)
            {
                Destroy(availableSlaves[0].gameObject);
                availableSlaves.RemoveAt(0);
            }
        }

        public void InstantiateStore()
        {
            ResetStore();
            foreach (var g in availableSlaves)
            {
                var newGladiatorInStore = Instantiate(gladiatorInStorePrefab, storeContent.transform);
                newGladiatorInStore.GetComponent<GladiatorInStore>().LoadGladiatorInStore(g);
            }
        }

        private void ResetStore()
        {
            foreach (Transform child in storeContent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void RemoveGladiator(GameObject g)
        {
            availableSlaves.Remove(g);
        }



    }
}