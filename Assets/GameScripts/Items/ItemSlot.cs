using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Gladiators;
using GameScripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Items
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private ItemKind itemKind;
        [SerializeField] private GameObject itemChoosePanel;
        [SerializeField] private GameObject itemChoosePanelContent;
        [SerializeField] private GameObject itemInMenu;

        [SerializeField] private Item item;
        
        
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("ItemChoosePanel")) return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                itemChoosePanel.SetActive(false);
                foreach (Transform child in itemChoosePanelContent.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        public void ChooseItem()
        {
            itemChoosePanel.SetActive(true);

            itemChoosePanel.GetComponent<ItemChooseMenu>().currentItemSlot = this.gameObject;
            
            var transformThis = transform;
            var transformPosition = transformThis.position;

            var rectTransformThis = GetComponent<RectTransform>().rect;
            var thisX = rectTransformThis.width / 2;
            var thisY = rectTransformThis.height / 2;

            var rectTransformBox = itemChoosePanel.GetComponent<RectTransform>().rect;
            var boxX = rectTransformBox.width / 2;
            var boxY = rectTransformBox.height / 2;

            itemChoosePanel.transform.position =
                transformPosition + new Vector3(-(thisX + boxX) * 0.0091f, -(thisY + boxY) * 0.0091f, 0);

            foreach (Item it in GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerItems>().GetPlayerItems())
            {
                if(it.GetItemKind() != itemKind) continue;
                var newItemInMenu = Instantiate(itemInMenu, itemChoosePanelContent.transform);
                newItemInMenu.GetComponent<ItemInMenu>().SetProperties(it);

            }
        }

        public void Equip(Item it)
        {
            item = it;
            var gladiatorPanel = GameObject.FindGameObjectWithTag("GladiatorPanel").GetComponent<GladiatorPanel>();
            gladiatorPanel.currentGladiator.EquipItem(it);
            gladiatorPanel.SetPanelProperties(gladiatorPanel.currentGladiator);
        }

        public Item GetItem()
        {
            return item;
        }

        public void SetItem(Item it)
        {
            item = it;
            GetComponent<Image>().sprite = it.itemIcon;
        }






    }
}