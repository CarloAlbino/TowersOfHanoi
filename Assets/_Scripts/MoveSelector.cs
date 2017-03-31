using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour {

    private int m_selectedDisc;
    private DiscMover m_discMover;
    private HintSystem m_hintSystem;

    void Start()
    {
        m_discMover = FindObjectOfType<DiscMover>();
        m_hintSystem = FindObjectOfType<HintSystem>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_discMover.MoveDisc(m_selectedDisc, ETower.Source);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_discMover.MoveDisc(m_selectedDisc, ETower.Auxiliary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_discMover.MoveDisc(m_selectedDisc, ETower.Target);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            m_discMover.MoveDisc(m_hintSystem.GetHint(), true);
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
                if (hit.collider.GetComponent<Disc>())
                {
                    m_selectedDisc = hit.collider.GetComponent<Disc>().GetDiscNumber();
                }
            }
        }
    }

}
