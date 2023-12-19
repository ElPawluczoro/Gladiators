using System.Collections;
using System.Collections.Generic;
using GameScripts.Gladiators;
using GameScripts.Jobs;
using GameScripts.UI;
using TMPro;
using UnityEngine;

public class GladiatorInJobPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text gladiatorNameTMP;
    [SerializeField] private TMP_Text healthTMP;
    [SerializeField] private TMP_Text attackDamageTMP;
    [SerializeField] private TMP_Text armorTMP;
    [SerializeField] private TMP_Text gladiatorHitChance;

    private Gladiator _gladiator;
        
    public void SetProperties(Gladiator g)
    {
        _gladiator = g;
            
        UIGenerator.SetGladiatorStats
            (g, gladiatorNameTMP, healthTMP, attackDamageTMP, armorTMP, gladiatorHitChance);
    }

    public void Choose()
    {
        ChosenJobPanel jobsPanel = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasController>()
            .jobPanel.GetComponent<ChosenJobPanel>();
            
        jobsPanel.ChooseGladiator(_gladiator);

    }
}
