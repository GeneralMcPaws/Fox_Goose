﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public enum CellState
{
	OCCUPIED,
	EMPTY,
	FOX,
	GOOSE,
	VISITED
}


public class GameManager : MonoBehaviour {

    public Text pointsText;
    public Text whoseTurnIsIt;
    public Text notificationText;
    
    public GameObject fox;

    private int foxPoints = 0;
    public int pointsToWin;

	public bool isFoxPlaying = true;

    private BoardManager boardScript;

	public static GameManager instance = null;
    private Moveset moveset;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		
	}

	void Start () {

        boardScript = GetComponent<BoardManager>();
        boardScript.Fox = fox;
        boardScript.SetupScene();

        moveset = FindObjectOfType(typeof(Moveset)) as Moveset;
        
        UI_WhoPlays(false);
	}

	public void playTurn(float xPos, float yPos, CellState cellState)
	{

        if (isFoxPlaying)
            FoxTurn((int)xPos, (int)yPos);
        
        else
            GooseTurn(cellState, (int)xPos, (int)yPos);
        
        if (hasGameEnded())
        {
            UI_WhoPlays(true);
            EndGame();
        }

        UI_WhoPlays(false);
	}

	void Update () {
	}

    private void FoxTurn(int xFoxPos,int yFoxPos)
    {
        MoveState moveState = boardScript.IsMoveAllowed(xFoxPos, yFoxPos);
        var xCurrentPos = (int)boardScript.Fox.transform.position.x;
        var yCurrentPos = (int)boardScript.Fox.transform.position.y;

        switch (moveState)
        {
            case MoveState.NOT_ALLOWED:
            case MoveState.OBLIGATORY:
                NotificationTextUpdate(moveState);
                break;
            case MoveState.NORMAL:
                NotificationTextUpdate(moveState);
                moveset.StoreMove("FOX", new Coordinate(xCurrentPos, yCurrentPos), new Coordinate(xFoxPos, yFoxPos), MoveState.NORMAL);
                MoveFox(xFoxPos, yFoxPos);
                UpdateScore(moveState);
                break;
            case MoveState.JUMP:
                NotificationTextUpdate(moveState);
                moveset.StoreMove("FOX", new Coordinate(xCurrentPos, yCurrentPos), new Coordinate(xFoxPos, yFoxPos), MoveState.JUMP);
                JumpFox(xFoxPos, yFoxPos);
                UpdateScore(moveState);
                break;
        }


    }

    private void GooseTurn(CellState cellState,  int xPos,  int yPos)
    {
        NotificationTextUpdate(MoveState.NORMAL,cellState);
        if (cellState == CellState.EMPTY)
        {
            moveset.StoreMove("Goose", null, new Coordinate(xPos, yPos), MoveState.NORMAL);
            MoveGoose(xPos, yPos);
        }
    }


    private void MoveFox(int xFoxPos,int yFoxPos)
    {
        boardScript.UpdateBoard(xFoxPos, yFoxPos,true);

        isFoxPlaying = !isFoxPlaying;
    }

    private void JumpFox(int xFoxPos,int yFoxPos)
    {

        boardScript.UpdateBoard(xFoxPos, yFoxPos,true);

        if (!boardScript.FoxCanJump)
        {
            isFoxPlaying = !isFoxPlaying;
        }

    }

    private void MoveGoose(int xPos,  int yPos)
    {
        boardScript.UpdateBoard(xPos, yPos,false);
        isFoxPlaying = !isFoxPlaying;
    }

    private void UpdateScore(MoveState moveState)
    {
        if (moveState == MoveState.NORMAL)
            foxPoints += 1;
        else
            foxPoints += 2;
        pointsText.text = "Fox Points = " + foxPoints;
    }

    bool hasGameEnded()
    {
        if(foxPoints >= pointsToWin || !boardScript.HasFoxMovesLeft((int)boardScript.Fox.transform.position.x,(int)boardScript.Fox.transform.position.y))
            return true; 
        return false;
    }


    void EndGame()
    {
        notificationText.text = "GAME ENDED";
        Debug.Log("GAME HAS ENDED");
        UnityEditor.EditorApplication.isPaused = true;
    }

    #region Text Updates

    private void NotificationTextUpdate(MoveState movestate, CellState cellState = CellState.OCCUPIED)
    {
        if (isFoxPlaying)
        {
            switch (movestate)
            {
                case MoveState.NOT_ALLOWED:
                    notificationText.text = "This move is not allowed!\n Try again.";
                    break;
                case MoveState.NORMAL:
                    notificationText.text = "";
                    break;
                case MoveState.OBLIGATORY:
                    notificationText.text = "There are obligatory moves.\nYou need to take those!";
                    break;
                case MoveState.JUMP:
                    notificationText.text = "Hop Hop Hop!";
                    break;
                default:
                    break;
            }
        }
        else
            switch (cellState)
            {
                case CellState.EMPTY:
                    notificationText.text = "";
                    break;
                case CellState.FOX:
                    notificationText.text = "Yeah that's right, this is your target!\n Dolby Surround him!";
                    break;
                case CellState.GOOSE:
                    notificationText.text = "Don't just click on yourself, go catch that fox!";
                    break;
                case CellState.OCCUPIED:
                case CellState.VISITED:
                    notificationText.text = "It appears that something \nis blocking your way...";
                    break;
                default:
                    break;
            }
    }

   
    private void UI_WhoPlays(bool endGame)
    {
        if (endGame)
        {
            if (foxPoints >= 30)
                whoseTurnIsIt.text = "GameOver! Fox Won!";
            else
                whoseTurnIsIt.text = "GameOver! Goose Won!";
        }
        if (isFoxPlaying)
            whoseTurnIsIt.text = "It's your turn Fox!";
        else
            whoseTurnIsIt.text = "It's your turn Goose!";
    }
    #endregion

    #region Hints

    internal void EnableHint()
    {
        if (!isFoxPlaying)
        {
            notificationText.text = "Goose can move everywhere.";
            return;
        }
        boardScript.ShowHints();
    }
    #endregion

}
