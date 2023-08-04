using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.Items
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private Sprite[] itemSprites;
        [SerializeField] private string[] itemNames;

        [SerializeField] private GameObject emptyItemGO;
        [SerializeField] private GameObject itemsGO;

        public GameObject GenerateNewWeapon()
        {
            var newItem = Instantiate(emptyItemGO, itemsGO.transform);

            var randomNumber = Random.Range(0, itemNames.Length);
            var n = itemNames[randomNumber];
            var hp = Random.Range(0, 3);
            var ad = Random.Range(1, 15);
            var armor = 0;
            var s = itemSprites[randomNumber];

            var baseCost = Random.Range(50, 100);
            var cost = ad * 5 + hp * 10 + baseCost;
            
            newItem.GetComponent<Item>().SetProperties(ItemKind.WEAPON, n, ad, hp, armor, s, cost);
            
            return newItem;
        }
        
        
        
    }
}