using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    Russian,
    English
}

public class LanguageControllers : MonoBehaviour
{
    public static LanguageControllers instance;

    public Language currentLanguage = Language.English;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetRussian()
    {
        currentLanguage = Language.Russian;
    }

    public void SetEnglish()
    {
        currentLanguage = Language.English;
    }

    public void ToggleLanguage()
    {
        if (currentLanguage == Language.English)
            currentLanguage = Language.Russian;
        else
        {
            currentLanguage = Language.English;
        }
    }
}
