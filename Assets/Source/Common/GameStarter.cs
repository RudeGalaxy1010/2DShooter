using Source.Damage;
using Source.EndGame;
using Source.HealthComponents;
using Source.Identity;
using Source.Input;
using Source.MoveComponents;
using Source.Shooting;
using Source.Spawn;
using Source.StaticData;
using UnityEngine;

namespace Source.Common
{
    public class GameStarter : StarterBase
    {
        [SerializeField] private PlayerSpawnerEmitter _playerSpawnerEmitter;
        [SerializeField] private EnemySpawnerEmitter _enemySpawnerEmitter;
        [SerializeField] private DamageSystemEmitter _damageSystemEmitter;
        [SerializeField] private ShootSystemEmitter _shootSystemEmitter;
        [SerializeField] private ReplayEmitter _replayEmitter;
        
        protected override void OnStart()
        {
            StaticDataProvider staticDataProvider = new StaticDataProvider();
            staticDataProvider.LoadAll();

            GameObject player = SetupPlayer(staticDataProvider);
            SetupEnemies(player, staticDataProvider);

            Pause pause = new Pause();
            Register(new Replay(_replayEmitter, player.GetComponent<Health>(), pause));
        }
        
        private void SetupEnemies(GameObject player, StaticDataProvider staticDataProvider)
        {
            EnemySpawner enemySpawner = Register(new EnemySpawner(_enemySpawnerEmitter, player, staticDataProvider));
            Register(new EnemyMove(enemySpawner, staticDataProvider));
            Register(new DamageSystem(_damageSystemEmitter, staticDataProvider, enemySpawner));
        }

        private GameObject SetupPlayer(StaticDataProvider staticDataProvider)
        {
            PlayerInput playerInput = Register(new PlayerInput());
            PlayerSpawner playerSpawner = new PlayerSpawner(_playerSpawnerEmitter, staticDataProvider);
            GameObject player = playerSpawner.CreatePlayer(EntityType.DefaultPlayer);
            Register(new PlayerMove(player.GetComponent<MoveEmitter>(), staticDataProvider, playerInput));
            Register(new ShootSystem(playerInput, player, _shootSystemEmitter));
            return player;
        }
    }
}
