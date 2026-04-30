using System;

namespace TheLastSprout.Spirits
{
    [Serializable]
    public struct SpiritAbilityDefinition
    {
        public SpiritTaskType taskType;
        public float energyCost;
        public float baseCooldown;
        public int requiredLevel;
    }
}
