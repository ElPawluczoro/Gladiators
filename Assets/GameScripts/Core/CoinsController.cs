using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameScripts.Core
{
    public class CoinsController : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinsText;

        private const string coins = "coins";
        void Update()
        {
            coinsText.text = PlayerPrefs.GetInt(coins, 0).ToString();
        }

        public static bool IsCoinsEnough(int v)
        {
            return PlayerPrefs.GetInt(coins, 0) >= v;
        }

        public static void SpendCoins(int v)
        {
            PlayerPrefs.SetInt(coins, PlayerPrefs.GetInt(coins, 0) - v);
            if (PlayerPrefs.GetInt(coins, 0) < 0)
            {
                PlayerPrefs.SetInt(coins, 0);
            }
        }

        public static void AddCoins(int amount)
        {
            PlayerPrefs.SetInt(coins, PlayerPrefs.GetInt(coins) + amount);
        }
        
        
        
    }   
}
