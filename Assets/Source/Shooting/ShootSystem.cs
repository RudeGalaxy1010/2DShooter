using System.Collections.Generic;
using Source.Common;
using Source.Identity;
using Source.Input;
using UnityEngine;

namespace Source.Shooting
{
    public class ShootSystem : IInitable, IDeinitable, IRunable
    {
        private readonly PlayerInput _playerInput;
        private readonly GameObject _player;
        private readonly ShootSystemEmitter _shootSystemEmitter;

        private readonly List<Bullet> _bullets;

        public ShootSystem(PlayerInput playerInput, GameObject player, ShootSystemEmitter shootSystemEmitter)
        {
            _playerInput = playerInput;
            _player = player;
            _shootSystemEmitter = shootSystemEmitter;
            _bullets = new List<Bullet>();
        }
        
        public void Init()
        {
            _playerInput.FireButtonPressed += OnFireButtonPressed;
        }
        
        public void Run()
        {
            foreach (Bullet bullet in _bullets)
            {
                bullet.Run();
            }
        }

        public void Deinit()
        {
            _playerInput.FireButtonPressed -= OnFireButtonPressed;
        }
        
        private void OnFireButtonPressed()
        {
            if (_player == null)
            {
                return;
            }
            
            Vector3 mousePosition = _playerInput.MousePosition;
            Vector3 worldMousePosition = _shootSystemEmitter.Camera.ScreenToWorldPoint(mousePosition);
            Vector3 playerPosition = _player.transform.position;
            
            Vector3 bulletDirection = (worldMousePosition - playerPosition);
            bulletDirection = new Vector3(bulletDirection.x, bulletDirection.y, 0f).normalized;

            Bullet bullet = Object.Instantiate(_shootSystemEmitter.BulletPrefab, playerPosition, Quaternion.identity);
            bullet.Construct(_player.GetComponent<Id>().EntityEntityType, bulletDirection);
            _bullets.Add(bullet);
            bullet.Destroyed += OnBulletDestroyed;
        }
        
        private void OnBulletDestroyed(Bullet bullet)
        {
            bullet.Destroyed -= OnBulletDestroyed;
            
            if (_bullets.Contains(bullet))
            {
                _bullets.Remove(bullet);
            }
        }
    }
}
