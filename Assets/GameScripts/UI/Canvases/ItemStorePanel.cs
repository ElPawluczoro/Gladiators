using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Core;
using GameScripts.Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScripts.UI
{
    public class ItemStorePanel : MonoBehaviour, IGamePanel
    {
        private List<GameObject> itemsInShop = new List<GameObject>();

        [SerializeField] private int minItems = 5;
        [SerializeField] private int maxItems = 10;

        [SerializeField] private GameObject itemInStorePrefab;

        [SerializeField] private GameObject storeContent;
        [SerializeField] private ItemGenerator itemGenerator;

        [SerializeField] private StoreKind storeKind;

        private void OnEnable()
        {
            ToursController.onTourEnd += OnTourEnd;
        }

        private void OnDisable()
        {
            ToursController.onTourEnd -= OnTourEnd;
        }
        
        public void OnTourEnd()
        {
            ActivateShop();
            ResetShop();
            ClearShopItems();
            GenerateNewShop();
            InitializeNewShop();
            DeactivateShop();
        }

        public void OnPanelOpen()
        {
            ActivateShop();
        }

        public void OnPanelClose()
        {
            DeactivateShop();
        }

        private void GenerateNewShop()
        {
            ItemKind ik;
            for (int i = 0; i < Random.Range(minItems, maxItems); i++)
            {
                if (storeKind == StoreKind.WEAPON_SMITH) ik = ItemKind.WEAPON;
                else
                {
                    var randomNum = Random.Range(1, 3);
                    if (randomNum == 1) ik = ItemKind.CHEST;
                    else ik = ItemKind.HELMET;
                }
                var newItem = itemGenerator.GenerateNewItem(ik);
                itemsInShop.Add(newItem);
            }
        }

        private void InitializeNewShop()
        {
            foreach (var item in itemsInShop)
            {
                var newItemInStore = Instantiate(itemInStorePrefab, storeContent.transform);

                var itemInStore = newItemInStore.GetComponent<ItemInStore>();
                itemInStore.SetItem(item);
                itemInStore.InitializeUI();
            }
        }

        private void ResetShop()
        {
            foreach (Transform child in storeContent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void ClearShopItems()
        {
            foreach (var item in itemsInShop)
            {
                Destroy(item);
            }
            itemsInShop.Clear();
        }

        public void RemoveItemFromStore(GameObject item)
        {
            itemsInShop.Remove(item);
            ResetShop();
            InitializeNewShop();
        }

        public void ActivateShop()
        {
            storeContent.SetActive(true);
        }

        public void DeactivateShop()
        {
            storeContent.SetActive(false);
        }
    }

    public enum StoreKind
    {
        WEAPON_SMITH, ARMORER
    }
}

