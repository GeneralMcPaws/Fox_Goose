using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("GameMoves")]
public class Moveset
{

    [XmlArrayAttribute("Moves")]
    public List<Move> moves = null;

    public Moveset()
    {
        moves = new List<Move>();
    }

    public Move Undo()
    {
        if (moves.Count == 0)
            return null;
        Move lastMove = moves[moves.Count - 1];
        moves.RemoveAt(moves.Count - 1);
        return lastMove;
    }

    public void Add(string playerName, Coordinate startPos, Coordinate finishPos, MoveState moveType)
    {
        Move currentMove = new Move(playerName, startPos, finishPos, moveType);
        moves.Add(currentMove);
    }

    public void Save(string dataPath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Moveset));
        FileStream fstream = new FileStream(dataPath, FileMode.Create);
        serializer.Serialize(fstream, this);
        fstream.Close();
    }

    public void Load(string dataPath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Moveset));
        FileStream fstream = new FileStream(dataPath, FileMode.Open);
        moves = ((Moveset)serializer.Deserialize(fstream)).moves;
    }

    public void Reset()
    {
        moves.Clear();
    }

}
