using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    public int Score { get; private set; }
    public int RequiredScore { get; private set; }

    private void Start() =>
        RequiredScore = FindObjectsOfType<CollectibleController>().Length;

    public void OnScore()
    {
        if (++Score >= RequiredScore)
            GameController.Instance.NotifyWin();
    }
}

