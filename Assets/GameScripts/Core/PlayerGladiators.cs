using System;
using System.Collections.Generic;
using GameScripts.Gladiators;
using UnityEngine;

namespace GameScripts.Core
{
    public class PlayerGladiators : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _playerGladiators;
        public List<GameObject> playerGladiatorsList { get => _playerGladiators; }

        private string salary = "salary";

        private StatusesHolder statusesHolder;

        private void Start()
        {
            statusesHolder = GameObject.FindGameObjectWithTag("StatusesHolder").GetComponent<StatusesHolder>();
        }

        private void OnEnable()
        {
            ToursController.onTourEnd += PaySalary;
            ToursController.onTourEnd += SetGladiatorsNoLongerTired;
        }

        private void OnDisable()
        {
            ToursController.onTourEnd -= PaySalary;
            ToursController.onTourEnd -= SetGladiatorsNoLongerTired;
        }

        public void AddGladiator(GameObject gladiator)
        {
            _playerGladiators.Add(gladiator);
            UpdateSalary();
        }

        public void RemoveGladiator(int i)
        {
            _playerGladiators.RemoveAt(0);
            UpdateSalary();
        }

        public void UpdateSalary()
        {
            int localSalary = 0;
            foreach (var g in _playerGladiators)
            {
                localSalary += g.GetComponent<Gladiator>().salary;
            }
            PlayerPrefs.SetInt(salary, localSalary);
        }

        public void PaySalary()
        {
            CoinsController.SpendCoins(PlayerPrefs.GetInt(salary, 0));
        }

        public void SetGladiatorsNoLongerTired()
        {
            foreach (var g in _playerGladiators)
            {
                g.GetComponent<Gladiator>().RemoveStatus(statusesHolder.tiredStatus);
            }
        }
        
        
    }
}









