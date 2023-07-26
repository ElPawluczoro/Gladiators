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
        public int _buyCost, _salary;
        public int buyCost { get => _buyCost; }
        public int salary { get => _salary; }
        
        
        
        public void SetGladiatorProperties(string name, int hp, int ad)
        {
            _gladiatorName = name;
            _healthPoints = hp;
            _attackDamage = ad;
            
            var priceMultiplier = Random.Range(1.00f, 2.00f);
            var priceAdd = Random.Range(10, 100);
            _buyCost = (int)(hp * priceMultiplier + ad * 10 * priceMultiplier + priceAdd);

            var salaryMultiplier = Random.Range(0.5f, 1.5f);
            _salary = (int)(_buyCost * 0.1f * salaryMultiplier);
        }

    }
}



















