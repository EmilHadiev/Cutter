using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.MapContainer, fileName = "MapsContainer")]
public class MapsContainer : ScriptableObject
{
    [SerializeField] private MapsColorContainer _colorsContainer;
    [SerializeField] private Map[] _maps;
    [SerializeField] private Map _startLevel;
    [SerializeField] private Map _bonus;
    [SerializeField] private Map[] _bosses;
    [SerializeField] private Map[] _blackKnights;

    private const int BossLevel = 9;
    private const int BlackKnightLevel = 5;
    private const int BonusLevel = 0;
    private const int StartLevel = 0;

    public Map GetMap(int level)
    {
        if (IsStartLevel(level))
            return _startLevel;

        if (IsBossLevel(level))
            return GetRandomMap(_bosses);

        if (IsBonusLevel(level))
            return _bonus;

        if (IsBlackKnightLevel(level))
            return GetRandomMap(_blackKnights);

        return GetDefaultMap(level);
    }

    public MapColor GetMapColor(int level) => 
        _colorsContainer.GetColor(level);

    private Map GetRandomMap(Map[] maps)
    {
        int index = Random.Range(0, maps.Length);
        return maps[index];
    }

    private Map GetDefaultMap(int level) => 
        _maps[GetLevel(level)];

    private int GetLevel(int level)
    {
        int studLevelCount = 1;
        int max = _maps.Length - studLevelCount;

        return level % max;
    }

    private bool IsStartLevel(int level) => level == StartLevel;
    private bool IsBossLevel(int level) => level % 10 == BossLevel;
    private bool IsBlackKnightLevel(int level) => level % 10 == BlackKnightLevel;
    private bool IsBonusLevel(int level) => level % 10 == BonusLevel;
}