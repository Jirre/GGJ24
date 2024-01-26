using UnityEngine;

namespace Project.Gameplay
{
    public class RandomEnemyConfig : AEnemyConfig
    {
        [SerializeField] private Vector2 _MinRange;
        public Vector2 MinRange => _MinRange;
        [SerializeField] private Vector2 _MaxRange;
        public Vector2 MaxRange => _MaxRange;
    }

    public class RandomEnemyContext : AEnemyContext<RandomEnemyConfig>
    {
        public float Speed;
    }
}
