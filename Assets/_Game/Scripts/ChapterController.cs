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
    public List<Light> suns;
    public Transform forestSage, stoneGuardian;
    void Awake()
    {
        Singelton = this;

        currentChapterIndex = PlayerPrefs.GetInt(SaveKeys.CURRENT_CHAPTER_INDEX, currentChapterIndex);

        foreach (var item in suns)
        {
            item.gameObject.SetActive(false);
        }
        suns[currentChapterIndex].gameObject.SetActive(true);

        if (currentChapterIndex == 2) // son chapter
        {
            Vector3 forestSageStartPos = forestSage.position;
            Vector3 stoneGuardianStartPos = stoneGuardian.position;
            forestSage.position = stoneGuardianStartPos;
            stoneGuardian.position = forestSageStartPos;
        }
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
