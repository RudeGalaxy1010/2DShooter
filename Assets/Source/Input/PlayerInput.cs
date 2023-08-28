using System;
using UnityEngine;

namespace Source.Input
{
    public class PlayerInput
    {
        public event Action FireButtonPressed;
        
        private readonly DefaultControls _defaultControls;
        
        public PlayerInput()
        {
            _defaultControls = new DefaultControls();
            _defaultControls.Enable();

            _defaultControls.DefaultActionMap.Fire.performed += (_) => FireButtonPressed?.Invoke();
        }

        public float Horizontal => _defaultControls.DefaultActionMap.AxisHorizontal.ReadValue<float>();
        public float Vertical => _defaultControls.DefaultActionMap.AxisVertical.ReadValue<float>();
        public Vector2 MousePosition => _defaultControls.DefaultActionMap.MousePosition.ReadValue<Vector2>();
    }
}
