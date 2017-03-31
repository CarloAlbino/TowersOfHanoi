using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour {

    private int m_selectedDisc;
    private DiscMover m_discMover;

    void Start()
    {
        m_discMover = FindObjectOfType<DiscMover>();
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
