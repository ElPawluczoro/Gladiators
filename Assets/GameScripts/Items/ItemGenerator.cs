using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.Items
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private Sprite[] itemSpritesWeapon;
        [SerializeField] private string[] itemNamesWeapon;
        [SerializeField] private Sprite[] itemSpritesBodyArmor;
        [SerializeField] private string[] itemNamesBodyArmor;
        [SerializeField] private Sprite[] itemSpritesHelmet;
        [SerializeField] private string[] itemNamesHelmet;

        [SerializeField] private GameObject emptyItemGO;
        [SerializeField] private GameObject itemsGO;

        public GameObject GenerateNewItem(ItemKind itemKind)
        {
            var newItem = Instantiate(emptyItemGO, itemsGO.transform);
            
            int randomNumber = 0;
            string n = "";
            int hp = 0;
            int ad = 0;
            int armor = 0;
            Sprite s;
            
            int baseCost = 0;

            switch (itemKind)
            {
                case ItemKind.WEAPON:
                    randomNumber = Random.Range(0, itemNamesWeapon.Length);
                    n = itemNamesWeapon[randomNumber];
                    hp = Random.Range(0, 3);
                    ad = Random.Range(1, 15);
                    armor = 0;
                    s = itemSpritesWeapon[randomNumber];
                    baseCost = Random.Range(50, 100);
                    break;
                case ItemKind.HELMET:
                    randomNumber = Random.Range(0, itemNamesHelmet.Length);
                    n = itemNamesHelmet[randomNumber];
                    hp = Random.Range(0, 10);
                    ad = 0;
                    armor = Random.Range(1, 15);
                    s = itemSpritesHelmet[randomNumber];
                    baseCost = Random.Range(40, 90);
                    break;
                case ItemKind.CHEST:
                    randomNumber = Random.Range(0, itemNamesBodyArmor.Length);
                    n = itemNamesBodyArmor[randomNumber];
                    hp = Random.Range(1, 15);
                    ad = 0;
                    armor = Random.Range(2, 30);
                    s = itemSpritesBodyArmor[randomNumber];
                    baseCost = Random.Range(70, 150);
                    break;
                default:
                    randomNumber = 0;
                    n = "BadInput";
                    hp = 0;
                    ad = 0;
                    armor = 0;
                    s = itemSpritesWeapon[randomNumber];
                    baseCost = 0;
                    break;
            }
            
            int cost = ad * 5 + hp * 10 + armor * 25 + baseCost;
            
            newItem.GetComponent<Item>().SetProperties(itemKind, n, ad, hp, armor, s, cost);
            
            return newItem;
        }
        
        
        
    }
}