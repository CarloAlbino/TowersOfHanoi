using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    private int m_numOfDiscs = 6;
    private BoardState m_currentBoardState = new BoardState();

	void Start ()
    {
        // For testing
        InitBoard(6);
    }

    public void InitBoard(int numOfDiscs)
    {
        m_numOfDiscs = numOfDiscs;

        m_currentBoardState = new BoardState();

        m_currentBoardState.towerA = new Stack<int>();
        for (int i = 0; i < m_numOfDiscs; i++)
        {
            m_currentBoardState.towerA.Push(i);
        }
        m_currentBoardState.towerB = new Stack<int>();
        m_currentBoardState.towerC = new Stack<int>();
    }

    public void UpdateBoardState(int disc, ETower tower)
    {
        if(m_currentBoardState.towerA.Contains(disc))
        {
            m_currentBoardState.towerA.Pop();
        }
        else if(m_currentBoardState.towerB.Contains(disc))
        {
            m_currentBoardState.towerB.Pop();
        }
        else if(m_currentBoardState.towerC.Contains(disc))
        {
            m_currentBoardState.towerC.Pop();
        }

        switch (tower)
        {
            case ETower.Source:
                m_currentBoardState.towerA.Push(disc);
                break;
            case ETower.Auxiliary:
                m_currentBoardState.towerB.Push(disc);
                break;
            case ETower.Target:
                m_currentBoardState.towerC.Push(disc);
                break;
        }
    }

    public int GetTopDiscSize(ETower tower)
    {
        // Peek the disc from the top of the tower passed in
        switch (tower)
        {
            case ETower.Source:
                if (m_currentBoardState.towerA.Count > 0)
                    return m_currentBoardState.towerA.Peek();
                break;
            case ETower.Auxiliary:
                if (m_currentBoardState.towerB.Count > 0)
                    return m_currentBoardState.towerB.Peek();
                break;
            case ETower.Target:
                if (m_currentBoardState.towerC.Count > 0)
                    return m_currentBoardState.towerC.Peek();
                break;
        }
        // Tower is empty
        return -1;
    }

    public bool IsTopDisc(int disc)
    {
        if (m_currentBoardState.towerA.Contains(disc))
        {
            if(disc == m_currentBoardState.towerA.Peek())
            {
                return true;
            }
        }
        else if (m_currentBoardState.towerB.Contains(disc))
        {
            if (disc == m_currentBoardState.towerB.Peek())
            {
                return true;
            }
        }
        else if (m_currentBoardState.towerC.Contains(disc))
        {
            if (disc == m_currentBoardState.towerC.Peek())
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
                return m_currentBoardState.towerA.Count;
            case ETower.Auxiliary:
                return m_currentBoardState.towerB.Count;
            case ETower.Target:
                return m_currentBoardState.towerC.Count;
        }
        // Tower is empty
        return 0;
    }
}
