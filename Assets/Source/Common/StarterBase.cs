using System.Collections.Generic;
using UnityEngine;

namespace Source.Common
{
    public abstract class StarterBase : MonoBehaviour
    {
        private List<IInitable> _initables;
        private List<IDeinitable> _deinitables;
        private List<IRunable> _runables;

        private void Awake()
        {
            _initables = new List<IInitable>();
            _deinitables = new List<IDeinitable>();
            _runables = new List<IRunable>();
        }

        private void Start()
        {
            OnStart();
            Init();
        }

        private void Update()
        {
            Run();
        }

        protected abstract void OnStart();

        protected T Register<T>(T service)
        {
            if (service is IInitable initable)
            {
                _initables.Add(initable);
            }
            if (service is IDeinitable deinitable)
            {
                _deinitables.Add(deinitable);
            }
            if (service is IRunable runable)
            {
                _runables.Add(runable);
            }

            return service;
        }

        private void OnDestroy()
        {
            Deinit();
        }

        private void Init()
        {
            foreach (IInitable initable in _initables)
            {
                initable.Init();
            }
        }

        private void Run()
        {
            foreach (IRunable runable in _runables)
            {
                runable.Run();
            }
        }
        
        private void Deinit()
        {
            foreach (IDeinitable deinitable in _deinitables)
            {
                deinitable.Deinit();
            }
        }
    }
}
