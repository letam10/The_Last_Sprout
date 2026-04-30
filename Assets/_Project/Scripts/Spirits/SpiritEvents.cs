namespace TheLastSprout.Spirits
{
    public struct SpiritSpawnEvent { public string SpiritId; }
    public struct SpiritDespawnEvent { public string SpiritId; }
    public struct SpiritEnergyChangedEvent { public string SpiritId; public float CurrentEnergy; }
    public struct SpiritTaskAssignedEvent { public string SpiritId; public SpiritTaskType TaskType; }
    public struct SpiritTaskCompletedEvent { public string SpiritId; public SpiritTaskType TaskType; public SpiritTaskResult Result; }
}
