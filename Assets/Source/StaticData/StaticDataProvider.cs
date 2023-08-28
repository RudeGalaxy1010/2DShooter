using System.Collections.Generic;
using System.Linq;
using Source.Identity;
using UnityEngine;

namespace Source.StaticData
{
    public class StaticDataProvider
    {
        private const string StaticDataPath = "StaticData";

        private Dictionary<EntityType, PlayerStaticData> _playerStaticData;
        private Dictionary<EntityType, EnemyStaticData> _enemyStaticData;

        public void LoadAll()
        {
            _playerStaticData = Resources.LoadAll<PlayerStaticData>(StaticDataPath).ToDictionary(x => x.EntityType, x => x);
            _enemyStaticData = Resources.LoadAll<EnemyStaticData>(StaticDataPath).ToDictionary(x => x.EntityType, x => x);
        }

        public EnemyStaticData GetStaticDataForEnemy(EntityType entityType)
        {
            return _enemyStaticData.TryGetValue(entityType, out EnemyStaticData enemyStaticData) ? enemyStaticData : null;
        }

        public PlayerStaticData GetStaticDataForPlayer(EntityType entityType)
        {
            return _playerStaticData.TryGetValue(entityType, out PlayerStaticData playerStaticData) ? playerStaticData : null;
        }
    }
}
