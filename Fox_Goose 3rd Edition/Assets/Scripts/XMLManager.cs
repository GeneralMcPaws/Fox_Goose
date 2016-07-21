using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class XMLManager : MonoBehaviour {

    public static XMLManager instance;

    void Awake()
    {
        instance = this;
    }
    
    public Moveset moveSet;

    //save function
    public void SaveItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Moveset));
        FileStream fstream = new FileStream(Application.dataPath + "/XML/move_data.xml",FileMode.Create);
        serializer.Serialize(fstream, moveSet);
        fstream.Close();
    }

    public void LoadItems()
    {

        //check if file exists
        XmlSerializer serializer = new XmlSerializer(typeof(Moveset));
        FileStream fstream = new FileStream(Application.dataPath + "/XML/move_data.xml", FileMode.Open);
        moveSet = serializer.Deserialize(fstream) as Moveset;

    }
}
