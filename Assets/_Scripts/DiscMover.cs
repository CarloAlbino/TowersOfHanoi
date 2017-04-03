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

    private bool m_isMoving = false;
    private Vector3[] m_movePositions = new Vector3[3];
    private int m_movingDisc;
    private int m_moveIndex = 0;
    private Board m_board;

    void Start()
    {
        m_board = FindObjectOfType<Board>();
    }

    void Update()
    {
        MoveTo();
    }

    // Used in regular play
    public bool MoveDisc(int disc, ETower target, bool useHint = false)
    {
        if(disc > m_board.GetTopDiscSize(target) && m_board.IsTopDisc(disc))
        {
            // Allowed to move the disc
            MoveDisc(disc, target, m_board.GetTowerSize(target));
            m_board.UpdateBoardState(disc, target, useHint);
            return true;
        }
        else
        {
            // Not allowed to move the disc
            Debug.LogWarning("Cannot move the disc there.");
            return false;
        }
    }

    // Move disc based off of state
    public bool MoveDisc(BoardState state, bool useHint = false)
    {
        int discToMove = m_board.GetDiscToMove(state);
        ETower target = m_board.FindTowerToMoveTo(state);

        return MoveDisc(discToMove, target, useHint);
    }

    // Used in automatic solve
    public void MoveDisc(int disc, ETower target, int level, bool instantMoves = false)
    {
        //Debug.Log(level);
        Vector3 newPosition;

        newPosition = m_discs[disc].position;
        newPosition.x = m_pegs[(int)target].position.x;
        newPosition.y = m_discLevels[level].position.y;

        if (m_instantMoves || instantMoves)
        {
            m_discs[disc].position = newPosition;
        }
        else
        {
            m_movingDisc = disc;
            Vector3 pos1 = m_discs[disc].position;
            pos1.y = m_pegs[0].position.y;
            m_movePositions[0] = pos1;
            Vector3 pos2 = m_discs[disc].position;
            pos2.x = m_pegs[(int)target].position.x;
            pos2.y = m_pegs[(int)target].position.y;
            m_movePositions[1] = pos2;
            m_movePositions[2] = newPosition; 

            m_isMoving = true;
        }
    }

    // Moves the visual component of the puzzle
    public void MoveTo()
    {
        if(m_isMoving)
        {
            if (m_moveIndex < m_movePositions.Length)
            {
                Vector3 newPos = Vector3.Lerp(m_discs[m_movingDisc].position, m_movePositions[m_moveIndex], m_moveSpeed * Time.deltaTime);
                m_discs[m_movingDisc].position = newPos;

                if (Vector3.Distance(m_discs[m_movingDisc].position, m_movePositions[m_moveIndex]) < 0.01f)
                {
                    m_moveIndex++;
                }
            }
            else
            {
                m_isMoving = false;
                m_moveIndex = 0;
            }
        }
    }

    // Are there discs moving?
    public bool IsMoving()
    {
        return m_isMoving;
    }

    public void InitBoard(int numOfDiscs)
    {
        for(int i = 0; i < m_discs.Length; i++)
        {
            if(i >= numOfDiscs)
            {
                m_discs[i].gameObject.SetActive(false);
            }
            else
            {
                m_discs[i].gameObject.SetActive(true);
            }
            MoveDisc(i, ETower.Source, i, true);
        }
    }

}
