using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Factories.ObjectPool
{
    public class BaseObjectPool<TPoolableType> where TPoolableType : IPoolableObject
    {
        private readonly int _maxSize;
        private readonly IFactory<TPoolableType> _factory;
        private readonly Stack<TPoolableType> _available = new Stack<TPoolableType>();
        
        private int _totalCount;

        private const int INITIAL_SIZE = 10;
        public BaseObjectPool(
            IFactory<TPoolableType> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _maxSize = Int32.MaxValue;

            InitializePool(INITIAL_SIZE);
        }

        private void InitializePool(int initialSize)
        {
            for (int i = 0; i < initialSize && _totalCount < _maxSize; i++)
            {
                ReturnToPool(CreateNew());
            }
        }

        public TPoolableType Get()
        {
            if (_available.Count > 0)
            {
                var pooledObject = _available.Pop();
                if (pooledObject is MonoBehaviour poolableGameObject)
                    poolableGameObject.gameObject.SetActive(true);
                return pooledObject;
            }

            return CreateNew();
        }

        public void Return(TPoolableType pooledObject)
        {
            ReturnToPool(pooledObject);
        }

        private TPoolableType CreateNew()
        {
            if (_totalCount >= _maxSize)
                throw new InvalidOperationException("Pool limit reached");

            var poolableObject = _factory.Create();
            _totalCount++;
            return poolableObject;
        }

        private void ReturnToPool(TPoolableType poolableObject)
        {
            if (poolableObject is MonoBehaviour poolableGameObject)
                poolableGameObject.gameObject.SetActive(false);
            
            _available.Push(poolableObject);
        }
    }
}