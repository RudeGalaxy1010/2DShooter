using Source.Identity;
using UnityEngine;

namespace Source.StaticData
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "Static Data/Player Static Data")]
    public class PlayerStaticData : ScriptableObject
    {
        public EntityType EntityType;
        public GameObject Prefab;
        public float MoveSpeed;
        public int Health;
    }
}
