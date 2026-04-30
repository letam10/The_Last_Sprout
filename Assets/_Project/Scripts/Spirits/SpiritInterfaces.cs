using UnityEngine;

namespace TheLastSprout.Spirits
{
    public interface IAutomationTaskTarget
    {
        string TargetId { get; }
        Vector3 TargetPosition { get; }
        bool IsValid();
    }

    public interface ISpiritTaskProvider
    {
        SpiritTaskRequest? GetNextTask(AutomationSpiritRole role, Vector3 spiritPosition, int spiritLevel);
    }

    public interface ISpiritEnergySource
    {
        EnergySourceType SourceType { get; }
        float ConsumeEnergy(float amountRequested);
    }

    public interface ISpiritTaskExecutor
    {
        SpiritTaskResult ExecuteTask(SpiritTaskRequest request, float deltaTime);
    }
}
