using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour {

    [SerializeField]
    private int m_discNumber;

    public int GetDiscNumber()
    {
        return m_discNumber;
    }
}
