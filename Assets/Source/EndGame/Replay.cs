using Source.Common;
using Source.HealthComponents;
using UnityEngine.SceneManagement;

namespace Source.EndGame
{
    public class Replay : IInitable, IDeinitable
    {
        private readonly ReplayEmitter _replayEmitter;
        private readonly Health _playerHealth;
        private readonly Pause _pause;
        
        public Replay(ReplayEmitter replayEmitter, Health playerHealth, Pause pause)
        {
            _replayEmitter = replayEmitter;
            _playerHealth = playerHealth;
            _pause = pause;
            _pause.SetPause(false);
        }
        
        public void Init()
        {
            _replayEmitter.ReplayButton.onClick.AddListener(OnReplayButtonClicked);
            _playerHealth.Died += OnPlayerDied;
        }

        public void Deinit()
        {
            _replayEmitter.ReplayButton.onClick.RemoveListener(OnReplayButtonClicked);
            _playerHealth.Died -= OnPlayerDied;
        }
        
        private void OnReplayButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void OnPlayerDied(Health playerHealth)
        {
            _pause.SetPause(true);
            _replayEmitter.EndGamePanel.SetActive(true);
        }
    }
}
