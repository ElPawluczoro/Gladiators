using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> canvases;
        private GameObject currentCanvas;

        public void SwitchPanel(GameObject canvas)
        {
            if (currentCanvas == canvas) return;
            currentCanvas = canvas;
            foreach (var c in canvases)
            {
                if (c.GetComponent<Canvas>().enabled)
                {
                    c.GetComponent<IGamePanel>().OnPanelClose();
                    c.GetComponent<Canvas>().enabled = false;
                }
            }

            canvas.GetComponent<Canvas>().enabled = true;
            canvas.GetComponent<IGamePanel>().OnPanelOpen();
        }
    }
}