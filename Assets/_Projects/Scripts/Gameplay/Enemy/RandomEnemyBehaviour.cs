using JvLib.Events;
using UnityEngine;

namespace Project.Gameplay.Enemy
{
    public class RandomEnemyBehaviour : AEnemyBehaviour<RandomEnemyConfig, RandomEnemyContext>
    {
        private Vector2 _minRange;
        private Vector2 _maxRange;
        private float _speed;
        
        private Vector2 _origin;
        private Vector2 _target;
        private float _lerpTime;
        private float _lerpDuration;

        protected override void Initialize(RandomEnemyContext pContext)
        {
            base.Initialize(pContext);
            _minRange = pContext.Config.MinRange;
            _maxRange = pContext.Config.MaxRange;
            _speed = pContext.Speed;
        }

        protected override void InitState(EventState<EStates> pState)
        {
            base.InitState(pState);
            _origin = transform.position;
            _target = _origin;
            _lerpTime = 1;
            _lerpDuration = 1;
        }

        protected override void RoamState(EventState<EStates> pState)
        {
            base.RoamState(pState);

            _lerpTime += Time.deltaTime;
            Vector2.Lerp(_origin, _target, _lerpTime / _lerpDuration);
            if (_lerpTime >= _lerpDuration)
                return;

            _origin = transform.position;
            _target = FieldMin + new Vector2(Random.Range(_minRange.x, _maxRange.x) * FieldSize.x, Random.Range(_minRange.y, _maxRange.y) * FieldSize.y);
            _lerpTime = 0;
            _lerpDuration = Vector2.Distance(_origin, _target) / _speed;
        }
    }
}
