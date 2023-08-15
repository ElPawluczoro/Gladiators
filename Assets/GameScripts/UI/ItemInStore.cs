using GameScripts.Core;
using GameScripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.UI
{
    public class ItemInStore : MonoBehaviour
    {
        [SerializeField] private GameObject itemIcon;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemHP;
        [SerializeField] private TMP_Text itemAD;
        [SerializeField] private TMP_Text itemArmor;
        [SerializeField] private TMP_Text itemCost;

        private GameObject itemGo;

        public void InitializeUI()
        {
            var item = itemGo.GetComponent<Item>();
            itemIcon.GetComponent<Image>().sprite = item.itemIcon;
            itemName.text = item.itemName;
            itemHP.text = item.healthPoints.ToString();
            itemAD.text = item.attackDamage.ToString();
            itemArmor.text = item.armor.ToString();
            itemCost.text = item.cost.ToString();
        }

        public void BuyItem()
        {
            var cost = itemGo.GetComponent<Item>().cost;
            if (!CoinsController.IsCoinsEnough(cost)) return;
            CoinsController.SpendCoins(cost);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>().AddItem(itemGo);
            if (itemGo.GetComponent<Item>().GetItemKind() == ItemKind.WEAPON)
            {
                GameObject.FindGameObjectWithTag("WeaponSmith").GetComponent<ItemStorePanel>().RemoveItemFromStore(itemGo);
            }
            else
            {
                GameObject.FindGameObjectWithTag("Armorer").GetComponent<ItemStorePanel>().RemoveItemFromStore(itemGo);
            }
        }

        public void SetItem(GameObject itGo)
        {
            itemGo = itGo;
        }



    }
}