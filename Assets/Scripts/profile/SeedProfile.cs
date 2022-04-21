using System.Collections.Generic;
using UnityEngine;

namespace Farm.Profile
{
    [CreateAssetMenu(fileName = "Seed", menuName = "Profile/Seed", order = 1)]
    public class SeedProfile: ScriptableObject
    {
        public string Name;

        public int ProductAmount;

        public List<SeedStageProfile> stages;
        
        public List<SeedPreRequirementProfile> preRequirementProfile;

        public Item resultProduct;
    }
}