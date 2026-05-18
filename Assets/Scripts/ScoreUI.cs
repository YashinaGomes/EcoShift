using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [Header("Referências")]
    public TextMeshProUGUI scoreText;
    
    [Header("Configurações")]
    public string prefixText = "Score: ";
    public string suffixText = "/10";
    public int maxScore = 10;

    void Start()
    {
        // Inscrever no evento de mudança de score
        GameManager.onScoreChanged += UpdateScoreDisplay;
        
        // Exibir score inicial
        UpdateScoreDisplay(0);
    }

    void OnDestroy()
    {
        // Desinscrever quando o objeto for destruído
        GameManager.onScoreChanged -= UpdateScoreDisplay;
    }

    // Atualizar o texto na tela
    void UpdateScoreDisplay(int currentScore)
    {
        if (scoreText != null)
        {
            scoreText.text = $"{prefixText}{currentScore}{suffixText}";
            
            // Opcional: Mudar cor quando perto de vencer
            if (currentScore >= maxScore)
            {
                scoreText.color = Color.green; // Verde quando completo
            }
            else if (currentScore >= maxScore / 2)
            {
                scoreText.color = Color.yellow; // Amarelo na metade
            }
            else
            {
                scoreText.color = Color.white; // Branco no início
            }
        }
    }
}