using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Structs

// Stores the state of the board
public struct BoardState
{
    public Stack<int> towerA;
    public Stack<int> towerB;
    public Stack<int> towerC;

    // Copy the passed in state to this state
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

#endregion Structs

public class TowersOfHanoiSolver : MonoBehaviour {

    #region Variables

    // The number of discs in the game
    private const int NUM_OF_DISCS = 6;

    // Store the string versions of the solution
    private Queue<string> m_solutionMoves = new Queue<string>();
    // Store the state version of the solution
    private Queue<BoardState> m_boardStates = new Queue<BoardState>();
    // Store the current state
    private BoardState m_currentState;

    // Used to store temporary states while solving the game
    private BoardState m_tempSolveState = new BoardState();

    // Controls the visual ascpect of the puzzle
    private DiscMover visualBoard;

    #endregion Variables

    #region Monobehaviour

    void Start()
    {
        // Get reference to the visual component of the game
        visualBoard = FindObjectOfType<DiscMover>();
        // Initialize the game and it's solution
        InitGame();
    }
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            GameStep();
        }
	}

    #endregion Monobehaviour

    #region Game Control

    // Initialize game
    public void InitGame()
    {
        // Use the temp solve state to solve the puzzle and save all the states:
        // Initialize the first tower
        m_tempSolveState.towerA = new Stack<int>();
        for (int i = 0; i < NUM_OF_DISCS; i++)
        {
            m_tempSolveState.towerA.Push(i);
        }
        // Initialize empty towers
        m_tempSolveState.towerB = new Stack<int>();
        m_tempSolveState.towerC = new Stack<int>();

        // Save the initial state of the puzzle in string form
        m_solutionMoves.Enqueue(("A - " +
        StackString(m_tempSolveState.towerA) +
        "B - " +
        StackString(m_tempSolveState.towerB) +
        "C - " +
        StackString(m_tempSolveState.towerC)));

        // Add the initial board state to the queue
        BoardState state = new BoardState();
        state.Copy(m_tempSolveState);
        m_boardStates.Enqueue(state);
        //print(m_solutionMoves.Dequeue());
        // Set the current state of the board
        m_solutionMoves.Dequeue();
        m_currentState.Copy(m_boardStates.Dequeue());

        // Solve the board
        SolveHanoi(m_tempSolveState.towerA.Count, m_tempSolveState.towerA, m_tempSolveState.towerC, m_tempSolveState.towerB);
    }

    // Display next step
    public void GameStep()
    {
        if (m_solutionMoves.Count > 0)
        {
            // Move disc according to solution
            visualBoard.MoveDisc(GetDiscToMove(), FindTowerToMoveTo(), GetTowerLevel());
            // Set the current state to the actual current state + remove the current state from the queue
            m_currentState.Copy(m_boardStates.Dequeue());
            //print(m_solutionMoves.Dequeue());
            m_solutionMoves.Dequeue();  // Remove the current state from the string queue
        }
        else
        {
            // Game over
            print("Game Done!");
        }
    }

    /// <summary>
    /// Solve towers of hanoi from a set start location
    /// </summary>
    /// <param name="disc">The disc number to move</param>
    /// <param name="sourceTower">The tower the disc is located at</param>
    /// <param name="targetTower">The target tower</param>
    /// <param name="auxiliaryTower">The auxiliary tower</param>
    private void SolveHanoi(int disc, Stack<int> sourceTower, Stack<int> targetTower, Stack<int> auxiliaryTower)
    {
        if(disc > 0)
        {
            // Move disk-1 disks from source to auxiliary, so they are out of the way
            SolveHanoi(disc - 1, sourceTower, auxiliaryTower, targetTower);
            // Move the nth disk from source to target
            targetTower.Push(sourceTower.Pop());

            // Save progress as a string to solution queue
            m_solutionMoves.Enqueue(("A - " +
            StackString(m_tempSolveState.towerA) +
            "B - " +
            StackString(m_tempSolveState.towerB) +
            "C - " +
            StackString(m_tempSolveState.towerC)));

            // Save progress as a BoardState
            BoardState state = new BoardState();
            state.Copy(m_tempSolveState);
            m_boardStates.Enqueue(state);

            //Debug.Log("Current State: " + currentState.towerA.Count);

            // Move the disk-1 disk that we left on the auxiliary onto target
            SolveHanoi(disc - 1, auxiliaryTower, targetTower, sourceTower);
        }
    }

    #endregion Game Control

    #region Display Logic

    // Translate an int stack into string from
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

    // Find the tower from which to move a disc from
    private ETower FindTowerToMoveFrom()
    {
        BoardState nextState = new BoardState();
        nextState.Copy(m_boardStates.Peek());
        ETower sourceTower = ETower.Target;

        // If a tower in the next state has fewer discs then the move come from that tower
        if (nextState.towerA.Count < m_currentState.towerA.Count)
        {
            sourceTower = ETower.Source;
        }
        else if(nextState.towerB.Count < m_currentState.towerB.Count)
        {
            sourceTower = ETower.Auxiliary;
        }
        else if(nextState.towerC.Count < m_currentState.towerC.Count)
        {
            sourceTower = ETower.Target;
        }
        else
        {
            Debug.LogWarning("Logic error fix this!");
        }

        return sourceTower;
    }

    // Find the tower from which to move a disc to
    private ETower FindTowerToMoveTo()
    {
        BoardState nextState = new BoardState();
        nextState.Copy(m_boardStates.Peek());
        ETower nextTower = ETower.Target;

        // If a tower in the next state has more discs then that is the target tower
        if (nextState.towerA.Count > m_currentState.towerA.Count)
        {
            nextTower = ETower.Source;
        }
        else if (nextState.towerB.Count > m_currentState.towerB.Count)
        {
            nextTower = ETower.Auxiliary;
        }
        else if (nextState.towerC.Count > m_currentState.towerC.Count)
        {
            nextTower = ETower.Target;
        }
        else
        {
            Debug.LogWarning("Logic error fix this!");
        }

        return nextTower;
    }

    // Get the next disc to move
    private int GetDiscToMove()
    {
        // Check peek the disc from the tower to move from
        switch(FindTowerToMoveFrom())
        {
            case ETower.Source:
                if(m_currentState.towerA.Count > 0)
                    return m_currentState.towerA.Peek();
                break;
            case ETower.Auxiliary:
                if (m_currentState.towerB.Count > 0)
                    return m_currentState.towerB.Peek();
                break;
            case ETower.Target:
                if (m_currentState.towerC.Count > 0)
                    return m_currentState.towerC.Peek();
                break;
        }
        Debug.LogWarning("Cannot find a disc");
        return 0;
    }

    // See how tall a tower is, how many discs there are on the tower
    // Used to show at what level to move the disc to 
    private int GetTowerLevel()
    {
        switch (FindTowerToMoveTo())
        {
            case ETower.Source:
                return m_currentState.towerA.Count;
            case ETower.Auxiliary:
                return m_currentState.towerB.Count;
            case ETower.Target:
                return m_currentState.towerC.Count;
        }
        Debug.LogWarning("Cannot find a tower");
        return 0;
    }

    #endregion Display Logic
}
