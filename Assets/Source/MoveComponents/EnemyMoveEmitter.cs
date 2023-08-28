using UnityEngine;

namespace Source.MoveComponents
{
    public class EnemyMoveEmitter : MoveEmitter
    {
        public float StopDistance;
        [HideInInspector] public Transform Target;
    }
}
