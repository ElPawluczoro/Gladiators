using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameScripts.UI
{
    public class TooltipsController : MonoBehaviour
    {
        [SerializeField] private GameObject emptyTooltip;
        [SerializeField] private GameObject lastTooltip;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (lastTooltip == null)
            {
                lastTooltip = emptyTooltip;
            }
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var tooltipComponent = hit.transform.gameObject.GetComponent<ITooltip>();
                if (tooltipComponent != null)
                {
                    lastTooltip = hit.transform.gameObject;
                    tooltipComponent.ShowToolTip();
                }
            }
            else if (lastTooltip.activeSelf)
            {
                lastTooltip.GetComponent<ITooltip>().HideToolTip();
            }
        }
    }
}