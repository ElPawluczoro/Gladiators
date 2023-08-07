using GameScripts.Gladiators;
using GameScripts.Items;
using UnityEngine;

namespace GameScripts.UI.Tooltips
{
    public class ItemTooltip : Tooltip, ITooltip
    {
        [SerializeField] private ItemKind itemKind;
        [SerializeField] private GladiatorPanel gladiatorPanel;

        private new void Start()
        {
            base.Start();
            gladiatorPanel = GameObject.FindGameObjectWithTag("GladiatorPanel").GetComponent<GladiatorPanel>();
        }

        public new void ShowToolTip()
        {
            toolTipBox.SetActive(true);
            SetTooltipPosition();
            SetTooltipDescription(GetItemText());
        }

        private string GetItemText()
        {
            Item item;
            var gladiator = gladiatorPanel.currentGladiator;
            switch (itemKind)
            {
                case ItemKind.WEAPON:
                    item = gladiator.weapon;
                    break;
                case ItemKind.HELMET:
                    item = gladiator.helmet;
                    break;
                case ItemKind.CHEST:
                    item = gladiator.chest;
                    break;
                default:
                    item = gladiator.chest;
                    break;
            }

            var txt = "";
            txt += item.itemName + "\n";
            if(item.healthPoints != 0) { txt += item.healthPoints + "hp" + "\n"; }
            if(item.attackDamage != 0) { txt += item.attackDamage + "ad"+ "\n"; }
            if(item.armor != 0) { txt += item.armor + "a"; }

            return txt;
        }
        
        
        
        
    }
}