using JvLib.Pooling.Data.Objects;
using UnityEngine;

namespace Project.Gameplay
{
    public class AmmunitionConfig : PooledObjectConfig
    {
        [SerializeField] private Sprite _UIImage;
        public Sprite UIImage => _UIImage;
    }
}
