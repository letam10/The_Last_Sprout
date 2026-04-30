using UnityEngine;

namespace TheLastSprout.World
{
    public class LoadingScreenController : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetProgress(float progress01)
        {
            // Update UI progress bar
        }
    }
}
