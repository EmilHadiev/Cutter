using UnityEngine;

public static class AssetProvider
{
    #region Particles
    public const string DefenseParticle = "DefenseParticle";
    public const string ParryParticle = "ParryParticle";
    public const string StunParticle = "StunParticle";
    public const string ParryWindowParticle = "ParryWindowParticle";

    /// <summary>
    /// players
    /// </summary>

    public enum Particles
    {
        None,
        FireParticle,
        ElectricParticle,
        HeartAndStarParticles,
        LightParticle,
        SoulsParticle,
        IceParticle,
        DarkParticle
    }
    #endregion

    #region Label
    public const string EnemyLabel = "Enemy";
    #endregion

    #region Enemies
    public const string OrcPrefab = "Enemies/Orc.prefab";
    public const string SkeletonPrefab = "Enemies/Skeleton.prefab";
    public const string BlackKnightPrefab = "Enemies/BlackKnight.prefab";
    public const string DemonKing = "Enemies/DemonKing.prefab";
    public const string Lizard = "Enemies/Lizard.prefab";
    #endregion

    #region SO
    public const string EnemiesDataFolder = "EnemiesData";
    public const string SkeletonData = "EnemiesData/Skeleton.asset";
    #endregion

    #region Swords
    public enum Swords
    {
        SkeletonSword,
        SpecterBlade,
        RatDagger,
        NagaSpear,
        KhopeshBlade,
        Hammer,
        DemonBlade,
        Axe
    }
    #endregion

    #region Scenes
    public const string ViewSelectorScene = "ViewSelector";
    #endregion
}