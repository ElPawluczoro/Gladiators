using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.Gladiators
{
    [CreateAssetMenu(fileName = "Status", menuName = "Gladiator/Status", order = 1)]
    public class SOStatus : ScriptableObject
    {
        [SerializeField] private string statusName;
        [SerializeField] private string statusDescription;
        [SerializeField] private GameObject statusGameObject;

        public string _StatusName => statusName;

        public string _StatusDescription => statusDescription;

        public GameObject _StatusGameObject => statusGameObject;
    }
}