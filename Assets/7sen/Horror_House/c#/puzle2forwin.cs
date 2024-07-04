using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class puzle2forwin : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;

    // ����� ������� ������
    private float timeRemaining = 240f; // 4 ����� = 240 �����
    private bool timerIsRunning = true;

    // ����� ����� ��� ������
    public TextMeshProUGUI timerText;

    // ����� ����� ����� ���� ������ �����
    public static bool puzzleCompleted = false;

    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(5f, 11f), Random.Range(3f, -5.5f));

        // ���� �� ����� �� ������ �� ������
        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ����� ������
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

    // ���� ������ ��� ������
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // ���� ������ �� ������ �����
    void CheckPuzzleCompletion()
    {
        if (InRightPosition || puzzleCompleted)
        {
            // ������ ��� ������� �������
            SceneManager.LoadScene(7); // ������ "NextLevelSceneName" ���� ������� �������
        }
        else
        {
            // ������ ��� ����� �������
            SceneManager.LoadScene(6); // ������ "GameOverSceneName" ���� ����� �������
        }
    }

    // ���� ������ �� ���� ���� �����
    void CheckAllPieces()
    {
        PieceScript122[] pieces = FindObjectsOfType<PieceScript122>();
        foreach (PieceScript122 piece in pieces)
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