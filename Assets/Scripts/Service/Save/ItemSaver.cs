using System;
using System.Collections.Generic;
using System.Linq;

public class ItemSaver
{
    private readonly List<SaveItem> _saveItems;
    private readonly IPurchasable[] _items;

    public ItemSaver(List<SaveItem> saveItems, IEnumerable<IPurchasable> items)
    {
        _saveItems = saveItems;
        _items = items.ToArray();
    }

    public void Save()
    {
        for (int i = 0; i < _items.Length; i++)
            _saveItems[i].IsPurchased = _items[i].IsPurchased;
    }

    public void Load()
    {
        if (_saveItems.Count == 0)
            FirstInit();
        else if (_saveItems.Count == _items.Length)
            DefaultInit();
        else
            throw new ArgumentOutOfRangeException($"wrong size between {nameof(_saveItems)} and {nameof(_items)}");
    }

    private void FirstInit()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            var item = new SaveItem
            {
                Id = _items[i].Id,
                IsPurchased = _items[i].IsPurchased,
            };

            _saveItems.Add(item);
        }
    }

    private void DefaultInit()
    {
        var items = _saveItems.ToDictionary(key => key.Id, value => value.IsPurchased);

        for (int i = 0; i < _items.Length; i++)
            if (items.TryGetValue(_items[i].Id, out bool isPurchased))
                _items[i].IsPurchased = isPurchased;
    }
}