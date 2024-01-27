using JvLib.Events;
using UnityEngine;

namespace Project.Gameplay.Enemy
{
    public class PathEnemyBehaviour : AEnemyBehaviour<PathEnemyConfig, PathEnemyContext>
    {
        private AnimationCurve _xCurve;
        private AnimationCurve _yCurve;

        protected override void Initialize(PathEnemyContext pConfig)
        {
            base.Initialize(pConfig);

            _xCurve = pConfig.Config.XMovement;
            _yCurve = pConfig.Config.YMovement;
        }

        protected override void RoamState(EventState<EStates> pState)
        {
            base.RoamState(pState);
            
            float x = FieldMin.x + FieldSize.x * _xCurve.Evaluate01(CurrentTime / Duration);
            float y = FieldMin.y + FieldSize.y * _yCurve.Evaluate01(CurrentTime / Duration);
            
            transform.position = new Vector3(x, y, 0);
        }
    }
}
