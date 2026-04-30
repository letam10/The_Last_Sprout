using UnityEngine;

namespace TheLastSprout.Spirits
{
    public struct SpiritTaskRequest
    {
        public string taskId;
        public SpiritTaskType taskType;
        public Vector3 targetPosition;
        public IAutomationTaskTarget target;
        public float priority; // Higher = more important
    }
}
