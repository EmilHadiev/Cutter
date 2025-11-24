using System;
using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        #region Coins
        public int coins = 0;
        #endregion

        #region PlayerData
        public AssetProvider.Swords CurrentSword;
        public AssetProvider.Particles CurrentParticle;
        #endregion

        #region SwordAndParticles
        public List<SaveItem> Swords = new List<SaveItem>();
        public List<SaveItem> Particles = new List<SaveItem>();

        #endregion

        #region PlayerProgress
        public int completedLevels = 0;
        public int completedHardcoreLevels = 0;
        public bool isHardcoreOpen = false;
        public int HardcoreHealth = 1;
        #endregion
    }
}

[Serializable]
public class SaveItem
{
    public int Id;
    public bool IsPurchased;
}