using System.Collections;
using System.Collections.Generic;
using HeneGames.DialogueSystem;
using UnityEngine;

public class ChapterController : MonoBehaviour
{
    public static ChapterController Singelton;
    public EnemySpawner enemySpawner;
    public int currentChapterIndex = 0;
    public List<DialogueManager> dialogues;
    public float startWaveWaitDuration = 10;
    void Awake()
    {
        Singelton = this;
        currentChapterIndex = PlayerPrefs.GetInt(SaveKeys.CURRENT_CHAPTER_INDEX, currentChapterIndex);
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        DialogueUI.instance.StartDialogue(dialogues[currentChapterIndex]);
    }
    public void StartEnemySpawner()
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(startWaveWaitDuration);
            enemySpawner.gameObject.SetActive(true);
        }
    }
    public void NextChapter()
    {
        currentChapterIndex += 1;
        PlayerPrefs.SetInt(SaveKeys.CURRENT_CHAPTER_INDEX, currentChapterIndex);

    }
}
