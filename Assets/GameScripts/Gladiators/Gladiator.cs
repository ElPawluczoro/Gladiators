using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable ConvertToAutoProperty
// ReSharper disable ArrangeAccessorOwnerBody

namespace GameScripts.Gladiators
{
    public class Gladiator : MonoBehaviour
    {
        //stats
        private string _gladiatorName;
        public string gladiatorName{ get => _gladiatorName; }
        
        private int _healthPoints, _attackDamage, _armor;
        private float _damageReduction;
        public int healthPoints { get => _healthPoints; }

        public int currentHealthPoints;
        
        public int attackDamage { get => _attackDamage; }
        public int armor { get => _armor; }
        
        public float damageReduction { get => _damageReduction; }

        private int _hitChance = 75;
        public int hitChance { get => _hitChance; }
        
        
        //level
        private int _gladiatorLevel = 1;
        public int gladiatorLevel { get => _gladiatorLevel; }

        private int _currentXP, _maxXP;
        public int currentXP { get => _currentXP; }
        public int maxXP { get => _maxXP; }

        private bool canGetXP = true;

        private readonly int[] xpForLevels =
        {
            0,
            100, 250, 400, 700, 1000,
            1500, 2300, 3000, 4000, 5500
        };

        private int skillPoints = 0;
        
        //costs
        private int _buyCost, _salary;
        public int buyCost { get => _buyCost; }
        public int salary { get => _salary; }
        
        //status
        private bool _tired;
        public bool tired { get => _tired; }
        
        
        public void SetGladiatorProperties(string n, int hp, int ad, int lv)
        {
            _gladiatorName = n;
            _healthPoints = hp;
            _attackDamage = ad;
            _gladiatorLevel = lv;
            
            var priceMultiplier = Random.Range(1.00f, 2.00f);
            var priceAdd = Random.Range(10, 100);
            _buyCost = (int)(hp * priceMultiplier + ad * 10 * priceMultiplier + priceAdd);

            var salaryMultiplier = Random.Range(0.5f, 1.5f);
            _salary = (int)(_buyCost * 0.1f * salaryMultiplier);

            _maxXP = xpForLevels[1];
            
            SetDamageReduction();
        }

        private void SetDamageReduction()
        {
            _damageReduction = 0;
            var reduction = 0.02f;
            for (int i = 0; i < _armor; i++)
            {
                _damageReduction += reduction;
                reduction = reduction * 0.9f;
            }
        }

        public void SetGladiatorProperties(Gladiator gladiator)
        {
            _gladiatorName = gladiator.gladiatorName;
            _healthPoints = gladiator.healthPoints;
            _attackDamage = gladiator.attackDamage;
            _armor = gladiator.armor;
            _gladiatorLevel = gladiator.gladiatorLevel;
        }

        public void SetGladiatorTired(bool t)
        {
            _tired = t;
        }

        public void GetXP(int amount)
        {
            if (!canGetXP) return;
            _currentXP += amount;
            if (currentXP >= maxXP)
            {
                LevelUp();
            }
        }
        
        private void LevelUp()
        {
            _currentXP -= maxXP;
            _gladiatorLevel++;
            _maxXP = xpForLevels[_gladiatorLevel];
            if (_gladiatorLevel == 10)
            {
                canGetXP = false;
            }
        }


    }
}



















