namespace TheLastSprout.Spirits
{
    public class WaterArea3x3Task : ISpiritTaskExecutor
    {
        public SpiritTaskResult ExecuteTask(SpiritTaskRequest request, float deltaTime)
        {
            // Placeholder: Call FarmGrid to water 3x3 at request.targetPosition
            return SpiritTaskResult.Success;
        }
    }

    public class HarvestMatureCropTask : ISpiritTaskExecutor
    {
        public SpiritTaskResult ExecuteTask(SpiritTaskRequest request, float deltaTime)
        {
            // Placeholder
            return SpiritTaskResult.Success;
        }
    }

    public class TransportItemTask : ISpiritTaskExecutor
    {
        public SpiritTaskResult ExecuteTask(SpiritTaskRequest request, float deltaTime)
        {
            // Placeholder
            return SpiritTaskResult.Success;
        }
    }

    public class RunMachineTask : ISpiritTaskExecutor
    {
        public SpiritTaskResult ExecuteTask(SpiritTaskRequest request, float deltaTime)
        {
            // Placeholder
            return SpiritTaskResult.Success;
        }
    }

    public class AssistCombatTask : ISpiritTaskExecutor
    {
        public SpiritTaskResult ExecuteTask(SpiritTaskRequest request, float deltaTime)
        {
            // Placeholder
            return SpiritTaskResult.Success;
        }
    }

    public class HuntTask : ISpiritTaskExecutor
    {
        public SpiritTaskResult ExecuteTask(SpiritTaskRequest request, float deltaTime)
        {
            // Placeholder
            return SpiritTaskResult.Success;
        }
    }
}
