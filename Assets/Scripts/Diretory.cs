using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

public class Diretory : MonoBehaviour
{
    private string _GroupData;
    private string _Pest;
    private string _JsonFile;
    private List<Names> NamesInGroup = new List<Names>
    {
        new Names("Alexander", 23,"Purple"),
        new Names("Oscar", 28,"Green"),
        new Names("Oscar D", 22,"Black"),
        new Names("Gilah", 19,"Blue"),
        new Names("Marcus", 20,"Teal"),
        new Names("Tristan", 23,"Dark Green"),
        new Names("Mark", 20,"Blue"),
    };

    void Awake()
    {
        _GroupData = Application.persistentDataPath + "/Group_Data/";
        _Pest = _GroupData + "Group_Information.xml";
        _JsonFile = _GroupData + "Group_Information.json";
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_GroupData);
        NewDirectory();
        SerializeXML();
        DeserializeXML();
    }

    public void NewDirectory()
    {
        if (Directory.Exists(_GroupData))
        {
            Debug.Log("Directory already exists.");
            return;
        }

        Directory.CreateDirectory(_GroupData);
        Debug.Log("New directory created!");
    }

    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Names>));

        using (FileStream stream = File.Create(_Pest))
        {
            xmlSerializer.Serialize(stream, NamesInGroup);
        }

        Debug.Log("XML file created successfully!");
    }

    public void DeserializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Names>));
        List<Names> deserializedNames;

        using (FileStream stream = File.OpenRead(_Pest))
        {
            deserializedNames = (List<Names>)xmlSerializer.Deserialize(stream);
        }

        Debug.Log("XML file read successfully!");

        var json = JsonUtility.ToJson(new Wrapper<Names> { Items = deserializedNames }, true);
        File.WriteAllText(_JsonFile, json);

        Debug.Log("JSON file created successfully!");
    }

    [Serializable]
    public class Wrapper<T>
    {
        public List<T> Items;
    }


    public class Names
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }

        public Names() { } // Required for XML serialization

        public Names(string name, int age, string color)
        {
            Name = name;
            Age = age;
            Color = color;
        }

    }
}




