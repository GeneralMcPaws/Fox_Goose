﻿using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    public CellState cellState = CellState.EMPTY;

	// Use this for initialization
	void Start () {
       
	}

	public void OnMouseDown()
	{
        if(Input.GetMouseButtonDown(0))
            GameManager.instance.playTurn (this.transform.position.x, this.transform.position.y, this.cellState);
	}



}
