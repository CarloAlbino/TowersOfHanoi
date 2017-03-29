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

    public void MoveDisc(int disc, ETower target, int level)
    {
        Debug.Log(level);
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
