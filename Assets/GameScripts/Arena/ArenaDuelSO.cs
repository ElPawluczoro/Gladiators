using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.Arena
{
    [CreateAssetMenu(fileName = "Duel", menuName = "Arena/Duel", order = 1)]
    public class ArenaDuelSO : ScriptableObject
    {
        public string difficultyName = "Normal";
        
        public int minEnemyLevel = 1;
        public int maxEnemyLevel = 1;

        public int minReward = 50;
        public int maxReward = 100;

        public int minXP = 10;
        public int maxXP = 20;

    }
}







































