using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoardState
{
    public Stack<int> towerA;
    public Stack<int> towerB;
    public Stack<int> towerC;

    public void Copy(BoardState other)
    {
        towerA = new Stack<int>();
        towerB = new Stack<int>();
        towerC = new Stack<int>();

        int[] arrayA = new int[other.towerA.Count];
        arrayA = other.towerA.ToArray();
        int[] arrayB = new int[other.towerB.Count];
        arrayB = other.towerB.ToArray();
        int[] arrayC = new int[other.towerC.Count];
        arrayC = other.towerC.ToArray();

        for(int i = 0; i < arrayA.Length; i++)
        {
            towerA.Push(arrayA[i]);
        }

        for (int i = 0; i < arrayB.Length; i++)
        {
            towerB.Push(arrayB[i]);
        }

        for (int i = 0; i < arrayC.Length; i++)
        {
            towerC.Push(arrayC[i]);
        }
    }
}

public class TowersOfHanoiText : MonoBehaviour {

    //private Stack<int> towerA;
    //private Stack<int> towerB;
    //private Stack<int> towerC;

    private BoardState allTowers = new BoardState();

    private const int NUM_OF_DISK = 6;

    private Queue<string> moves = new Queue<string>();
    private Queue<BoardState> boardState = new Queue<BoardState>();
    private BoardState currentState;

    public DiscMover board;

    void Start()
    {
        board = FindObjectOfType<DiscMover>();

        // Initialize the first tower
        allTowers.towerA = new Stack<int>();
        for (int i = 0; i < NUM_OF_DISK; i++)
        {
            allTowers.towerA.Push(i);
        }
        // Initialize empty towers
        allTowers.towerB = new Stack<int>();
        allTowers.towerC = new Stack<int>();

        moves.Enqueue(("A - " +
        StackString(allTowers.towerA) +
        "B - " +
        StackString(allTowers.towerB) +
        "C - " +
        StackString(allTowers.towerC)));

        BoardState state = new BoardState();
        state.Copy(allTowers);
        //state.towerA = towerA;
        //state.towerB = towerB;
        //state.towerC = towerC;
        boardState.Enqueue(state);
        print(moves.Dequeue());

        currentState.Copy(boardState.Dequeue());

        SolveHanoi(allTowers.towerA.Count, allTowers.towerA, allTowers.towerC, allTowers.towerB);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            GameStep();
        }
	}

    private void GameStep()
    {
        if (moves.Count > 0)
        {
            board.MoveDisc(GetDiscToMove(), FindTargetTower(), GetTowerLevel());
            currentState.Copy(boardState.Dequeue());
            print(moves.Dequeue());
        }
        else
        {
            print("Game Done!");
        }
    }

    private void SolveHanoi(int disk, Stack<int> sourceTower, Stack<int> targetTower, Stack<int> auxiliaryTower)
    {
        if(disk > 0)
        {
            // Move disk-1 disks from source to auxiliary, so they are out of the way
            SolveHanoi(disk - 1, sourceTower, auxiliaryTower, targetTower);
            // Move the nth disk from source to target
            targetTower.Push(sourceTower.Pop());
            // Display progress
            moves.Enqueue(("A - " +
            StackString(allTowers.towerA) +
            "B - " +
            StackString(allTowers.towerB) +
            "C - " +
            StackString(allTowers.towerC)));

            BoardState state = new BoardState();
            state.Copy(allTowers);
            //state.towerA = towerA;
            //state.towerB = towerB;
            //state.towerC = towerC;
            boardState.Enqueue(state);

            Debug.Log("Current State: " + currentState.towerA.Count);

            // Move the disk-1 disk that we left on the auxiliary onto target
            SolveHanoi(disk - 1, auxiliaryTower, targetTower, sourceTower);
        }
    }

    private string StackString(Stack<int> stack)
    {
        int[] array = new int[stack.Count];
        array = stack.ToArray();
        string arrayDisplay = "";
        for(int i = 0; i < array.Length; i++)
        {
            arrayDisplay += array[i].ToString() + " < ";
        }
        return arrayDisplay;
    }

    private ETower FindTargetTower()
    {
        BoardState nextState = new BoardState();
        nextState.Copy(boardState.Peek());
        Debug.Log("Next " + nextState.towerA.Count + " " + nextState.towerB.Count + " " + nextState.towerC.Count);
        Debug.Log("Current " + currentState.towerA.Count + " " + currentState.towerB.Count + " " + currentState.towerC.Count);
        ETower nextTower = ETower.Target;

        if (nextState.towerA.Count > currentState.towerA.Count)
        {
            nextTower = ETower.Source;
        }
        else if(nextState.towerB.Count > currentState.towerB.Count)
        {
            nextTower = ETower.Auxiliary;
        }
        else if(nextState.towerC.Count > currentState.towerC.Count)
        {
            nextTower = ETower.Target;
        }
        else
        {
            Debug.LogWarning("Logic errorm fix this!");
        }

        return nextTower;
    }

    private int GetDiscToMove()
    {
        switch(FindTargetTower())
        {
            case ETower.Source:
                if(allTowers.towerA.Count > 0)
                    return allTowers.towerA.Peek();
                break;
            case ETower.Auxiliary:
                if (allTowers.towerB.Count > 0)
                    return allTowers.towerB.Peek();
                break;
            case ETower.Target:
                if (allTowers.towerC.Count > 0)
                    return allTowers.towerC.Peek();
                break;
        }
        Debug.LogWarning("Cannot find a disc");
        return 0;
    }

    private int GetTowerLevel()
    {
        switch (FindTargetTower())
        {
            case ETower.Source:
                return allTowers.towerA.Count;
            case ETower.Auxiliary:
                return allTowers.towerB.Count;
            case ETower.Target:
                return allTowers.towerC.Count;
        }
        Debug.LogWarning("Cannot find a tower");
        return 0;
    }
}
