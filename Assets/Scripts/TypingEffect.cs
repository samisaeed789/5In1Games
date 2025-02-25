using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TypingEffect : MonoBehaviour
{
    public Text textComponent;  // Reference to the Text component
    public string fullText;     // The full text to display
    public float typingSpeed = 0.05f;  // Typing speed (in seconds per character)
    public event Action OnTypingFinished;

    private Coroutine typingCoroutine;
    MySoundManager soundman;
    

    private IEnumerator TypeText()
    {
        textComponent.text = "";
        foreach (char letter in fullText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void settext(string s) 
    {
        fullText = s; 
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); 
        }
        
        if(textComponent!=null)
            typingCoroutine = StartCoroutine(TypeText());
    }
}