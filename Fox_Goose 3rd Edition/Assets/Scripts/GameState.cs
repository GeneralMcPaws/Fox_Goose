﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


[XmlRoot("GameState")]
public class GameState  {

    [XmlArrayAttribute("Cells")]
    public List<CellState> cells = new List<CellState>();

    [XmlAttribute("Rows")]
    public int Rows { get; set; }

    [XmlAttribute("Collumns")]
    public int Collumns { get; set; }

    [XmlAttribute("FoxTurn")]
    public bool FoxTurn { get; set; }

    [XmlAttribute("Points")]
    public int Points { get; set; }

    public GameState() { }
    
    public GameState(Cell[,] grid, int rows, int collumns, bool foxTurn, int points)
    {
        foreach(var cell in grid)
            cells.Add(cell.cellState);
        Rows = rows;
        Collumns = collumns;
        FoxTurn = FoxTurn;
        Points = points;

    }

    public void Save(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameState));
        FileStream fstream = new FileStream(path, FileMode.Create);
        serializer.Serialize(fstream, this);
        fstream.Close();
    }

    public GameState Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameState));
        FileStream fstream = new FileStream(path, FileMode.Open);
        return serializer.Deserialize(fstream) as GameState;
    }
}