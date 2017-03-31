using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystem : MonoBehaviour {

    private Board m_board;

    private Stack<BoardState> m_hints = new Stack<BoardState>();
    private BoardState[] m_perfectStates;

	void Start ()
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
        for(int i = 0; i < m_perfectStates.Length; i++)
        {
            if(m_perfectStates[i].Equals(state))
            {
                m_hints.Clear();
                m_hints.Push(m_perfectStates[i + 1]);
                foundMatch = true;
                break;
            }
        }

        if(!foundMatch && !useHint)
        {
            m_hints.Push(prevState);
        }
    }

    public BoardState GetHint()
    {
        return m_hints.Pop();
    }

}
