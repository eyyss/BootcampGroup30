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
    public TransitionSettings retryTS;
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
