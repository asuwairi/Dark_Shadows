using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PieceScript : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;

    // ÅÖÇÝÉ ãÊÛíÑÇÊ ÇáãÄÞÊ
    private float timeRemaining = 60f; // 4 ÏÞÇÆÞ = 240 ËÇäíÉ
    private bool timerIsRunning = true;

    // ÅÖÇÝÉ ãÊÛíÑ áäÕ ÇáãÄÞÊ
    public TextMeshProUGUI timerText;

    // ãÊÛíÑ Úãæãí áÊÊÈÚ ÍÇáÉ ÇßÊãÇá ÇááÛÒ
    public static bool puzzleCompleted = false;
    public bool isRight()
    {
        return InRightPosition;
    }
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(6.5f, 10.37f), (Random.Range(2f, -5f)));

        // ÊÃßÏ ãä ÊÚííä äÕ ÇáãÄÞÊ Ýí ÇáãÍÑÑ
        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ÊÍÏíË ÇáãÄÞÊ
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                CheckPuzzleCompletion();
            }
        }

        if (Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {
            if (!Selected)
            {
                transform.position = RightPosition;
                InRightPosition = true;
                CheckAllPieces();
            }
        }
    }

    // ÏÇáÉ áÊÍÏíË ÚÑÖ ÇáãÄÞÊ
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // ÏÇáÉ ááÊÍÞÞ ãä ÇßÊãÇá ÇááÛÒ
    void CheckPuzzleCompletion()
    {
        if (InRightPosition || puzzleCompleted)
        {
            // ÇäÊÞÇá Åáì ÇáãÑÍáÉ ÇáÊÇáíÉ
            SceneManager.LoadScene(0); // ÇÓÊÈÏá "NextLevelSceneName" ÈÇÓã ÇáãÑÍáÉ ÇáÊÇáíÉ
        }
        else
        {
            // ÇäÊÞÇá Åáì ãÑÍáÉ ÇáÎÓÇÑÉ
            SceneManager.LoadScene(1); // ÇÓÊÈÏá "GameOverSceneName" ÈÇÓã ãÑÍáÉ ÇáÎÓÇÑÉ
        }
    }

    // ÏÇáÉ ááÊÍÞÞ ãä ÍÇáÉ ÌãíÚ ÇáÞØÚ
    void CheckAllPieces()
    {
        PieceScript[] pieces = FindObjectsOfType<PieceScript>();
        foreach (PieceScript piece in pieces)
        {
            if (!piece.InRightPosition)
            {
                return;
            }
        }
        puzzleCompleted = true;
        CheckPuzzleCompletion();
    }
}