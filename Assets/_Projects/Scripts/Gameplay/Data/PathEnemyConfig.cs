using UnityEngine;

namespace Project.Gameplay
{
    public class PathEnemyConfig : AEnemyConfig
    {
        [SerializeField] private AnimationCurve _XMovement;
        public AnimationCurve XMovement => _XMovement;
        [SerializeField] private AnimationCurve _YMovement;
        public AnimationCurve YMovement => _YMovement;
    }

    public class PathEnemyContext : AEnemyContext<PathEnemyConfig>
    {
    }
}
