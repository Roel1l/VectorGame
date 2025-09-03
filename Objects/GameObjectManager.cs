using System;
using System.Collections.Generic;
using System.Linq;

namespace VectorGame.Objects;

public class GameObjectManager
{
    private int _nextId = 0;
    private readonly Dictionary<int, GameObject> _gameObjectsById = [];
    private readonly Dictionary<Type, List<GameObject>> _objectsByType = [];

    public void Add(GameObject gameObject)
    {
        gameObject.Id = _nextId++;
        _gameObjectsById[gameObject.Id] = gameObject;

        var type = gameObject.GetType();

        if (!_objectsByType.TryGetValue(type, out var value))
        {
            value = [];
            _objectsByType[type] = value;
        }

        value.Add(gameObject);
    }

    public void Remove(GameObject gameObject)
    {
        if (_gameObjectsById.ContainsKey(gameObject.Id))
        {
            _gameObjectsById.Remove(gameObject.Id);
            _objectsByType[gameObject.GetType()].Remove(gameObject);
        }
    }

    public GameObject? GetById(int id)
    {
        _gameObjectsById.TryGetValue(id, out var gameObject);
        return gameObject;
    }

    public IEnumerable<T> GetAll<T>() where T : GameObject
    {
        var type = typeof(T);

        if (_objectsByType.TryGetValue(type, out var value))
        {
            return value.Cast<T>();
        }

        return [];
    }

    public IEnumerable<GameObject> GetAll()
    {
        return _gameObjectsById.Values;
    }
}
