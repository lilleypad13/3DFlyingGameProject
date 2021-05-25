using UnityEngine;
using UnityEngine.UI;

public class UIGameTimer : MonoBehaviour
{
    [SerializeField] private string initialTimeText = "Time: ";
    [SerializeField] private string initialCompletionTimeText = "Completed: ";

    [SerializeField] private Text gameTimerText;
    [SerializeField] private Text completionTimeText;
    private void Start()
    {
        TargetManager.MissionAccomplished += CompletionTimeSet;
    }

    private void OnDestroy()
    {
        TargetManager.MissionAccomplished -= CompletionTimeSet;
    }

    private void Update()
    {
        gameTimerText.text = initialTimeText + GameManager.GameTimer.ToString("F2");
    }

    private void CompletionTimeSet()
    {
        Debug.Log("Completion time set");
        completionTimeText.text = initialCompletionTimeText + GameManager.GameTimer.ToString("F2");
    }
}
