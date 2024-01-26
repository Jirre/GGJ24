using JvLib.Data;

namespace Project.Gameplay
{
    public abstract class AEnemyConfig : DataEntry
    {
    }

    public abstract class AEnemyContext<TConfig>
        where TConfig : AEnemyConfig
    {
        public TConfig Config { get; private set; }
        public float Depth;
        public float Duration;
    }
}
