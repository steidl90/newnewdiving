using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SerializeDictionary<K,V>:Dictionary<K,V>,ISerializationCallbackReceiver
{
    [SerializeField]
    List<K> keys = new List<K>();
    [SerializeField]
    List<V> values = new List<V>(0);
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();
        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }

}
