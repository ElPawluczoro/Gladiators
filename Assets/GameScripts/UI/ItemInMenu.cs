using System.Collections;
using System.Collections.Generic;
using GameScripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.UI
{
    public class ItemInMenu : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemStats;
        private Item item;

        public string GetItemName()
        {
            return itemName.text;
        }
        
        public void SetProperties(Item it)
        {
            item = it;
            itemIcon.sprite = it.itemIcon;
            itemName.text = it.itemName;
            
            itemStats.text = "";
            if (it.attackDamage != 0) itemStats.text += it.attackDamage + "ad ";
            if (it.healthPoints != 0) itemStats.text += it.healthPoints + "hp ";
            if (it.armor != 0) itemStats.text += it.armor + "a";
        }

        public void EquipItem()
        {
            var itemChooseMenu = GameObject.FindGameObjectWithTag("ItemChoosePanel");
            var currentItemSlot = itemChooseMenu.GetComponent<ItemChooseMenu>().currentItemSlot;
            var currentItemSlotScript = currentItemSlot.GetComponent<ItemSlot>();
            var playerItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>();

            if (currentItemSlotScript.GetItem() != null)
            {
                if (currentItemSlotScript.GetItem().itemName != "empty")
                {
                    playerItems.AddItem(currentItemSlotScript.GetItem());     
                }
            }

            currentItemSlotScript.Equip(item);
            currentItemSlot.GetComponent<Image>().sprite = item.itemIcon;

            if (item.itemName != "empty")
            {
                playerItems.RemoveItem(item);
            }
            
            itemChooseMenu.SetActive(false);
        }





    }
}