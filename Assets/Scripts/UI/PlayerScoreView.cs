using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public sealed class PlayerScoreView : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_player;

    [SerializeField]
    private string m_prefix = "Score: ";

    private Text m_text;
    private Text Text => m_text ? m_text : m_text = GetComponent<Text>();

    public void Update() =>
        Text.text = m_prefix + m_player.Score + "/" + m_player.RequiredScore;
}
