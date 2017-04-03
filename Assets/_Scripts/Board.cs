using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    private int m_numOfDiscs = 6;
    private BoardState m_currentBoardState = new BoardState();
    private HintSystem m_hints;

	void Start ()
    {
        m_hints = FindObjectOfType<HintSystem>();
        // For testing
        //InitBoard(6, 3);
    }

    public void InitBoard(int numOfDiscs, int numOfPegs)
    {
        m_numOfDiscs = numOfDiscs;

        m_currentBoardState = new BoardState();
        m_currentBoardState.towers = new Stack<int>[numOfPegs];
        m_currentBoardState.towers[0] = new Stack<int>();
        for (int i = 0; i < m_numOfDiscs; i++)
        {
            m_currentBoardState.towers[0].Push(i);
        }

        for(int i = 1; i< m_currentBoardState.towers.Length; i++)
        {
            m_currentBoardState.towers[i] = new Stack<int>();
        }
    }

    public void UpdateBoardState(int disc, ETower tower, bool useHint = false)
    {
        BoardState prevState = new BoardState();
        prevState.Copy(m_currentBoardState);

        for(int i = 0; i < m_currentBoardState.towers.Length; i++)
        {
            if (m_currentBoardState.towers[i].Contains(disc))
            {
                m_currentBoardState.towers[i].Pop();
                break;
            }
        }

        switch (tower)
        {
            case ETower.Source:
                m_currentBoardState.towers[0].Push(disc);
                break;
            case ETower.Auxiliary:
                m_currentBoardState.towers[1].Push(disc);
                break;
            case ETower.Target:
                m_currentBoardState.towers[2].Push(disc);
                break;
        }

        m_hints.CheckAgainstSolution(prevState, m_currentBoardState, useHint);
    }

    public int GetTopDiscSize(ETower tower)
    {
        // Peek the disc from the top of the tower passed in
        switch (tower)
        {
            case ETower.Source:
                if (m_currentBoardState.towers[0].Count > 0)
                    return m_currentBoardState.towers[0].Peek();
                break;
            case ETower.Auxiliary:
                if (m_currentBoardState.towers[1].Count > 0)
                    return m_currentBoardState.towers[1].Peek();
                break;
            case ETower.Target:
                if (m_currentBoardState.towers[2].Count > 0)
                    return m_currentBoardState.towers[2].Peek();
                break;
        }
        // Tower is empty
        return -1;
    }

    public bool IsTopDisc(int disc)
    {
        if (m_currentBoardState.towers[0].Contains(disc))
        {
            if(disc == m_currentBoardState.towers[0].Peek())
            {
                return true;
            }
        }
        else if (m_currentBoardState.towers[1].Contains(disc))
        {
            if (disc == m_currentBoardState.towers[1].Peek())
            {
                return true;
            }
        }
        else if (m_currentBoardState.towers[2].Contains(disc))
        {
            if (disc == m_currentBoardState.towers[2].Peek())
            {
                return true;
            }
        }

        return false;
    }

    public int GetTowerSize(ETower tower)
    {
        switch (tower)
        {
            case ETower.Source:
                return m_currentBoardState.towers[0].Count;
            case ETower.Auxiliary:
                return m_currentBoardState.towers[1].Count;
            case ETower.Target:
                return m_currentBoardState.towers[2].Count;
        }
        // Tower is empty
        return 0;
    }

    // Find the tower from which to move a disc to
    public ETower FindTowerToMoveTo(BoardState nextState)
    {
        ETower nextTower = ETower.Target;

        // If a tower in the next state has more discs then that is the target tower
        if (nextState.towers[0].Count > m_currentBoardState.towers[0].Count)
        {
            nextTower = ETower.Source;
        }
        else if (nextState.towers[1].Count > m_currentBoardState.towers[1].Count)
        {
            nextTower = ETower.Auxiliary;
        }
        else if (nextState.towers[2].Count > m_currentBoardState.towers[2].Count)
        {
            nextTower = ETower.Target;
        }
        else
        {
            Debug.LogWarning("Logic error fix this!");
        }

        return nextTower;
    }

    // Find the tower from which to move a disc from
    public ETower FindTowerToMoveFrom(BoardState nextState)
    {
        ETower sourceTower = ETower.Target;

        // If a tower in the next state has fewer discs then the move come from that tower
        if (nextState.towers[0].Count < m_currentBoardState.towers[0].Count)
        {
            sourceTower = ETower.Source;
        }
        else if (nextState.towers[1].Count < m_currentBoardState.towers[1].Count)
        {
            sourceTower = ETower.Auxiliary;
        }
        else if (nextState.towers[2].Count < m_currentBoardState.towers[2].Count)
        {
            sourceTower = ETower.Target;
        }
        else
        {
            Debug.LogWarning("Logic error fix this!");
        }

        return sourceTower;
    }


    // Get the next disc to move
    public int GetDiscToMove(BoardState state)
    {
        // Check peek the disc from the tower to move from
        switch (FindTowerToMoveFrom(state))
        {
            case ETower.Source:
                if (m_currentBoardState.towers[0].Count > 0)
                    return m_currentBoardState.towers[0].Peek();
                break;
            case ETower.Auxiliary:
                if (m_currentBoardState.towers[1].Count > 0)
                    return m_currentBoardState.towers[1].Peek();
                break;
            case ETower.Target:
                if (m_currentBoardState.towers[2].Count > 0)
                    return m_currentBoardState.towers[2].Peek();
                break;
        }
        Debug.LogWarning("Cannot find a disc");
        return 0;
    }

    public bool DidWin(BoardState finalState)
    {
        if(m_currentBoardState.Equals(finalState))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
