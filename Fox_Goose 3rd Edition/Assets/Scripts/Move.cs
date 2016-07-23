using UnityEngine;
using System.Collections;


[System.Serializable]
public class Move
{
    public string PlayerName { get; set; }
    public Coordinate StartPos { get; set; }
    public Coordinate FinishPos { get; set; }
    public MoveState MoveType { get; set; }

    public Move() { }

    public Move(string playerName, Coordinate startPos, Coordinate finishPos, MoveState moveType)
    {
        PlayerName = playerName;
        StartPos = startPos;
        FinishPos = finishPos;
        MoveType = moveType;

    }
}
