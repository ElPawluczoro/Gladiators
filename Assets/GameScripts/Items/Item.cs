using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable ArrangeAccessorOwnerBody

namespace GameScripts.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemKind itemKind;
        [SerializeField] private string _itemName;
        [SerializeField] private int _attackDamage, _healthPoints, _armor;
        [SerializeField] public Sprite itemIcon;
        
        public string itemName { get => _itemName; }
        public int attackDamage { get => _attackDamage; }
        public int healthPoints { get => _healthPoints; }
        public int armor { get => _armor; }

        public ItemKind GetItemKind()
        {
            return itemKind;
        }
        
        
        
    }
}
