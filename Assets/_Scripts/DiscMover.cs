using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETower
{
    Source, 
    Auxiliary, 
    Target
}

public class DiscMover : MonoBehaviour {

    [SerializeField]
    private Transform[] m_discs;
    [SerializeField]
    private Transform[] m_pegs;
    [SerializeField]
    private Transform[] m_discLevels;
    [SerializeField]
    private float m_moveSpeed = 3.0f;
    [SerializeField]
    private bool m_instantMoves = true;

    private Board m_board;

    void Start()
    {
        m_board = FindObjectOfType<Board>();
    }

    // Used in regular play
    public void MoveDisc(int disc, ETower target, bool useHint = false)
    {
        if(disc > m_board.GetTopDiscSize(target) && m_board.IsTopDisc(disc))
        {
            // Allowed to move the disc
            MoveDisc(disc, target, m_board.GetTowerSize(target));
            m_board.UpdateBoardState(disc, target, useHint);
        }
        else
        {
            // Not allowed to move the disc
            Debug.LogWarning("Cannot move the disc there.");
        }
    }

    // Move disc based off of state
    public void MoveDisc(BoardState state, bool useHint = false)
    {
        int discToMove = m_board.GetDiscToMove(state);
        ETower target = m_board.FindTowerToMoveTo(state);

        MoveDisc(discToMove, target, useHint);
    }

    // Used in automatic solve
    public void MoveDisc(int disc, ETower target, int level)
    {
        //Debug.Log(level);
        Vector3 newPosition;
        if(m_instantMoves)
        {
            newPosition = m_discs[disc].position;
            newPosition.x = m_pegs[(int)target].position.x;
            newPosition.y = m_discLevels[level].position.y;

            m_discs[disc].position = newPosition;
        }
    }

}
