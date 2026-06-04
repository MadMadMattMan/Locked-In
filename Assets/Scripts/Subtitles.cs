using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Subtitles : MonoBehaviour
{
    public Transform subtitleCanvas;
    public GameObject SubtitlePrefab;
    public Font Font;
    public int FontSize;
    public float AnimationOffset;
    public float TimeToDisplayText;
    public int WPM;
    public void SubtitleText(string display)
    {
        int length = display.Length;
        int wordCount = GetWordCount(display);
        float time = TimeToDisplayText / length;
        float currentTime = time;
        float timeToDisplayLetter = WPM / 60f * wordCount;
        GUIStyle style = new GUIStyle();
        style.font = Font;
        style.fontSize = FontSize;
        Vector2 totalSize = style.CalcSize(new GUIContent(display));
        float currentPos = -totalSize.x/2;
        foreach (char c in display)
        {
            Vector2 size = style.CalcSize(new GUIContent(c.ToString()));
            float offset = currentPos + size.x/2;
            StartCoroutine(SubtitleRoutine(c, currentTime, offset, timeToDisplayLetter));
            currentTime += time;
            currentPos += size.x;
        }
    }
    IEnumerator SubtitleRoutine(char letter, float delayTime, float offset, float timeToDisplayLetter)
    {
        yield return new WaitForSeconds(delayTime);
        GameObject subtitle = Instantiate(SubtitlePrefab, subtitleCanvas);
        subtitle.GetComponent<SubtitlePrefab>().SetUptime(timeToDisplayLetter);
        Text textComponent = subtitle.GetComponent<Text>();
        subtitle.GetComponent<RectTransform>().localPosition += new Vector3(offset, 0, 0);
        textComponent.text = letter.ToString();
        textComponent.font = Font;
        textComponent.fontSize = FontSize;
        Destroy(subtitle, timeToDisplayLetter + AnimationOffset);
    }
    int GetWordCount(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        char[] delimiters = new char[] { ' ', '\r', '\n', '\t' };
        string[] words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}
