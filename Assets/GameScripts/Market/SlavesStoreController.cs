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


        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private string[] firstNames =
        {
            "Agrippa", "Gaius", "Marcus",
            "Paullus", "Sertor", "Titus",
            "Appius", "Gnaeus", "Mettius",
            "Postumus", "Servius", "Tullus",
            "Aulus", "Hostus", "Nonus",
            "Proculus", "Sextus", "Vibius",
            "Caeso", "Lucius", "Numerius",
            "Publius", "Spurius", "Volesus",
            "Decimus", "Mamercus", "Octavius",
            "Quintus", "Statius", "Vopiscus",
            "Faustus", "Manius", "Opiter",
            "Septimus", "Tiberius"
        };

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private string[] lastNames =
        {
            "Agelatus", "Balbin", "Brokchus",
            "Brutus", "Cato","Caecus",
            "Cepio", "Cincinnatus", "Crassus",
            "Cunctator", "Flaccus", "Flakkus",
            "Flavius", "Glaba", "Geta",
            "Grakhus", "Kaligula", "Kalwus",
            "Karakalla", "Karbo", "Katullus",
            "Longiunus", "Lukkulus", "Magnus",
            "Maksymus", "Mektator", "Nazyka",
            "Nerwa", "Piso", "Postumus",
            "Pulcher", "Rufus", "Ruso",
            "Scewola", "Saturninus", "Skaurus",
            "Strabo", "Sulla", "Verres",
            "Verrucosus", "Varo"
        };
        
        
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
                    .SetGladiatorProperties(GenerateGladiatorName(), Random.Range(80, 140), Random.Range(6, 12));
                availableSlaves.Add(newGladiator);
            }
        }

        public string GenerateGladiatorName()
        {
            string gladiatorName = "";
            gladiatorName += firstNames[Random.Range(0, firstNames.Length)];
            gladiatorName += " ";
            gladiatorName += lastNames[Random.Range(0, lastNames.Length)];

            return gladiatorName;
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