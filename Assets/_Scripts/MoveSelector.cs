using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSelector : MonoBehaviour {

    private int m_selectedDisc;
    private DiscMover m_discMover;
    private HintSystem m_hintSystem;
    private Board m_board;

    public Text m_score;
    private int m_numOfMoves = 0;
    public Text m_winText;

    private Disc m_lastSelectedDisc;

    void Start()
    {
        m_board = FindObjectOfType<Board>();
        m_discMover = FindObjectOfType<DiscMover>();
        m_hintSystem = FindObjectOfType<HintSystem>();
    }

    void Update()
    {
        m_score.text = m_numOfMoves.ToString();
        if (m_board.DidWin(m_hintSystem.FinalState()))
            m_winText.text = "You win!";
        else
            m_winText.text = "";

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MoveDisc(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MoveDisc(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MoveDisc(3);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit " + hit.collider.name);

                if(hit.collider.GetComponent<Disc>())
                {
                    if (m_lastSelectedDisc == null)
                    {
                        m_lastSelectedDisc = hit.collider.GetComponent<Disc>();
                    }
                    m_lastSelectedDisc.Unselect();
                    m_lastSelectedDisc = hit.collider.GetComponent<Disc>();
                }

                if (m_lastSelectedDisc)
                {
                    m_selectedDisc = m_lastSelectedDisc.GetDiscNumber();
                    m_lastSelectedDisc.Select();
                }
            }
        }
    }

    public void MoveDisc(int tower)
    {
        if (!m_board.DidWin(m_hintSystem.FinalState()))
        {
            if (!m_discMover.IsMoving())
            {

                switch (tower)
                {
                    case 1:
                        if(m_discMover.MoveDisc(m_selectedDisc, ETower.Source))
                            m_numOfMoves++;
                        break;
                    case 2:
                        if(m_discMover.MoveDisc(m_selectedDisc, ETower.Auxiliary))
                            m_numOfMoves++;
                        break;
                    case 3:
                        if(m_discMover.MoveDisc(m_selectedDisc, ETower.Target))
                            m_numOfMoves++;
                        break;
                    default:
                        Debug.LogWarning("Wrong index passed in.");
                        break;
                }
            }
        }
    }

    public void MoveDiscWithHint()
    {
        if (!m_board.DidWin(m_hintSystem.FinalState()))
        {
            if (!m_discMover.IsMoving())
            {
                if(m_discMover.MoveDisc(m_hintSystem.GetHint(), true))
                    m_numOfMoves++;
            }
        }
    }

    public void ResetScore()
    {
        m_numOfMoves = 0;
    }

}
