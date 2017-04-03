using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    [SerializeField]
    private GameObject m_HUDCanvas;
    [SerializeField]
    private GameObject m_menuCanvas;

    private GameSetupController m_game;
    private MoveSelector m_moveSelector;
    private RotateCamera m_rotateCamera;

    void Start()
    {
        m_game = FindObjectOfType<GameSetupController>();
        m_moveSelector = FindObjectOfType<MoveSelector>();
        m_rotateCamera = FindObjectOfType<RotateCamera>();
    }

    public void StartEasyGame()
    {
        m_game.StartEasyMode();
        m_HUDCanvas.SetActive(true);
        m_menuCanvas.SetActive(false);
        m_moveSelector.ResetScore();
        m_rotateCamera.StartGame();
    }

    public void StartMediumGame()
    {
        m_game.StartMediumMode();
        m_HUDCanvas.SetActive(true);
        m_menuCanvas.SetActive(false);
        m_moveSelector.ResetScore();
        m_rotateCamera.StartGame();
    }

    public void StartHardGame()
    {
        m_game.StartHardMode();
        m_HUDCanvas.SetActive(true);
        m_menuCanvas.SetActive(false);
        m_moveSelector.ResetScore();
        m_rotateCamera.StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        m_HUDCanvas.SetActive(false);
        m_menuCanvas.SetActive(true);
        m_rotateCamera.autoRotate = true;
    }

    public void GetHint()
    {
        m_moveSelector.MoveDiscWithHint();
    }

}
