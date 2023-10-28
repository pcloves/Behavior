using System.Collections.Generic;
using Godot;

namespace BehaviorAI;

public partial class Blackboard : Node
{
    private readonly Dictionary<string, object> _data = new();

    public bool SetData<T>(string key, T value, bool overwrite = true)
    {
        if (value == null)
        {
            return false;
        }

        if (_data.ContainsKey(key) && !overwrite)
        {
            return false;
        }

        _data.Add(key, value);

        return true;
    }

    public bool GetData<T>(string key, out T value)
    {
        _data.TryGetValue(key, out var valueReal);

        value = valueReal is T v ? v : default;

        return value != null;
    }

    public bool RemoveData(string key)
    {
        return _data.Remove(key);
    }

    public void ClearData()
    {
        _data.Clear();
    }
}