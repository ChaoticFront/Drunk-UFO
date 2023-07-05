using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int level;
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject playerWin;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject storyPanel;


    private void Awake()
    {
        GetSaves();
        Init();
    }

    private void Start()
    {
        ShowStartMenu();
    }

    private void Init()
    {
        // ���������, ���� ���������� ��������� �� �������� ������� ��� �����������
        if (level < 0 || level >= levelPrefabs.Length)
        {
            // ������������� ������ �������
            level = 0;
            Save();
        }
    }

    private void GetSaves()
    {
        // �������� ���������� ������, ���� ��� ����������
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            // ���������� �� ����������, ������������� �������� �� ���������
            level = 0;
        }
    }

    public void AddLevel()
    {
        level++;
        Save();
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < levelPrefabs.Length)
        {
            // ����� ����� ���� ��� ��� ������� �������� ������, ���� ����������

            // �������� ������ ������
            Instantiate(levelPrefabs[levelIndex], Vector3.zero, Quaternion.identity);
            HideAllMenus(); // �������� ��� ����, ����� ���������� ������� �������
            Time.timeScale = 1f; // ��������, ��� ����� ����������� ����� ����� �������� ������
        }
        else
        {
            Debug.LogError("Invalid level index: " + levelIndex);
        }
    }

    public void ActivateStoryPanel()
    {
        HideAllMenus();
        storyPanel.SetActive(true);
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        HideAllMenus();
        gameOver.SetActive(true);
    }

    private void PlayerWin()
    {
        Time.timeScale = 0f;
        HideAllMenus();
        playerWin.SetActive(true);
    }

    private void ShowStartMenu()
    {
        Time.timeScale = 0f;
        HideAllMenus();
        startMenu.SetActive(true);
    }

    public void StartGame()
    {
        HideAllMenus();
        storyPanel.SetActive(true);
        LoadLevel(0);
    }

    public void Next()
    {
        HideAllMenus();
        LoadLevel(0);
    }

    private void HideAllMenus()
    {
        startMenu.SetActive(false);
        playerWin.SetActive(false);
        gameOver.SetActive(false);
        storyPanel.SetActive(false);
    }

    private void Save()
    {
        PlayerPrefs.SetInt("level", level);
    }
}
