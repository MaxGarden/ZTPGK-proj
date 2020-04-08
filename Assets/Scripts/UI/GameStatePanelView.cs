using UnityEngine;

public sealed class GameStatePanelView : MonoBehaviour
{
    [SerializeField]
    private GameObject m_gamePanel;
    
    [SerializeField]
    private GameObject m_pausePanel;
    
    [SerializeField]
    private GameObject m_victoryPanel;

    private void Update()
    {
        var gameController = GameController.Instance;
        m_gamePanel.SetActive(gameController.State == GameController.GameState.Game);
        m_pausePanel.SetActive(gameController.State == GameController.GameState.Pause);
        m_victoryPanel.SetActive(gameController.State == GameController.GameState.Victory);
    }
}
