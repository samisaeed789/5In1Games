using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TypingEffect1 : MonoBehaviour
{
    public Text textComponent;  // Reference to the Text component
    public string fullText;     // The full text to display
    public float typingSpeed = 0.05f;  // Typing speed (in seconds per character)
    public event Action OnTypingFinished;

    private Coroutine typingCoroutine;
    MySoundManager soundman;
    private void OnEnable()
    {
        soundman = MySoundManager.instance;
        if (textComponent != null)
        {
            StartCoroutine(TypeText());
        }
    }

    private IEnumerator TypeText()
    {
        //textComponent.text = "";
        //soundman?.PlayTypeSound(true);
        //foreach (char letter in fullText)
        //{
        //    textComponent.text += letter;
        //    yield return new WaitForSeconds(typingSpeed);
        //}
        //OnTypingFinished?.Invoke();

        textComponent.text = "";
        soundman?.PlayTypeSound(true);
        foreach (char letter in fullText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        OnTypingFinished?.Invoke();
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