using System;
using JvLib.Services;
using UnityEngine;

namespace Project.Gameplay
{
    [ServiceInterface(Name = "Gameplay")]
    public partial class GameplayServiceManager : MonoBehaviour, IService
    {
        public bool IsServiceReady { get; private set; }

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        private void Start()
        {
            IsServiceReady = true;
            ServiceLocator.Instance.ReportInstanceReady(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector3 pos = (_MinBound + _MaxBound) * .5f;
            Vector3 size = _MaxBound - _MinBound;
            Gizmos.DrawWireCube(pos, size);
        }
    }
}