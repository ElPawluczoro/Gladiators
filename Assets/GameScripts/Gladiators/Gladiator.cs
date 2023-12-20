using System.Collections.Generic;
using GameScripts.Core;
using GameScripts.Items;
using GameScripts.Jobs;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        //items

        public Item helmet, chest, weapon;
        private int lastAD = 0, lastHP = 0, lastArmor = 0;
        
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
        public bool isLastJobFarmer = false;
        [SerializeField] private int farmerStacks = 0;
        private int maxFarmerStacks = 3;
        [SerializeField]private bool moreLikeFarmer = false;
        private bool removingFarmerStacks = false;
        
        public bool MoreLikeFarmer { get => moreLikeFarmer; }
        
        private bool _tired;
        public bool tired { get => _tired; }
        
        //Jobs
        public Dictionary<EJobs, int> bonusJobsGold = new();
        private void Start()
        {
            InitializeJobs();
            var itemsTransform = GameObject.FindGameObjectWithTag("Items").transform;
            helmet = itemsTransform.GetChild(0).GetComponent<Item>();
            weapon = itemsTransform.GetChild(1).GetComponent<Item>();
            chest = itemsTransform.GetChild(2).GetComponent<Item>();
            ToursController.onTourEnd += CheckForStacks;
        }

        public void InitializeJobs()
        {
            bonusJobsGold.Add(EJobs.FARMER, 0);
        }
        

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

        private void CheckForStacks()
        {
            CheckForFarmerStacks();
        }

        private void CheckForFarmerStacks()
        {
            if (farmerStacks == 0) return;
            if (!isLastJobFarmer && !removingFarmerStacks)
            {
                ToursController.onTourEnd += RemoveFarmerStacks;
                removingFarmerStacks = true;
            }

            if (!moreLikeFarmer && farmerStacks == 0)
            {
                ToursController.onTourEnd -= RemoveFarmerStacks;
                removingFarmerStacks = false;
            }
        }

        public void AddFarmerStacks()
        {
            if (farmerStacks < maxFarmerStacks)
            {
                farmerStacks++;
            }

            if (farmerStacks == maxFarmerStacks)
            {
                AddMoreLikeFarmerStatus();
            }
        }

        private void AddMoreLikeFarmerStatus()
        {
            moreLikeFarmer = true;
            _hitChance -= 25;
            bonusJobsGold[EJobs.FARMER] = 10;
        }

        private void RemoveMoreLikeFarmerStatus()
        {
            moreLikeFarmer = false;
            _hitChance += 25;
            bonusJobsGold[EJobs.FARMER] = 0;
        }
        
        public void RemoveFarmerStacks()
        {
            if(isLastJobFarmer) return;
            if (farmerStacks > 0)
            {
                farmerStacks--;
            }

            if (farmerStacks == 0)
            {
                RemoveMoreLikeFarmerStatus();
            }
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

        public void EquipItem(Item item)
        {
            switch (item.GetItemKind())
            {
                case ItemKind.HELMET:
                    helmet = item;
                    break;
                case ItemKind.WEAPON:
                    weapon = item;
                    break;
                case ItemKind.CHEST:
                    chest = item;
                    break;
            }
            
            UpdateStatsFromItems();
        }

        public void UpdateStatsFromItems()
        {
            var ad_ = 0;
            var hp_ = 0;
            var armor_ = 0;

            _attackDamage -= lastAD;
            _healthPoints -= lastHP;
            _armor -= lastArmor;

            if (helmet != null)
            {
                ad_ += helmet.attackDamage;
                hp_ += helmet.healthPoints;
                armor_ += helmet.armor;
            }
            
            if (weapon != null)
            {
                ad_ += weapon.attackDamage;
                hp_ += weapon.healthPoints;
                armor_ += weapon.armor;
            }

            if (chest != null)
            {
                ad_ += chest.attackDamage;
                hp_ += chest.healthPoints;
                armor_ += chest.armor;
            }
            
            _attackDamage += ad_;
            _healthPoints += hp_;
            _armor += armor_;
            
            lastAD = ad_;
            lastHP = hp_;
            lastArmor = armor_;
            
            SetDamageReduction();
        }
    }
}



















