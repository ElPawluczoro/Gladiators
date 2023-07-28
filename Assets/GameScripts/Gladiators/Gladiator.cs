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
        [SerializeField] private string _gladiatorName;
        public string gladiatorName{ get => _gladiatorName; }
        
        [SerializeField] private int _healthPoints, _attackDamage, _armor;
        public int healthPoints { get => _healthPoints; }
        public int attackDamage { get => _attackDamage; }
        public int armor { get => _armor; }
        
        //level
        [SerializeField] private int _gladiatorLevel = 1;
        public int gladiatorLevel { get => _gladiatorLevel; }

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



    }
}



















