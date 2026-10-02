using System;
using System.Collections.Generic;

class Cache<TKey, TValue> where TKey : notnull
{
    private class CacheItem
    {
        public TValue Value { get; }
        public DateTime? ExpiresAt { get; }

        public CacheItem(TValue value, DateTime? expiresAt)
        {
            Value = value;
            ExpiresAt = expiresAt;
        }

        public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow >= ExpiresAt.Value;
    }

    private readonly Dictionary<TKey, CacheItem> _items = new Dictionary<TKey, CacheItem>();
    private readonly TimeSpan? _defaultTtl;
    private readonly object _lock = new object();

    public Cache(TimeSpan? defaultTtl = null)
    {
        _defaultTtl = defaultTtl;
    }

    public void Add(TKey key, TValue value, TimeSpan? ttl = null)
    {
        TimeSpan? lifetime = ttl ?? _defaultTtl;
        DateTime? expiresAt = lifetime.HasValue ? DateTime.UtcNow + lifetime.Value : (DateTime?)null;

        lock (_lock)
        {
            _items[key] = new CacheItem(value, expiresAt);
        }
    }

    public bool TryGet(TKey key, out TValue value)
    {
        lock (_lock)
        {
            if (_items.TryGetValue(key, out CacheItem item))
            {
                if (!item.IsExpired)
                {
                    value = item.Value;
                    return true;
                }
                _items.Remove(key);          
            }
        }

        value = default;
        return false;
    }

    public TValue Get(TKey key)
    {
        if (TryGet(key, out TValue value))
            return value;

        throw new KeyNotFoundException($"Key '{key}' was not found or has expired.");
    }

    public bool Contains(TKey key) => TryGet(key, out _);

    public bool Remove(TKey key)
    {
        lock (_lock)
        {
            return _items.Remove(key);
        }
    }

    public int RemoveExpired()
    {
        lock (_lock)
        {
            var expiredKeys = new List<TKey>();
            foreach (var pair in _items)
                if (pair.Value.IsExpired)
                    expiredKeys.Add(pair.Key);

            foreach (TKey key in expiredKeys)
                _items.Remove(key);

            return expiredKeys.Count;
        }
    }

    public int Count
    {
        get
        {
            RemoveExpired();
            lock (_lock) { return _items.Count; }
        }
    }

    public void Clear()
    {
        lock (_lock) { _items.Clear(); }
    }
}