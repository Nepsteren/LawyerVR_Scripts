using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public enum TaskName
{
    FindDocumentsInArchive,
    DrawUpAnAgreement,
    CheckTheDetails,
    CertifyCopiesWithSeal,
    PrepareDocumentPackage,
    HandOverToTheCourier,
    AllTaskDone
}


public class Task : MonoBehaviour
{
    public AudioClip audio;
    public AudioSource audioSrc;

    public virtual bool activated {  get; set; }

    public string ruDescrption;
    public string engDescrption;

    public virtual string RUDescription { get; set; }
    public virtual string ENGDescription { get; set; }

    public virtual TaskName Name { get; set; }

    public string GetDescription()
    {
        if (LanguageControllers.instance == null)
            return ruDescrption;

        if (LanguageControllers.instance.currentLanguage == Language.Russian)
            return ruDescrption;
        else
            return engDescrption;
    }


    public virtual void Complete(TaskName name)
    {
        activated = false;
        TaskController.taskController.ChangeTask(name);
    }

    public virtual void Terms()
    {
        activated = true;
    }

    public virtual void PlaySound()
    {
        audioSrc = GetComponent<AudioSource>();

        if (audioSrc == null)
        {
            audioSrc = gameObject.AddComponent<AudioSource>();
        }

        if (audio != null)
        {
            audioSrc.PlayOneShot(audio);
        }
    }
}
