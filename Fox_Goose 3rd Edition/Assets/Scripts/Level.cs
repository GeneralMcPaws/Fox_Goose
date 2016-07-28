using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("Level")]
public class Level
{
    [XmlAttribute("LevelName")]
    public string levelName;

    [XmlAttribute("boardWidth")]
    public int boardWidth;

    [XmlAttribute("boardHeight")]
    public int boardHeight;

    [XmlArrayAttribute("Items")]
    public List<Item> items = new List<Item>();

    public Level() { }

    public void Save(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Level));
        FileStream fstream = new FileStream(path, FileMode.Create);
        serializer.Serialize(fstream, this);
        fstream.Close();

    }

    public void Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Level));
        FileStream fstream = new FileStream(path, FileMode.Open);
        items = ((Level)serializer.Deserialize(fstream)).items;
    }


}

[System.Serializable]
public struct Item
{
    [XmlAttribute]
    public string PrefabName { get; set; }

    [XmlAttribute]
    public int XMin { get; set; }

    [XmlAttribute]
    public int XMax { get; set; }

    [XmlAttribute]
    public int YMin { get; set; }

    [XmlAttribute]
    public int YMax { get; set; }

    public Item(string prefabName, int xmin, int xmax, int ymin, int ymax)
    {
        PrefabName = prefabName;
        XMin = xmin;
        XMax = xmax;
        YMin = ymin;
        YMax = ymax;
    }
}
