using System.Collections;
using System.Collections.Generic;
using GameScripts.Core;
using GameScripts.Gladiators;
using GameScripts.Market;
using TMPro;
using UnityEngine;


namespace GameScripts.UI
{
    public class GladiatorInStore : MonoBehaviour
    {
        [SerializeField] private TMP_Text gladiatorName;
        [SerializeField] private TMP_Text gladiatorHP;
        [SerializeField] private TMP_Text gladiatorAD;
        [SerializeField] private TMP_Text gladiatorArmor;
        [SerializeField] private TMP_Text gladiatorCost;
        [SerializeField] private TMP_Text gladiatorSalary;

        public GameObject gladiator;

        public void LoadGladiatorInStore(GameObject g)
        {
            gladiator = g;
            var localGladiator = g.GetComponent<Gladiator>();
            UIGenerator.SetGladiatorStats
                (g.GetComponent<Gladiator>(), gladiatorName, gladiatorHP, gladiatorAD, gladiatorArmor);
            gladiatorCost.text = localGladiator.buyCost.ToString();
            gladiatorSalary.text = localGladiator.salary.ToString();
        }

        public void BuyGladiator()
        {
            var buyCost = gladiator.GetComponent<Gladiator>().buyCost;
            if (CoinsController.IsCoinsEnough(buyCost))
            {
                CoinsController.SpendCoins(buyCost);
                
                var playerGladiators = GameObject.FindGameObjectWithTag("Player")
                    .GetComponent<PlayerGladiators>();
                var slavesStoreController = GameObject.FindGameObjectWithTag("SlavesStoreController")
                    .GetComponent<SlavesStoreController>();
            
                playerGladiators.AddGladiator(gladiator);
                slavesStoreController.RemoveGladiator(gladiator);
                slavesStoreController.InstantiateStore();
            }
        }
        


    }
}