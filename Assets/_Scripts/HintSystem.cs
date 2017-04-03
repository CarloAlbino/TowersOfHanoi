using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystem : MonoBehaviour {

    private Board m_board;

    private Stack<BoardState> m_hints = new Stack<BoardState>();
    private BoardState[] m_perfectStates;

    void Start()
    {
        m_board = FindObjectOfType<Board>();
    }

    public void SetHintSystem(Queue<BoardState> solution)
    {
        m_perfectStates = solution.ToArray();
        m_hints.Push(m_perfectStates[1]);
    }

    public void CheckAgainstSolution(BoardState prevState, BoardState state, bool useHint = false)
    {
        bool foundMatch = false;
        //if (!IsMoveRepeated(state))
        //{
            for (int i = 0; i < m_perfectStates.Length; i++)
            {
                if (m_perfectStates[i].Equals(state))
                {
                    m_hints.Clear();
                    if (i + 1 < m_perfectStates.Length)
                        m_hints.Push(m_perfectStates[i + 1]);
                    else
                        m_hints.Push(m_perfectStates[i]);
                    foundMatch = true;
                    break;
                }
            }

            if (!foundMatch && !useHint)
            {
                m_hints.Push(prevState);
            }
        //}
    }

    public BoardState GetHint()
    {
        if (m_hints.Count > 0)
            return m_hints.Pop();
        else
            return m_perfectStates[m_perfectStates.Length - 1];
    }

    private bool IsMoveRepeated(BoardState state)
    {
        BoardState[] hints = m_hints.ToArray();
        int popAmount = 0;
        bool isRepeated = false;

        for(int i = hints.Length - 1; i > -1; i--)
        {
            if(hints[i].Equals(state))
            {
                isRepeated = true;
                break;
            }
            else
            {
                popAmount++;
            }
        }

        if(isRepeated)
        {
            if (popAmount > 0)
            {
                for (int i = 0; i < popAmount + 1; i++)
                {
                    if(m_hints.Count > 1)
                        m_hints.Pop();
                }
            }
        }

        return isRepeated;
    }

    public BoardState FinalState()
    {
        if (m_perfectStates != null)
            return m_perfectStates[m_perfectStates.Length - 1];
        else
        {
            BoardState b = new BoardState();
            b.towers = new Stack<int>[3];

            for (int i = 0; i < b.towers.Length; i++)
            {
                b.towers[i] = new Stack<int>();
            }
            return b;
        }
    }

}
