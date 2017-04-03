using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour {

    [SerializeField]
    private int m_discNumber;
    [SerializeField]
    private Color m_colour;
    private MeshRenderer m_renderer;
    private bool m_isSelected = false;
    private bool m_lerpUp = false;

    void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_colour = m_renderer.material.color;
    }

    void Update()
    {
        if(m_isSelected)
        {
            Color newColor = m_renderer.material.color;
            Debug.Log(newColor);
            if (m_lerpUp)
            {
                newColor = Color.Lerp(newColor, m_colour, 1 * Time.deltaTime);
                if (newColor == m_colour)
                    m_lerpUp = false;
            }
            else
            {
                newColor = Color.Lerp(newColor, Color.black, 1 * Time.deltaTime);
                if (newColor == Color.black)
                    m_lerpUp = true;
            }
        }
        else
        {
            m_renderer.material.color = m_colour;
            m_lerpUp = false;
        }
    }

    public int GetDiscNumber()
    {
        return m_discNumber;
    }

    public void Select()
    {
        Debug.Log("Selecting");
        m_isSelected = true;
    }

    public void Unselect()
    {
        m_isSelected = false;
    }
}
