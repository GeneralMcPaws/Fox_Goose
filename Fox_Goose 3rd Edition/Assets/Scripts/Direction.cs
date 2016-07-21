using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Direction : MonoBehaviour {

    private int x;
    private int y;

    public Direction()
    {

    }
    public Direction(int x, int y)
    {
        X = x;
        Y = y;
    }
    [XmlAttribute]
    public int X
    {
        get;
        set;
    }
    [XmlAttribute]
    public int Y
    {
        get;
        set;
    }
}
