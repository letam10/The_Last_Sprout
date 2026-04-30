using TheLastSprout.Save;
using TheLastSprout.Weather;
using UnityEngine;

namespace TheLastSprout.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        public static GameBootstrapper Instance { get; private set; }
        
        public ServiceRegistry Registry { get; private set; }

        [SerializeField] private TickManager tickManagerPrefab;
        [SerializeField] private WeatherManager weatherManagerPrefab;
        // Mock SaveManager as it's pure C# or MonoBehaviour depending on implementation. Let's assume standard object for now.
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeCore();
        }

        private void InitializeCore()
        {
            Registry = new ServiceRegistry();

            var eventBus = new EventBus();
            Registry.Register(eventBus);

            var stateMachine = new GameStateMachine();
            Registry.Register(stateMachine);

            // Tick Manager
            var tickManager = Instantiate(tickManagerPrefab != null ? tickManagerPrefab : new GameObject("TickManager").AddComponent<TickManager>(), transform);
            tickManager.Initialize();
            Registry.Register(tickManager);

            // Save Manager
            var saveManager = new SaveManager();
            saveManager.Initialize(eventBus);
            Registry.Register(saveManager);

            // Weather Manager
            var weatherManager = Instantiate(weatherManagerPrefab != null ? weatherManagerPrefab : new GameObject("WeatherManager").AddComponent<WeatherManager>(), transform);
            weatherManager.Initialize(Registry);
            Registry.Register(weatherManager);

            stateMachine.ChangeState(GameState.MainMenu);
        }
    }
}
