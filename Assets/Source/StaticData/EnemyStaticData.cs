using Source.Identity;
using UnityEngine;

namespace Source.StaticData
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Static Data/Enemy Static Data")]
    public class EnemyStaticData : ScriptableObject
    {
        public EntityType EntityType;
        public GameObject Prefab;
        public float Speed;
        public int Health;
        public int DamagePerTouch;
    }
}
