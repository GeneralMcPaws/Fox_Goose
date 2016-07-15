using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
public class Moveset : MonoBehaviour{

    public MovesDB moves = new MovesDB();
    

    public Move Undo()
    {
        if (moves.list.Count == 0)
            return null;
        Move lastMove = moves.list[moves.list.Count - 1];
        moves.list.RemoveAt(moves.list.Count - 1);
        return lastMove;
    }

    public void StoreMove(string playerName, Coordinate startPos, Coordinate finishPos, MoveState moveType)
    {
        Move currentMove = new Move(playerName, startPos, finishPos, moveType);
        moves.list.Add(currentMove);
    }

    public void Save()
    {
        XMLManager.instance.movesetDatabase = moves;
        XMLManager.instance.SaveItems();
    }

    public void NewGame()
    {
        moves.list.Clear();
    }

    [System.Serializable]
    public class Move
    {
        public string PlayerName { get; set; }
        public Coordinate StartPos { get; set; }
        public Coordinate FinishPos { get; set; }
        public MoveState MoveType { get; set; }

        public Move()
        {
            
        }
        public Move(string playerName, Coordinate startPos, Coordinate finishPos, MoveState moveType)
        {
            PlayerName = playerName;
            StartPos = startPos;
            FinishPos = finishPos;
            MoveType = moveType;

        }

    }
    [System.Serializable]
    public class MovesDB
    {
        public List<Move> list = new List<Move>();
    }
}
