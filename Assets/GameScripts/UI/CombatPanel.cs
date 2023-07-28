using GameScripts.Arena;
using GameScripts.Core;
using GameScripts.Gladiators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.UI
{
    public class CombatPanel : MonoBehaviour, IGamePanel
    {
        private Gladiator currentGladiator;
        private ArenaDuelSO currentDuel;
        private GameObject enemyGO;
        private Gladiator enemyGladiator;

        private int playerGladiatorCurrentHP;
        private int enemyCurrentHP;

        [SerializeField] private GameObject gladiatorPrefab; 
        [SerializeField] private GameObject gladiatorsGO;

        [SerializeField] private GameObject playerGladiatorPanel;
        [SerializeField] private GameObject enemyGladiatorPanel;

        [SerializeField] private TMP_Text playerHealthTMP;
        [SerializeField] private TMP_Text enemyHealthTMP;

        [SerializeField] private GameObject winnerText;
        [SerializeField] private Button nextRoundButton;
        [SerializeField] private Button skipCombatButton;
        [SerializeField] private GameObject exitButtonGo;
        [SerializeField] private GameObject rewardTextGo;
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
            ResetRewardText();

        }

        public void OnPanelClose()
        {
            ResetEnemy();
            ResetDuel();
            ResetGladiator();
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
            playerGladiatorCurrentHP = currentGladiator.healthPoints;
            enemyCurrentHP = enemyGladiator.healthPoints;
        }

        public void UpdateHealthTexts()
        {
            playerHealthTMP.text = playerGladiatorCurrentHP + "/" + currentGladiator.healthPoints;
            enemyHealthTMP.text = enemyCurrentHP + "/" + enemyGladiator.healthPoints;
        }

        public void Attack()
        {
            enemyCurrentHP -= currentGladiator.attackDamage;
            playerGladiatorCurrentHP -= enemyGladiator.attackDamage;
            UpdateHealthTexts();

            if (playerGladiatorCurrentHP <= 0 || enemyCurrentHP <= 0)
            {
                CombatEnd();
            }
        }

        public void SkipCombat()
        {
            while (playerGladiatorCurrentHP > 0 && enemyCurrentHP > 0)
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
        }

        private void SetWinner()
        {
            winnerText.SetActive(true);
            var winnerTMP = winnerText.GetComponent<TMP_Text>();
            winnerTMP.enabled = true;
            if (playerGladiatorCurrentHP <= 0 && enemyCurrentHP <= 0)
            {
                winnerTMP.color = Color.red;
                winnerTMP.text = "Tie";
            }
            else if (playerGladiatorCurrentHP <= 0)
            {
                winnerTMP.color = Color.red;
                winnerTMP.text = enemyGladiator.gladiatorName + " Won!";
            }
            else if (enemyCurrentHP <= 0)
            {
                winnerTMP.color = Color.green;
                winnerTMP.text = currentGladiator.gladiatorName + " Won!";
            }
        }

        public void GiveReward()
        {
            if (playerGladiatorCurrentHP > 0)
            {
                var reward = Random.Range(currentDuel.minReward, currentDuel.maxReward);
                CoinsController.AddCoins(reward);
                SetRewardText(reward);
            }
        }

        public void SetRewardText(int reward)
        {
            rewardTextGo.SetActive(true);
            rewardTextGo.GetComponent<TMP_Text>().text = "+ " + reward + "coins";
        }

        public void ResetRewardText()
        {
            rewardTextGo.SetActive(false);
            rewardTextGo.GetComponent<TMP_Text>().text = " ";
        }
        
    }
}

























