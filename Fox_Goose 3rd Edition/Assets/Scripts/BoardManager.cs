using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public enum MoveState
{
    NORMAL,
    JUMP,
    NOT_ALLOWED,
    OBLIGATORY
}

public enum LoadChoice
{
    XML_LOCAL,
    DEFAULT
}



public class BoardManager : MonoBehaviour {

    public LoadChoice loader = LoadChoice.DEFAULT;
    private string dataPath = @"C:\Users\Digi\Documents\UnityProjects\foxgoose\Fox_Goose 3rd Edition\Assets\XML\level1.xml";
    private string levelName;

	private Cell[,] grid;    
    private Transform boardHolder;
    public int collumns;
    public int rows;

    public GameObject occupied;
    public GameObject goose;
    public GameObject groundTile;
    private GameObject fox;
    public GameObject hint;

    private IList<Coordinate> jumpMoves;
    private List<Coordinate> adjacentCoordinates = new List<Coordinate>();

    #region Scene Initialisation

    public void SetupScene()
	{ 
        switch(loader)
        {
            case LoadChoice.DEFAULT:
                InitialiseBoard();
                CenterCamera();
                InitialiseFox();
                break;
            case LoadChoice.XML_LOCAL:
                Level level = LoadLevel(dataPath);
                ConstructLevel(level);
                CenterCamera();
                InitialiseFox();
                break;
            default:
                break;

        }
	}

    Level LoadLevel(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Level));
        FileStream fstream = new FileStream(path, FileMode.Open);
        return serializer.Deserialize(fstream) as Level;
    }

    void ConstructLevel(Level level)
    {
        boardHolder = new GameObject("Board").transform;
        rows = level.boardHeight;
        collumns = level.boardWidth;
        grid = new Cell[collumns, rows];

        GameObject prefab;

        foreach (var item in level.items)
        { 
            for (int x = item.XMin; x <= item.XMax; x++)
            {
                for (int y = item.YMin; y <= item.YMax; y++)
                {
                    switch(item.PrefabName)
                    {
                        case "Fox":
                            prefab = fox;
                            break;
                        case "Goose":
                            prefab = goose;
                            break;
                        case "Poo":
                            prefab = occupied;
                            break;
                        case "GroundTile":
                        default:
                            prefab = groundTile;
                            break;
                    }
                    GameObject instance = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    if(item.PrefabName == "GroundTile")
                        grid[x, y] = instance.GetComponent<Cell>();
                }
            }
        }
    }

	void InitialiseBoard()
	{
        boardHolder = new GameObject("Board").transform;

		grid = new Cell[collumns, rows];
		for (int x = 0; x < collumns; x++) 
		{
            for (int y = 0; y < rows; y++)
            {
                GameObject instance = Instantiate(groundTile, new Vector3(x, y), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                grid[x, y] = instance.GetComponent<Cell>();
            }
		}
	}

	void CenterCamera()
	{
		float zPos = Camera.main.transform.position.z;


		if (((collumns % 2 == 0))) {
            Camera.main.orthographicSize = (collumns + rows) / 4;

            Camera.main.transform.position = new Vector3 (collumns / 2-0.5f, (rows / 2) - .5f, zPos);

		}
		else {
            Camera.main.orthographicSize = ((collumns + rows) / 4)+0.5f;

            Camera.main.transform.position = new Vector3 ((collumns / 2), ((rows / 2) - .5f)+0.5f, zPos);
		}
	}

	void InitialiseFox()
	{
        //if (((boardWidth % 2 == 0)))
        //{

        //    fox = Instantiate(fox, new Vector3(boardWidth / 2 - UnityEngine.Random.Range(0, 2), boardHeight / 2 - UnityEngine.Random.Range(0, 2), -1f), Quaternion.identity) as GameObject;
        //}
        //else
        //{

        //    fox = Instantiate(fox, new Vector3(boardWidth / 2, boardHeight / 2, -1f), Quaternion.identity) as GameObject;
        //}


        fox = Instantiate(fox, new Vector3(-100f,-100f), Quaternion.identity) as GameObject;
<<<<<<< HEAD
        fox.transform.SetParent(boardHolder);
=======
       // fox.transform.SetParent(boardHolder);
>>>>>>> 1a5c3c476274013a503480af8c2420e2d7aa0e3f
        //grid[(int)fox.transform.position.x, (int)fox.transform.position.y].cellState = CellState.FOX;

    }
    #endregion

    public void DESTROY()
    {
        Destroy(boardHolder.gameObject);

    }

    internal void LoadState(GameState gameState)
    {
        DESTROY();

        boardHolder = new GameObject("Board").transform;
        rows = gameState.Rows;
        collumns = gameState.Collumns;

        grid = new Cell[collumns, rows];
        

        for (int x = 0; x < collumns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject instance = Instantiate(groundTile, new Vector3(x, y), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
                grid[x, y] = instance.GetComponent<Cell>();

                GameObject prefab = null;
<<<<<<< HEAD

=======
                bool settingfox = false;
>>>>>>> 1a5c3c476274013a503480af8c2420e2d7aa0e3f
                var index = x * rows + y;
                switch(gameState.cells[index])
                {
                    case CellState.FOX:
                        grid[x, y].cellState = CellState.FOX;
<<<<<<< HEAD
                        prefab = fox;
                        fox = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;
=======
                        fox.transform.position = new Vector3(x,y);
                        settingfox = true;
//                        prefab = fox;
//                        fox = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;
>>>>>>> 1a5c3c476274013a503480af8c2420e2d7aa0e3f
                        break;
                    case CellState.OCCUPIED:
                        grid[x, y].cellState = CellState.OCCUPIED;
                        prefab = occupied;
                        break;
                    case CellState.GOOSE:
                        prefab = goose;
                        grid[x, y].cellState = CellState.GOOSE;
                        break;
                    case CellState.VISITED:
                        prefab = occupied;
                        grid[x, y].cellState = CellState.VISITED;
                        break;
                    case CellState.EMPTY:
                    default:
                        break;
                }

<<<<<<< HEAD
                if (prefab != null && prefab != fox)
=======
                if (prefab != null && !settingfox )
>>>>>>> 1a5c3c476274013a503480af8c2420e2d7aa0e3f
                {
                    instance = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }

            }
        }  
        

    }


    public CellState CheckCellState(int x, int y)
	{
		return grid [x, y].cellState;
	}

    public void UpdateCell(int x, int y,CellState cellstate)
    {

        switch (cellstate)
        {
            case CellState.EMPTY:
                break;
            case CellState.VISITED:
                grid[x, y].cellState = cellstate;
                (Instantiate(occupied, new Vector2(x, y), Quaternion.identity) as GameObject).transform.SetParent(boardHolder);
                break;
            case CellState.FOX:
                grid[x, y].cellState = cellstate;
                break;
            case CellState.GOOSE:
                grid[x, y].cellState = cellstate;
                (Instantiate(goose, new Vector2(x, y), Quaternion.identity) as GameObject).transform.SetParent(boardHolder);
                break;
                
        }

    }

    public bool HasFoxMovesLeft(int xFoxPos, int yFoxPos)  //To-Do implement the code to check if there are any available moves for the Fox
    {
        if (FoxCanJump)
            return true;
        return FoxHasAdjacentMoves(xFoxPos, yFoxPos);
    }

    private bool FoxHasAdjacentMoves(int xFoxPos, int yFoxPos)
    {
        CalculateAdjacentCoordinates(xFoxPos, yFoxPos);
        foreach (var coordinate in adjacentCoordinates)
        {
            var adjacentX = coordinate.X;
            var adjacentY = coordinate.Y;

            if (OutsideOfGrid(adjacentX, adjacentY))
                continue;

            if (grid[adjacentX, adjacentY].cellState == CellState.EMPTY)
                return true;

            if (grid[adjacentX, adjacentY].cellState == CellState.GOOSE)
            {
                int behindX = adjacentX + coordinate.Direction.X;
                int behindY = adjacentY + coordinate.Direction.Y;

                if (OutsideOfGrid(behindX, behindY))
                    continue;

                if (grid[behindX, behindY].cellState == CellState.EMPTY)
                    return true;
            }
        }

        //        Debug.Log("No Obligatory Moves");
        return false;
    }
              
    private void CalculateAdjacentCoordinates(int x, int y)
    {
        adjacentCoordinates.Clear();

        Coordinate north = new Coordinate(x, y + 1,Directions.N);
        Coordinate south = new Coordinate(x, y - 1,Directions.S);
        Coordinate northEast = new Coordinate(x+1, y + 1,Directions.NE);
        Coordinate southEast = new Coordinate(x+1, y - 1,Directions.SE);
        Coordinate northWest = new Coordinate(x-1, y + 1,Directions.NW);
        Coordinate southWest = new Coordinate(x-1, y -1,Directions.SW);
        Coordinate East = new Coordinate(x+1, y,Directions.E);
        Coordinate West = new Coordinate(x-1, y,Directions.W);

        adjacentCoordinates.AddRange( new List<Coordinate>(){north, south, northEast, southEast, northWest, southWest, East, West});
    }

    

    private IList<Coordinate> FindObligatoryMoves(int xFoxPos, int yFoxPos)
    {
        if (jumpMoves == null )
            jumpMoves = new List<Coordinate>();

        jumpMoves.Clear();

        foreach (var coordinate in adjacentCoordinates)
        {
            var adjacentX = coordinate.X;
            var adjacentY = coordinate.Y;

            if (OutsideOfGrid(adjacentX,adjacentY))
                continue;
            
            if (grid[adjacentX, adjacentY].cellState == CellState.GOOSE)
            {
                int behindX = adjacentX + coordinate.Direction.X;
                int behindY = adjacentY + coordinate.Direction.Y;

                if (OutsideOfGrid(behindX,behindY))
                    continue;
                if (grid[behindX, behindY].cellState == CellState.EMPTY)
                    jumpMoves.Add(new Coordinate(behindX,behindY,coordinate.Direction));
            }
        }
        return jumpMoves;
    }

    private IList<Coordinate> FindHintMoves()
    {
        int xFox = (int)fox.transform.position.x;
        int yFox = (int)fox.transform.position.y;

        //if (hintMoves == null)
        //    hintMoves = new List<Coordinate>();

        List<Coordinate> hintMoves = new List<Coordinate>();

        CalculateAdjacentCoordinates(xFox, yFox);
        jumpMoves = FindObligatoryMoves(xFox, yFox);

        if (jumpMoves.Count == 0)
        {
            foreach (var coordinate in adjacentCoordinates)
            {
                var adjacentX = coordinate.X;
                var adjacentY = coordinate.Y;

                if (OutsideOfGrid(adjacentX, adjacentY))
                    continue;
                if (grid[coordinate.X, coordinate.Y].cellState == CellState.EMPTY)
                    hintMoves.Add(coordinate);
            }
            return hintMoves;
        }
        else 
            return jumpMoves;
        
    }

    

    public MoveState IsMoveAllowed(int xNew,int yNew, bool firstMove)
    {

        var xOld = (int)fox.transform.position.x;
        var yOld = (int)fox.transform.position.y;

        if (grid[xNew,yNew].cellState != CellState.EMPTY)
            return MoveState.NOT_ALLOWED;

        if (firstMove)
            return MoveState.NORMAL;

        CalculateAdjacentCoordinates(xOld, yOld);

        IList<Coordinate> obligatoryMoves = FindObligatoryMoves(xOld, yOld);

        //        Debug.Log("Found Obligatory Moves : " + jumpMoves.Count);

        if (obligatoryMoves.Count == 0)
        {
            foreach (var coordinate in adjacentCoordinates)
            {
                if (coordinate.X == xNew &&
                    coordinate.Y == yNew)
                    return MoveState.NORMAL;

            }

            return MoveState.NOT_ALLOWED;
        }

        foreach (var jumpMove in jumpMoves)
        {
            if (jumpMove.X == xNew &&
                jumpMove.Y == yNew)
            {
//                Debug.Log("JumpMove Removed : " +jumpMoves.Remove(jumpMove));
                return MoveState.JUMP;
            }
        }
        return MoveState.OBLIGATORY;


    }

    

    public void UpdateBoard(int xPos,int yPos,bool isFoxPlaying,bool firstMove)
    {
        if (!isFoxPlaying)
        {
            UpdateCell(xPos, yPos, CellState.GOOSE);
            return;
        }


        if (!firstMove)
        {
            var previousXPos = (int)fox.transform.position.x;
            var previousYpos = (int)fox.transform.position.y;

            UpdateCell(previousXPos, previousYpos, CellState.VISITED);
        }
        fox.transform.position = new Vector3(xPos, yPos, -1f);
        UpdateCell(xPos, yPos, CellState.FOX);

        CalculateAdjacentCoordinates(xPos, yPos);
        FindObligatoryMoves(xPos, yPos);
       
    }

    public void ShowHints()
    {
        IList<Coordinate> hintMoves = FindHintMoves();
        foreach (var hintMove in hintMoves)
        {
            Instantiate(hint, new Vector3(hintMove.X, hintMove.Y), Quaternion.identity);
        }
                   
    }

    private bool OutsideOfGrid(int xPos, int yPos)
    {
        return (xPos < 0 || xPos >= grid.GetLength(0) || yPos < 0 || yPos >= grid.GetLength(1));
    }

    public bool FoxCanJump
    {
        get 
        {
            if (jumpMoves != null)
                return jumpMoves.Count != 0;

            return false;
        }
    }


   
    public GameObject Fox
    {
        get { return fox; }

        set { if(fox==null)
                fox = value; }
    }

    

    public Cell[,] Grid
    {
        get { return grid; }
    }


}

public class Coordinate
{
    private int x;
    private int y;
    private Direction direction = null;

    public Coordinate() { }

    public Coordinate(int x, int y,Direction direction=null)
    {
        X = x;
        Y = y;
        Direction = direction;
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

    public Direction Direction
    {
        get;
        set;
    }

}





public static class Directions
{
    private static Direction north;
    private static Direction south;
    private static Direction northEast;
    private static Direction southEast;
    private static Direction northWest;
    private static Direction southWest;
    private static Direction east;
    private static Direction west;


    static Directions()
    {
        north = new Direction(0, 1);
        south = new Direction(0, -1);
        northEast = new Direction(1, 1);
        southEast = new Direction(1, -1);
        northWest = new Direction(-1, 1);
        southWest = new Direction(-1, -1);
        east = new Direction(1, 0);
        west = new Direction(-1, 0);

    }
    public static Direction N
    {
        get{ return north; }
    }

    public static Direction S
    {
        get{ return south; }
    }

    public static Direction NE
    {
        get{ return northEast; }
    }

    public static Direction SE
    {
        get{ return southEast; }
    }

    public static Direction NW
    {
        get{ return northWest; }
    }

    public static Direction SW
    {
        get{ return southWest; }
    }

    public static Direction E
    {
        get{ return east; }
    }

    public static Direction W
    {
        get{ return west; }
    }
}