using DG.Tweening;
using JvLib.Events;
using JvLib.Pooling;
using JvLib.Pooling.Objects;
using JvLib.Services;
using UnityEngine;

namespace Project.Gameplay.Enemy
{
    public class AEnemyBehaviour<TConfig, TContext> : PooledObject, IPoolListener
        where TConfig : AEnemyConfig
        where TContext : AEnemyContext<TConfig>
    {
        [SerializeField] private Transform _MeshRoot;

        protected float Duration;
        protected float CurrentTime;
        private float _rotation;
        private const float ROTATE_SPEED = 360f;
        private const float HIT_DURATION = 0.25f;
        
        protected Vector2 FieldMin;
        protected Vector2 FieldSize;
        
        private const float EXIT_LERP_SPEED = 10;
        private float _exitStartX;
        private float _exitEndX;
        private float _exitTime;
        private float _exitDuration;

        private float X => (transform.position.x - FieldMin.x) / FieldSize.x;
        private float Y => (transform.position.y - FieldMin.y) / FieldSize.y;


        protected enum EStates
        {
            Init,
            Roam,
            Hit,
            Exit
        }

        private EventStateMachine<EStates> _eventStateMachine;
        
        public void OnActivate(params object[] pContexts)
        {
            if (pContexts.Length <= 0 || pContexts[0] is not TContext context) return;
            
            Duration = context.Duration;
            Initialize(context);

            FieldMin = Svc.Gameplay.MinBound;
            FieldSize = Svc.Gameplay.MaxBound - Svc.Gameplay.MinBound;
        }
        
        protected virtual void Update()
        {
            _eventStateMachine.Update();
        }

        protected virtual void Initialize(TContext pConfig)
        {
            _eventStateMachine = new EventStateMachine<EStates>(GetType().Name);
            _eventStateMachine.Add(EStates.Init, InitState);
            _eventStateMachine.Add(EStates.Roam, RoamState);
            _eventStateMachine.Add(EStates.Hit, HitState);
            _eventStateMachine.Add(EStates.Exit, ExitState);
        }

        protected virtual void InitState(EventState<EStates> pState)
        {
            pState.GotoState(EStates.Roam);
        }
        
        protected virtual void RoamState(EventState<EStates> pState)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime >= Duration)
                pState.GotoState(EStates.Exit);
        }
        
        protected virtual void HitState(EventState<EStates> pState)
        {
            if (pState.GetRealLifeTime() > HIT_DURATION)
            {
                if (CurrentTime < Duration)
                    pState.GotoState(EStates.Roam);
                else pState.GotoState(EStates.Exit);
                
                _MeshRoot.localEulerAngles = Vector3.zero;
            }
        }

        protected virtual void ExitState(EventState<EStates> pState)
        {
            if (X <= 0.01f || X >= 0.99f)
            {
                Deactivate();
                return;
            }

            if (pState.IsFistFrame)
            {
                _exitTime = 0;
                float delta = X <= 0.5f ? X : 1f - X;
                delta *= FieldSize.x;

                _exitDuration = delta / EXIT_LERP_SPEED;

                _exitStartX = X;
                _exitEndX = X <= 0.5f ? 0f : 1f;
            }

            _exitTime += Time.deltaTime;
            
            SetPosition(new Vector2(Mathf.Lerp(_exitStartX, _exitEndX, _exitTime / _exitDuration), Y));
        }

        protected void SetPosition(Vector2 pPos)
        {
            Vector3 position = transform.position;
            float oldX = position.x;
            position = FieldMin + FieldSize * pPos;
            transform.position = position;
            
            float delta = position.x - oldX;
            if (Mathf.Abs(delta) < Time.deltaTime * 0.1f)
                return;
            
            transform.eulerAngles =
                new Vector3(0,
                    Mathf.Clamp(transform.eulerAngles.x + (delta > 0 ? Time.deltaTime : -Time.deltaTime) * ROTATE_SPEED, 0, 180), 0);
        }

        public bool Hit()
        {
            if (_eventStateMachine.IsCurrentState(EStates.Hit))
                return false;

            _MeshRoot.DOLocalRotate(Vector3.up * 360f, HIT_DURATION - 0.01f);
            return true;
        }
    }
}