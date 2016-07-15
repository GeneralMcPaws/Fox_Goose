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
    
    public Moveset.MovesDB movesetDatabase;

    //save function
    public void SaveItems()
    {
        //open a new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(Moveset.MovesDB));
        FileStream fstream = new FileStream(Application.dataPath + "/XML/move_data.xml",FileMode.Create);
        serializer.Serialize(fstream, movesetDatabase);
        fstream.Close();
    }
}
