using UnityEngine;

namespace TheLastSprout.Test
{
    /// <summary>
    /// Simple script to test MCP connection and basic script execution.
    /// </summary>
    public class MCPConnectionTest : MonoBehaviour
    {
        [SerializeField] private string testMessage = "MCP Connected Successfully!";

        private void Start()
        {
            Debug.Log($"[MCP Test] {testMessage}");
        }
    }
}
