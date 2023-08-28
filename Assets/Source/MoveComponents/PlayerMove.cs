using Source.Common;
using Source.Input;
using Source.StaticData;
using UnityEngine;

namespace Source.MoveComponents
{
    public class PlayerMove : IRunable
    {
        private const float LimitsX = 8.25f;
        private const float LimitsY = 4.5f;
        
        private readonly MoveEmitter _moveEmitter;
        private readonly PlayerInput _playerInput;
        private readonly PlayerStaticData _playerStaticData;
        
        public PlayerMove(MoveEmitter moveEmitter, StaticDataProvider staticDataProvider, PlayerInput playerInput)
        {
            _moveEmitter = moveEmitter;
            _playerInput = playerInput;
            _playerStaticData = staticDataProvider.GetStaticDataForPlayer(_moveEmitter.Id.EntityEntityType);
        }
        
        public void Run()
        {
            if (_moveEmitter == null)
            {
                return;
            }
            
            Vector3 moveVector = new Vector3(_playerInput.Horizontal, _playerInput.Vertical) * (_playerStaticData.MoveSpeed * Time.deltaTime);
            Vector3 position = _moveEmitter.Transform.position;
            position = new Vector3(
                Mathf.Clamp(position.x + moveVector.x, -LimitsX, LimitsX),
                Mathf.Clamp(position.y + moveVector.y, -LimitsY, LimitsY));
            _moveEmitter.Transform.position = position;
        }
    }
}
