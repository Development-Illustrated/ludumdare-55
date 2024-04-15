using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    protected int score = 0;
    [SerializeField] protected TMP_Text scoreText;
    [SerializeField] protected TMP_Text finalScoreText;
    
    [SerializeField] protected int maxFailableRequests = 10;
    protected int failedRequests = 0;
    [SerializeField] protected TMP_Text failedText;
    [SerializeField] protected GameObject endGameUI;
    [SerializeField] protected GameObject inGameUI;

    [HideInInspector]
    private List<Color> OrbColors = new List<Color>();

    private void ResetGameData()
    {
        score = 0;
        failedRequests = 0;
        scoreText.text = "Score: 0";
        finalScoreText.text = "0";
        failedText.text = "Missed orders: 0/" + maxFailableRequests;
        inGameUI.SetActive(true);
        endGameUI.SetActive(false);
    }

    public void RestartGame()
    {
        ResetGameData();

        SceneManager.LoadScene("FinalGameSceneV1");
    }

    public void QuitToMain()
    {
        ResetGameData();

        SceneManager.LoadScene("MainTitle");
    }

    public void IncrementScore(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = "Score: " + score;
        finalScoreText.text = scoreText.text;
    }

    public void IncrementFailure()
    {
        failedRequests += 1;
        failedText.text = "Missed orders: " + failedRequests + "/" + maxFailableRequests;

        if(failedRequests == maxFailableRequests)
        {
            endGameUI.SetActive(true);
            inGameUI.SetActive(false);
        }
    }

    private void CreateSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        OrbColors.Add(Color.red);
        OrbColors.Add(Color.blue);
        OrbColors.Add(Color.green);
        OrbColors.Add(Color.yellow);
        OrbColors.Add(Color.black);
        OrbColors.Add(Color.magenta);

        CreateSingleton();

        failedText.text = "Missed orders: 0/" + maxFailableRequests;
        endGameUI.SetActive(false);
    }

    private void OnEnable()
    {
        Ingredient[] ingredientsInScene = FindObjectsOfType<Ingredient>();

        foreach (Ingredient ingredient in ingredientsInScene)
        {
            Color newColor = OrbColors[UnityEngine.Random.Range(0, OrbColors.Count)];

            ingredient.color = newColor;
            Renderer renderer = ingredient.GetComponent<Renderer>();

            renderer.material.color = newColor;
            renderer.material.SetColor("_EmissionColor", newColor);

            OrbColors.Remove(newColor);
        }
    }
}
