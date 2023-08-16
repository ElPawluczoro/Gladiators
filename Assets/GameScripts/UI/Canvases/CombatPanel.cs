using System;
using GameScripts.Arena;
using GameScripts.Core;
using GameScripts.Gladiators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameScripts.UI
{
    public class CombatPanel : MonoBehaviour, IGamePanel
    {
        private Gladiator currentGladiator;
        private ArenaDuelSO currentDuel;
        private GameObject enemyGO;
        private Gladiator enemyGladiator;

        [SerializeField] private GameObject gladiatorPrefab; 
        [SerializeField] private GameObject gladiatorsGO;

        [SerializeField] private GameObject playerGladiatorPanel;
        [SerializeField] private GameObject enemyGladiatorPanel;

        [SerializeField] private TMP_Text playerHealthTMP;
        [SerializeField] private TMP_Text enemyHealthTMP;

        [SerializeField] private TMP_Text playerHitText;
        [SerializeField] private TMP_Text enemyHitText;

        [SerializeField] private GameObject winnerText;
        [SerializeField] private Button nextRoundButton;
        [SerializeField] private Button skipCombatButton;
        [SerializeField] private GameObject exitButtonGo;
        [SerializeField] private GameObject rewardTextGo;
        [SerializeField] private GameObject expTextGo;

        private CanvasController canvasController;
        private ToursController tourController; 

        private void Start()
        {
            canvasController = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasController>();
            tourController = GameObject.FindGameObjectWithTag("TourController").GetComponent<ToursController>();
        }

        public void OnPanelOpen()
        {
            currentGladiator.SetGladiatorTired(true);
            GenerateEnemy();
            playerGladiatorPanel.GetComponent<GladiatorInCombat>().LoadGladiator(currentGladiator);
            enemyGladiatorPanel.GetComponent<GladiatorInCombat>().LoadGladiator(enemyGO.GetComponent<Gladiator>());
            InitiateHP();
            UpdateHealthTexts();
            nextRoundButton.interactable = true;
            skipCombatButton.interactable = true;
            winnerText.SetActive(false);
            exitButtonGo.SetActive(false);
            ResetRewardsText(rewardTextGo);
            ResetRewardsText(expTextGo);
            canvasController.BlockCanvasSwitch();
            tourController.BlockNewTour();

        }

        public void OnPanelClose()
        {
            ResetEnemy();
            ResetDuel();
            ResetGladiator();
            ResetHitTexts();
        }
        
        public void SetGladiator(Gladiator g)
        {
            currentGladiator = g;
        }
        
        public void SetDuel(ArenaDuelSO d)
        {
            currentDuel = d;
        }

        public void GenerateEnemy()
        {
            enemyGO = Instantiate(gladiatorPrefab, gladiatorsGO.transform);
            
            var lv = Random.Range(currentDuel.minEnemyLevel, currentDuel.maxEnemyLevel);
            var hp = Random.Range(90, 130);
            var ad = Random.Range(7, 11);
            for (int i = 0; i < lv - 1; i++)
            {
                var r = Random.Range(1, 2);
                if (r == 1) hp += 5;
                else ad += 1;
            }

            enemyGO.GetComponent<Gladiator>()
                .SetGladiatorProperties(GladiatorsGenerator.GenerateGladiatorName(), hp, ad, lv);
            enemyGladiator = enemyGO.GetComponent<Gladiator>();
        }

        public void ResetEnemy()
        {
            Destroy(enemyGO);
            enemyGO = null;
            enemyGladiator = null;
        }

        public void ResetDuel()
        {
            currentDuel = null;
        }

        public void ResetGladiator()
        {
            currentGladiator = null;
        }

        public void InitiateHP()
        {
            currentGladiator.currentHealthPoints = currentGladiator.healthPoints;
            enemyGladiator.currentHealthPoints = enemyGladiator.healthPoints;
        }

        public void UpdateHealthTexts()
        {
            playerHealthTMP.text = currentGladiator.currentHealthPoints + "/" + currentGladiator.healthPoints;
            enemyHealthTMP.text = enemyGladiator.currentHealthPoints + "/" + enemyGladiator.healthPoints;
        }

        public void Attack()
        {
            var playerHit = Random.Range(1, 100);
            var enemyHit = Random.Range(1, 100);
            if (playerHit <= currentGladiator.hitChance)
            {
                DealDamage(currentGladiator, enemyGladiator);
                playerHitText.text = "Hit!";
            }
            else
            {
                playerHitText.text = "Miss";
            }

            if (enemyHit <= enemyGladiator.hitChance)
            {
                DealDamage(enemyGladiator, currentGladiator);
                enemyHitText.text = "Hit!";
            }
            else
            {
                enemyHitText.text = "Miss";
            }
            UpdateHealthTexts();

            if (currentGladiator.currentHealthPoints <= 0 || enemyGladiator.currentHealthPoints <= 0)
            {
                CombatEnd();
            }
        }

        private void DealDamage(Gladiator attacker, Gladiator target)
        {
            target.currentHealthPoints -= (int)(attacker.attackDamage - attacker.attackDamage * target.damageReduction);
        }

        public void SkipCombat()
        {
            while (currentGladiator.currentHealthPoints > 0 && enemyGladiator.currentHealthPoints > 0)
            {
                Attack();
            }
        }

        public void CombatEnd()
        {
            SetWinner();
            nextRoundButton.interactable = false;
            skipCombatButton.interactable = false;
            exitButtonGo.SetActive(true);
            GiveReward();
            canvasController.UnlockCanvasSwitch();
            tourController.UnlockNewTour();
            
        }

        private void SetWinner()
        {
            winnerText.SetActive(true);
            var winnerTMP = winnerText.GetComponent<TMP_Text>();
            winnerTMP.enabled = true;
            if (currentGladiator.currentHealthPoints <= 0 && enemyGladiator.currentHealthPoints <= 0)
            {
                winnerTMP.color = Color.red;
                winnerTMP.text = "Tie";
            }
            else if (currentGladiator.currentHealthPoints <= 0)
            {
                winnerTMP.color = Color.red;
                winnerTMP.text = enemyGladiator.gladiatorName + " Won!";
            }
            else if (enemyGladiator.currentHealthPoints <= 0)
            {
                winnerTMP.color = Color.green;
                winnerTMP.text = currentGladiator.gladiatorName + " Won!";
            }
        }

        public void GiveReward()
        {
            if (currentGladiator.currentHealthPoints > 0)
            {
                var reward = Random.Range(currentDuel.minReward, currentDuel.maxReward);
                CoinsController.AddCoins(reward);

                var xpGained = Random.Range(currentDuel.minXP, currentDuel.maxXP);
                
                currentGladiator.GetXP(xpGained);
                SetRewardsText(rewardTextGo, reward, "coins");
                SetRewardsText(expTextGo, xpGained, "XP");
            }
        }

        public void SetRewardsText(GameObject textGo, int reward, string text)
        {
            textGo.SetActive(true);
            textGo.GetComponent<TMP_Text>().text = "+ " + reward + " " + text;
        }

        public void ResetRewardsText(GameObject textGo)
        {
            textGo.SetActive(false);
            textGo.GetComponent<TMP_Text>().text = " ";
        }

        public void ResetHitTexts()
        {
            playerHitText.text = " ";
            enemyHitText.text = " ";
        }
        
    }
}

























