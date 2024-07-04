using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PieceScript122 : MonoBehaviour
{
    private Vector3 RightPosition;
    public bool InRightPosition;
    public bool Selected;

    // إضافة متغيرات المؤقت
    private float timeRemaining = 240f; // 4 دقائق = 240 ثانية
    private bool timerIsRunning = true;

    // إضافة متغير لنص المؤقت
    public TextMeshProUGUI timerText;

    // متغير عمومي لتتبع حالة اكتمال اللغز
    public static bool puzzleCompleted = false;

    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(5f, 11f), Random.Range(3f, -5.5f));

        // تأكد من تعيين نص المؤقت في المحرر
        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // تحديث المؤقت
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

    // دالة لتحديث عرض المؤقت
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // دالة للتحقق من اكتمال اللغز
    void CheckPuzzleCompletion()
    {
        if (InRightPosition || puzzleCompleted)
        {
            // انتقال إلى المرحلة التالية
            SceneManager.LoadScene(7); // استبدل "NextLevelSceneName" باسم المرحلة التالية
        }
        else
        {
            // انتقال إلى مرحلة الخسارة
            SceneManager.LoadScene(6); // استبدل "GameOverSceneName" باسم مرحلة الخسارة
        }
    }

    // دالة للتحقق من حالة جميع القطع
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