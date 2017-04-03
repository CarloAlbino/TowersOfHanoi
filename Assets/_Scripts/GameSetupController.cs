using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupController : MonoBehaviour {

    private TowersOfHanoiSolver m_solver;
    private Board m_board;
    private DiscMover m_discs;

	void Start ()
    {
        m_solver = FindObjectOfType<TowersOfHanoiSolver>();
        m_board = FindObjectOfType<Board>();
        m_discs = FindObjectOfType<DiscMover>();
	}

    public void StartEasyMode()
    {
        m_solver.InitGame(3);
        m_board.InitBoard(3, 3);
        m_discs.InitBoard(3);
    }

    public void StartMediumMode()
    {
        m_solver.InitGame(4);
        m_board.InitBoard(4, 3);
        m_discs.InitBoard(4);
    }
    
    public void StartHardMode()
    {
        m_solver.InitGame(6);
        m_board.InitBoard(6, 3);
        m_discs.InitBoard(6);
    }

}
