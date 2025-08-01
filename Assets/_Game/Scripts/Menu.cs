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
            if (PlayerPrefs.GetInt(SaveKeys.NEW_PLAYER, 0) == 0)
            {
                PlayerPrefs.SetInt(SaveKeys.NEW_PLAYER, 1);
                playButton.interactable = false;
                TransitionManager.Instance().Transition("Enterance", menuToChapterTS, 0);
            }
            else
            {
                playButton.interactable = false;
                TransitionManager.Instance().Transition("Chapter", menuToChapterTS, 0);
            }

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
