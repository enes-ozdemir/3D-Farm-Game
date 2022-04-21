using UnityEngine;

namespace Farm.Profile
{
    public enum SeedPreRequirementType
    {
        WATER,
        FERTILIZER
    }

    [CreateAssetMenu(fileName = "Seed PreRequirement", menuName = "Profile/SeedPreRequirement", order = 1)]
    public class SeedPreRequirementProfile : ScriptableObject
    {
        public string Name;
        public SeedPreRequirementType Type = SeedPreRequirementType.WATER;
        public bool Enabled = true;
        public float Amount = 0f;
        public float InitialAmount = 0f;

        public SeedPreRequirement ToClass()
        {
            return new SeedPreRequirement(Name, Type, Enabled, InitialAmount, this);
        }
    }


    public class SeedPreRequirement
    {
        public string Name;
        public SeedPreRequirementType Type;
        public bool Enabled;
        public float Amount;
        public SeedPreRequirementProfile profile;

        public SeedPreRequirement(string Name, SeedPreRequirementType Type, bool Enabled, float Amount, SeedPreRequirementProfile profile)
        {
            this.Name = Name;
            this.Type = Type;
            this.Enabled = Enabled;
            this.Amount = Amount;
            this.profile = profile;
        }
    }
}