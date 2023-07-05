using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject storyPanel;
    [SerializeField] private LevelManager levelManager;

    public void OnStartButtonClicked()
    {
        startMenu.SetActive(false);
        levelManager.ActivateStoryPanel();
    }
}