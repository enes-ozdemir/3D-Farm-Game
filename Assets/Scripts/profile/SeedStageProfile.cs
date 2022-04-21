using System.Collections.Generic;
using UnityEngine;

namespace Farm.Profile
{
    [CreateAssetMenu(fileName = "Seed Stage", menuName = "Profile/Seed Stage", order = 1)]
    public class SeedStageProfile: ScriptableObject
    {
        public string State;
        
        public float Time;
        
        public GameObject prefab;
    }
}