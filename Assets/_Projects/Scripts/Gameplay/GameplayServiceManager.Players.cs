using System;
using System.Collections.Generic;
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
            if (_playerData?[pIndex] == null)
            {
                _playerData = new PlayerData[MAX_PLAYER_COUNT];
                List<AmmunitionConfig> ammunition = new List<AmmunitionConfig>();
                foreach (PooledObjectConfig config in PooledObjectConfigs.Entries)
                {
                    if (config is AmmunitionConfig aConfig)
                        ammunition.Add(aConfig);
                }
                if (ammunition.Count <= 0)
                    throw new NullReferenceException("No Ammunition Configs available");
                for (int i = 0; i < MAX_PLAYER_COUNT; i++)
                {
                    _playerData[i] = new PlayerData()
                    {
                        Ammunition = ammunition[Random.Range(0, ammunition.Count)],
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
