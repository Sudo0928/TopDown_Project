using System;
using UnityEngine;

namespace TopDown_Project
{
    public interface IPoolable
    {
        void Initialize(Action<GameObject> returnAction);
        void OnSpawn();
        void OnDespawn();
    }
}
