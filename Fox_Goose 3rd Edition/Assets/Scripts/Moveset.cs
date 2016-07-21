using System.Xml.Serialization;
using System.Collections.Generic;

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

    public void Save()
    {
        XMLManager.instance.moveSet = this;
        XMLManager.instance.SaveItems();
    }

    public void Reset()
    {
        moves.Clear();
    }

}
