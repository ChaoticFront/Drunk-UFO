using UnityEngine;

public class StoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject storyPanel;

    public void HidePanel()
    {
        storyPanel.SetActive(false);
    }
}