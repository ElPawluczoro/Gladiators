using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Core;
using UnityEngine;

namespace GameScripts.UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> canvases;
        private GameObject currentCanvas;

        public GameObject duelPanel;

        private bool canSwitchCanvas;

        private void OnEnable()
        {
            ToursController.onTourEnd += OnTourEndBehaviour;
        }
        
        private void OnDisable()
        {
            ToursController.onTourEnd -= OnTourEndBehaviour;
        }

        private void Start()
        {
            canSwitchCanvas = true;
        }

        public void SwitchPanel(GameObject canvas)
        {
            if (!canSwitchCanvas) return;
            
            if (currentCanvas == canvas) return;
            currentCanvas = canvas;
            CloseAllCanvases();

            canvas.GetComponent<Canvas>().enabled = true;
            canvas.GetComponent<IGamePanel>().OnPanelOpen();
        }

        private void CloseAllCanvases()
        {
            foreach (var c in canvases)
            {
                if (c.GetComponent<Canvas>().enabled)
                {
                    c.GetComponent<IGamePanel>().OnPanelClose();
                    c.GetComponent<Canvas>().enabled = false;
                }
            }
        }

        private void OnTourEndBehaviour()
        {
            CloseAllCanvases();
            currentCanvas = null;
        }

        public void BlockCanvasSwitch()
        {
            canSwitchCanvas = false;
        }

        public void UnlockCanvasSwitch()
        {
            canSwitchCanvas = true;
        }
        

    }
}