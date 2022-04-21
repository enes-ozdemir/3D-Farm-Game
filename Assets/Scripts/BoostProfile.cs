using UnityEngine;

namespace Farm.Profile
{
    public enum BoostType
    {
        TIME_REDUCE,
        PRODUCTION_INCREMENT,
        QUALITY_INCREMENT
    }

    [CreateAssetMenu(fileName = "Boost", menuName = "Profile/Boost")]
    public class BoostProfile : ScriptableObject
    {
        public string Name;

        public BoostType Type;

        public float Value;
    }
}