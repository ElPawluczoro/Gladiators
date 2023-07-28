using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameScripts.Core
{
    public class ToursController : MonoBehaviour
    {
        [SerializeField] private TMP_Text tourText;
    
        private const string playerPrefTour = "tour";
        
        public delegate void OnTourEnd();
        public static event OnTourEnd onTourEnd;

        private void Start()
        {
            UpdateTourText();
            onTourEnd?.Invoke();
        }

        public void EndTour()
        {
            var currentTour = PlayerPrefs.GetInt(playerPrefTour, 0);
            PlayerPrefs.SetInt(playerPrefTour, currentTour + 1);
            UpdateTourText();

            onTourEnd?.Invoke();
        }

        private void UpdateTourText()
        {
            tourText.text = "Tour " + PlayerPrefs.GetInt(playerPrefTour, 0);
        }
    }
}