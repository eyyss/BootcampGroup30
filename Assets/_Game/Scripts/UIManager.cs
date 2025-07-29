using System.Collections;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singelton;
    public GameObject defeatPanel;
    public Button retryButton;
    public GameObject victoryPanel;
    public Button nextButton;
    public TransitionSettings retryTS;
    public GameObject preparationPanel;
    public GameObject finalWavePanel;

    void Awake()
    {
        Singelton = this;
    }

    void Start()
    {
        retryButton.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
            TransitionManager.Instance().Transition(SceneManager.GetActiveScene().buildIndex, retryTS, 0);
        });
        nextButton.onClick.AddListener(delegate
        {
            nextButton.interactable = false;
            ChapterController.Singelton.NextChapter();
            TransitionManager.Instance().Transition(SceneManager.GetActiveScene().buildIndex, retryTS, 0);
        });
    }
    public void OpenPreparationPanel()
    {
        preparationPanel.SetActive(true);
    }
    public void ClosePreparationPanel()
    {
        preparationPanel.SetActive(false);
    }
    public void OpenFinalWavePanel()
    {
        IEnumerator FinalWaveOpen()
        {
            finalWavePanel.SetActive(true);
            yield return new WaitForSeconds(2f);
            finalWavePanel.SetActive(false);
        }
        StartCoroutine(FinalWaveOpen());
    }



    public void OpenDefeatPanel()
    {
        Time.timeScale = 0;
        defeatPanel.SetActive(true);
    }

    public void OpenVictoryPanel()
    {
        victoryPanel.SetActive(true);
    }
}
