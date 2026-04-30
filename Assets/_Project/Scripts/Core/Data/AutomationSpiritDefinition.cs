using System.Collections.Generic;
using TheLastSprout.Spirits;
using UnityEngine;

namespace TheLastSprout.Core.Data
{
    [CreateAssetMenu(fileName = "NewAutomationSpirit", menuName = "The Last Sprout/Data/Automation Spirit Definition")]
    public class AutomationSpiritDefinition : BaseDefinition
    {
        public AutomationSpiritRole role;
        public float maxEnergy;
        public EnergySourceType preferredEnergySource;
        public List<SpiritAbilityDefinition> abilities = new List<SpiritAbilityDefinition>();
        
        [Header("Stats")]
        public float moveSpeed;
        public float workSpeedMultiplier;
        public int maxLevel = 10;
        
        [Header("Visuals")]
        public GameObject prefab;
    }
}
