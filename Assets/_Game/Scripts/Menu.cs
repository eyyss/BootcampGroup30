using EasyTransition;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button playButton, creditButton, quitButton;
    public TextMeshProUGUI creditText;
    public TransitionSettings menuToChapterTS;
    void Start()
    {
        playButton.onClick.AddListener(delegate
        {
            playButton.interactable = false;
            TransitionManager.Instance().Transition("Chapter", menuToChapterTS, 0);
        });
        quitButton.onClick.AddListener(delegate
        {
            Application.Quit();
        });
        creditButton.onClick.AddListener(delegate
        {
            creditText.gameObject.SetActive(!creditText.gameObject.activeSelf);
        });
    }
}
