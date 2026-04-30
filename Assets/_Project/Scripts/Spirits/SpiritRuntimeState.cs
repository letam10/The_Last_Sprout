using System;
using System.Collections.Generic;

namespace TheLastSprout.Spirits
{
    [Serializable]
    public class SpiritRuntimeState
    {
        public string instanceId;
        public string definitionId;
        public int level;
        public float currentXp;
        public float currentEnergy;
        public bool isActive;
        public float cooldownTimer;
        public List<string> assignedTaskIds = new List<string>();
    }
}
