using System;
using System.Linq;
using JvLib.Pooling.Data.Objects;
using Random = UnityEngine.Random;

namespace Project.Gameplay
{
    public partial class GameplayServiceManager // Players
    {
        private PlayerData[] _playerData;
        private const int MAX_PLAYER_COUNT = 2;

        public PlayerData GetPlayerData(int pIndex)
        {
            if (_playerData == null || _playerData[pIndex] == null)
            {
                _playerData = new PlayerData[MAX_PLAYER_COUNT];
                AmmunitionConfig[] ammunition = PooledObjectConfigs.Entries.Where(e => e is AmmunitionConfig).ToArray() as AmmunitionConfig[];
                if (ammunition == null)
                    throw new NullReferenceException("No Ammunition Configs available");
                for (int i = 0; i < MAX_PLAYER_COUNT; i++)
                {
                    _playerData[i] = new PlayerData()
                    {
                        Ammunition = ammunition[Random.Range(0, ammunition.Length)],
                        Score = 0
                    };
                }
            }
            
            if (pIndex >= _playerData.Length)
                throw new IndexOutOfRangeException();

            return _playerData[pIndex];
        }

        public void ResetPlayerData()
        {
            _playerData ??= new PlayerData[MAX_PLAYER_COUNT];
            for (int i = 0; i < MAX_PLAYER_COUNT; i++)
            {
                _playerData[i].Reset();
            }
        }
    }

    public class PlayerData
    {
        public AmmunitionConfig Ammunition;
        public int Score;

        public void Reset()
        {
            Score = 0;
        }
    }
}
