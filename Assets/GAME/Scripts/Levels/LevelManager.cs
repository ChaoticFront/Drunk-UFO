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
        // ѕровер€ем, если сохранение указывает на неверный уровень или отсутствует
        if (level < 0 || level >= levelPrefabs.Length)
        {
            // ”станавливаем первый уровень
            level = 0;
            Save();
        }
    }

    private void GetSaves()
    {
        // ѕолучаем сохранение игрока, если оно существует
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            // —охранение не существует, устанавливаем значение по умолчанию
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
            // «десь может быть код дл€ очистки текущего уровн€, если необходимо

            // «агрузка нового уровн€
            Instantiate(levelPrefabs[levelIndex], Vector3.zero, Quaternion.identity);
            HideAllMenus(); // —крывает все меню, чтобы отобразить игровой уровень
            Time.timeScale = 1f; // ¬озможно, вам нужно возобновить врем€ после загрузки уровн€
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
