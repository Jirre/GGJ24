using System;
using JvLib.Services;
using Project.StateMachines;
using UnityEngine;

namespace Project.Gameplay
{
    [ServiceInterface(Name = "Gameplay")]
    public partial class GameplayServiceManager : MonoBehaviour, IService
    {
        public bool IsServiceReady { get; private set; }
        public bool IsRunning { get; private set; }

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        private void Start()
        {
            IsServiceReady = true;
            ServiceLocator.Instance.ReportInstanceReady(this);
            Svc.Ref.GameStateMachine.WaitForInstanceReady(() =>
            {
                Svc.GameStateMachine.OnStateChangedEvent += OnGameStateChanged;
            });
        }

        private void Update()
        {
            EnemySpawnerUpdate();
        }

        private void OnGameStateChanged(GameState pOld, GameState pNew)
        {
            IsRunning = pNew.StateType == GameStates.Gameplay;
        }

        public void InitGame()
        {
            EnemySpawnerInit();
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