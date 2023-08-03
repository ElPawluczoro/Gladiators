using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable FunctionRecursiveOnAllPaths
// ReSharper disable ArrangeAccessorOwnerBody

namespace GameScripts.Items
{
    public class PlayerItems : MonoBehaviour
    {
        [SerializeField] private List<Item> playerItems;

        public List<Item> GetPlayerItems()
        {
            return playerItems;
        }

        public void AddItem(Item item)
        {
            playerItems.Add(item);
        }

        public void RemoveItem(Item item)
        {
            playerItems.Remove(item);
        }



    }
}