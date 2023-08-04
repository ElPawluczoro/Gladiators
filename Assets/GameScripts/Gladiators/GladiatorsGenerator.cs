using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace GameScripts.Gladiators
{
    public class GladiatorsGenerator : MonoBehaviour
    {
        [SerializeField] private static GameObject gladiatorsGO;
        [SerializeField] private static GameObject gladiatorPrefab;
        
        
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private static readonly string[] firstNames =
        {
            "Agrippa", "Gaius", "Marcus",
            "Paullus", "Sertor", "Titus",
            "Appius", "Gnaeus", "Mettius",
            "Postumus", "Servius", "Tullus",
            "Aulus", "Hostus", "Nonus",
            "Proculus", "Sextus", "Vibius",
            "Caeso", "Lucius", "Numerius",
            "Publius", "Spurius", "Volesus",
            "Decimus", "Mamercus", "Octavius",
            "Quintus", "Statius", "Vopiscus",
            "Faustus", "Manius", "Opiter",
            "Septimus", "Tiberius"
        };

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private static readonly string[] lastNames =
        {
            "Agelatus", "Balbin", "Brokchus",
            "Brutus", "Cato","Caecus",
            "Cepio", "Cincinnatus", "Crassus",
            "Cunctator", "Flaccus", "Flakkus",
            "Flavius", "Glaba", "Geta",
            "Grakhus", "Kaligula", "Kalwus",
            "Karakalla", "Karbo", "Katullus",
            "Longiunus", "Lukkulus", "Magnus",
            "Maksymus", "Mektator", "Nazyka",
            "Nerwa", "Piso", "Postumus",
            "Pulcher", "Rufus", "Ruso",
            "Scewola", "Saturninus", "Skaurus",
            "Strabo", "Sulla", "Verres",
            "Verrucosus", "Varo"
        };
        
        public static string GenerateGladiatorName()
        {
            string gladiatorName = "";
            gladiatorName += firstNames[Random.Range(0, firstNames.Length)];
            gladiatorName += " ";
            gladiatorName += lastNames[Random.Range(0, lastNames.Length)];

            return gladiatorName;
        }



    }
}














