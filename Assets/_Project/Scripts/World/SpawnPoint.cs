using UnityEngine;

namespace TheLastSprout.World
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private string id;

        public string Id => id;
        public Vector3 Position => transform.position;
    }
}
