using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton - apenas uma instância do GameManager
    public static GameManager Instance { get; private set; }

    // Estados do jogo
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Victory
    }

    [Header("Game State")]
    public GameState currentState = GameState.MainMenu;

    [Header("Score System")]
    public int currentScore = 0;
    public int maxScore = 10;

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject hudPanel;

    // Eventos
    public delegate void OnScoreChanged(int newScore);
    public static event OnScoreChanged onScoreChanged;

    public delegate void OnGameStateChanged(GameState newState);
    public static event OnGameStateChanged onGameStateChanged;

    void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        ChangeGameState(GameState.MainMenu);
    }

    // Controle de Estado
    public void ChangeGameState(GameState newState)
    {
        currentState = newState;
        onGameStateChanged?.Invoke(newState);

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                ShowPanel(mainMenuPanel);
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                ShowPanel(hudPanel);
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                ShowPanel(gameOverPanel);
                break;

            case GameState.Victory:
                Time.timeScale = 0f;
                ShowPanel(victoryPanel);
                break;
        }

        Debug.Log($"[GameManager] Estado alterado para: {newState}");
    }

    // Mostrar apenas o painel especificado
    void ShowPanel(GameObject panelToShow)
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(mainMenuPanel == panelToShow);
        if (gameOverPanel != null) gameOverPanel.SetActive(gameOverPanel == panelToShow);
        if (victoryPanel != null) victoryPanel.SetActive(victoryPanel == panelToShow);
        if (hudPanel != null) hudPanel.SetActive(hudPanel == panelToShow);
    }

    // Sistema de Score
    public void AddScore(int points)
    {
        currentScore += points;
        onScoreChanged?.Invoke(currentScore);

        Debug.Log($"[GameManager] Score: {currentScore}/{maxScore}");

        // Verifica vitória ao atingir 10 coletas
        if (currentScore >= maxScore)
        {
            Victory();
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        onScoreChanged?.Invoke(currentScore);
    }

    // Controle de Fluxo
    public void StartGame()
    {
        ResetScore();
        SceneManager.LoadScene("Fase1");
        ChangeGameState(GameState.Playing);
    }

    public void GameOver()
    {
        ChangeGameState(GameState.GameOver);
        Debug.Log("[GameManager] Game Over!");
    }

    public void Victory()
    {
        ChangeGameState(GameState.Victory);
        Debug.Log("[GameManager] Vitória!");
    }

    public void RestartGame()
    {
        ResetScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ChangeGameState(GameState.Playing);
    }

    public void LoadMainMenu()
    {
        ResetScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        ChangeGameState(GameState.MainMenu);
    }

    public void QuitGame()
    {
        Debug.Log("[GameManager] Saindo do jogo...");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void PauseGame()
    {
        ChangeGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        ChangeGameState(GameState.Playing);
    }
}