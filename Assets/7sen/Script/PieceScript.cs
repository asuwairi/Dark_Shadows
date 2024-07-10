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

    // ����� ������� ������
    private float timeRemaining = 60f; // 4 ����� = 240 �����
    private bool timerIsRunning = true;

    // ����� ����� ��� ������
    public TextMeshProUGUI timerText;

    // ����� ����� ����� ���� ������ �����
    public static bool puzzleCompleted = false;
    public bool isRight()
    {
        return InRightPosition;
    }
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(6.5f, 10.37f), (Random.Range(2f, -5f)));

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
            SceneManager.LoadScene(0); // ������ "NextLevelSceneName" ���� ������� �������
        }
        else
        {
            // ������ ��� ����� �������
            SceneManager.LoadScene(1); // ������ "GameOverSceneName" ���� ����� �������
        }
    }

    // ���� ������ �� ���� ���� �����
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