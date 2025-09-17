using System;

public class AssetSwordProperty
{
    public event Action<AssetProvider.Swords> Changed;

    private AssetProvider.Swords _value;

    public AssetSwordProperty(AssetProvider.Swords value)
    {
        _value = value;
    }

    public AssetProvider.Swords Value
    {
        get => _value;
        set
        {
            AssetProvider.Swords oldValue = _value;
            _value = value;

            if (_value.CompareTo(oldValue) != 0)
                Changed?.Invoke(_value);
        }
    }
}