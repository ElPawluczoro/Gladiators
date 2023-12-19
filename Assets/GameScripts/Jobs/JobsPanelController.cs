using System.Collections;
using System.Collections.Generic;
using GameScripts.UI;
using UnityEngine;

namespace GameScripts.Jobs
{
    public class JobsPanelController : MonoBehaviour, IGamePanel
    {
        [SerializeField] private GameObject chosenJobPanel;
        public void ChooseJobButton(SOJob job)
        {
            chosenJobPanel.GetComponent<ChosenJobPanel>().chooseJob(job);
        }


        public void OnPanelOpen()
        {
            
        }

        public void OnPanelClose()
        {
            
        }
    }
}
