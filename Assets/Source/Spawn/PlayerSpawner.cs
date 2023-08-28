using Source.HealthComponents;
using Source.Identity;
using Source.StaticData;
using UnityEngine;

namespace Source.Spawn
{
    public class PlayerSpawner
    {
        private readonly PlayerSpawnerEmitter _playerSpawnerEmitter;
        private readonly StaticDataProvider _staticDataProvider;

        public PlayerSpawner(PlayerSpawnerEmitter playerSpawnerEmitter, StaticDataProvider staticDataProvider)
        {
            _playerSpawnerEmitter = playerSpawnerEmitter;
            _staticDataProvider = staticDataProvider;
        }

        public GameObject CreatePlayer(EntityType entityType)
        {
            PlayerStaticData playerStaticData = _staticDataProvider.GetStaticDataForPlayer(entityType);
            GameObject player = Object.Instantiate(playerStaticData.Prefab, _playerSpawnerEmitter.SpawnPoint.position, Quaternion.identity);

            if (player.TryGetComponent(out Health health))
            {
                health.Construct(playerStaticData.Health);
            }
            
            return player;
        }
    }
}
