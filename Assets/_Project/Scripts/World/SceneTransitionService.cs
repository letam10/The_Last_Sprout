namespace TheLastSprout.World
{
    public class SceneTransitionService
    {
        private RegionLoader _regionLoader;
        private LoadingScreenController _loadingScreen;

        public void Initialize(RegionLoader regionLoader, LoadingScreenController loadingScreen)
        {
            _regionLoader = regionLoader;
            _loadingScreen = loadingScreen;
        }

        public void BeginTransition(RegionId targetRegion, string spawnPointId)
        {
            _loadingScreen?.Show();
            
            // Placeholder for real transition logic
            _regionLoader.LoadRegionAsync(targetRegion);
            
            _loadingScreen?.Hide();
        }
    }
}
