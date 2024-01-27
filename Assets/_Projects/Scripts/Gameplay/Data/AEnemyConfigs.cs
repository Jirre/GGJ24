using JvLib.Data;
using UnityEngine;

namespace Project.Gameplay
{
    [CreateAssetMenu(fileName = "EnemyConfigs", menuName = "Project/Data/Enemies", order = 170)]
    public class AEnemyConfigs : DataList<AEnemyConfig>
    {
    }
}
