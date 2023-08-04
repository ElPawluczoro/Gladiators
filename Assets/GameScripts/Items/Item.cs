using UnityEngine;

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

        public int cost;

        public ItemKind GetItemKind()
        {
            return itemKind;
        }

        public void SetProperties(ItemKind ik, string n, int ad, int hp, int a, Sprite s, int c)
        {
            itemKind = ik;
            _itemName = n;
            _healthPoints = hp;
            _attackDamage = ad;
            _armor = a;
            itemIcon = s;
            cost = c;
        }
        
        
    }
}
