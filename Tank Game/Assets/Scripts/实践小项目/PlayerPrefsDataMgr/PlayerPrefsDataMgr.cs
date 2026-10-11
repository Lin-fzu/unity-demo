using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPrefsDataMgr 
{
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();

    public static PlayerPrefsDataMgr Instance
    {
        get
        {
            return instance;
        }
    }

    private PlayerPrefsDataMgr()
    {

    }

    public void SaveData(object data,string keyName)
    {
        Type dataType = data.GetType();
        FieldInfo[] infos = dataType.GetFields();
        string saveKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++)
        {
            info = infos[i];
            saveKeyName = keyName + "_" + dataType.Name +
                "_" + info.FieldType.Name + "_" + info.Name;
            SaveValue(info.GetValue(data),saveKeyName);
        }
        PlayerPrefs.Save();
    }
    private void SaveValue(object value,string keyName)
    {
        Type typeField = value.GetType();
        if (typeField == typeof(int))
        {
            int rValue = (int)value;
            rValue += 10;
            PlayerPrefs.SetInt(keyName, rValue);
        }
        else if (typeField == typeof(float))
        {
            PlayerPrefs.SetFloat(keyName, (float)value);
        }
        else if (typeField == typeof(string))
        {
            PlayerPrefs.SetString(keyName, value.ToString());
        }
        else if (typeField == typeof(bool))
        {
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }
        else if (typeof(IList).IsAssignableFrom(typeField))
        {
            IList list = value as IList;
            PlayerPrefs.SetInt(keyName, list.Count);
            int index = 0;
            foreach (object obj in list)
            {
                SaveValue(obj, keyName + index);
                ++index;
            }
        }
        else if (typeof(IDictionary).IsAssignableFrom(typeField))
        {
            IDictionary dic = value as IDictionary;
            PlayerPrefs.SetInt(keyName, dic.Count);
            int index = 0;
            foreach (object key in dic.Keys)
            {
                SaveValue(key, keyName + "_key_" + index);
                SaveValue(dic[key], keyName + "_value_" + index);
                ++index;
            }
        }
        else
        {
            SaveData(value, keyName);
        }
    }
    public object LoadData(Type type,string keyName)
    {
        object data = Activator.CreateInstance(type);
        FieldInfo[] infos = type.GetFields();
        string loadKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++)
        {
            info = infos[i];
            loadKeyName= keyName + "_" + type.Name +
                "_" + info.FieldType.Name + "_" + info.Name;
            info.SetValue(data, LoadValue(info.FieldType, loadKeyName));
        }
        return data;
    }
    private object LoadValue(Type fieldType,string keyName)
    {
        if (fieldType == typeof(int))
        {
            return PlayerPrefs.GetInt(keyName, 0) - 10;
        }
        else if (fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(keyName, 0);
        }
        else if (fieldType == typeof(string))
        {
            return PlayerPrefs.GetString(keyName, "");
        }
        else if (fieldType == typeof(bool))
        {
            return PlayerPrefs.GetInt(keyName, 0) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            int count = PlayerPrefs.GetInt(keyName,0);
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++)
            {
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + i));
            }
            return list;
        }
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            int count = PlayerPrefs.GetInt(keyName, 0);
            IDictionary dic = Activator.CreateInstance(fieldType) as IDictionary;
            Type[] kvType = fieldType.GetGenericArguments();
            for (int i = 0; i < count; i++)
            {
                dic.Add(LoadValue(kvType[0], keyName + "_key_" + i), LoadValue(kvType[1], keyName + "_value_" + i));
            }
            return dic;
        }
        else
        {
            return LoadData(fieldType,keyName);
        }
    }

}
