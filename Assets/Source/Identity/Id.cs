using UnityEngine;

namespace Source.Identity
{
    public class Id : MonoBehaviour
    {
        [SerializeField] private EntityType _entityType;

        public EntityType EntityEntityType => _entityType;
    }
}
