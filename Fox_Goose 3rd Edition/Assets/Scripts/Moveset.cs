using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Moveset : MonoBehaviour {

    public List<Move> moves;

    private Moveset()
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

    public void StoreMove(string playerName, Coordinate startPos, Coordinate finishPos, MoveState moveType)
    {
        Move currentMove = new Move(playerName, startPos, finishPos, moveType);
        moves.Add(currentMove);
    }

    public void NewGame()
    {
        moves.Clear();
    }

    public class Move
    {
        public string PlayerName { get; set; }
        public Coordinate StartPos { get; set; }
        public Coordinate FinishPos { get; set; }
        public MoveState MoveType { get; set; }

        public Move(string playerName, Coordinate startPos, Coordinate finishPos, MoveState moveType)
        {
            PlayerName = playerName;
            StartPos = startPos;
            FinishPos = finishPos;
            MoveType = moveType;

        }

    }
}
