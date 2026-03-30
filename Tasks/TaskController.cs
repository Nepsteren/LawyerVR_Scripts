using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Контроллер заданий
public class TaskController : MonoBehaviour
{
    public static TaskController taskController;

    public List<Task> Tasks = new List<Task>();


    //public AudioClip audio;
    //public AudioSource audioSrc;

    private Task currentTask;

    void Awake()
    {
        if (taskController == null)
        {
            taskController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Task[] tasks = FindObjectsOfType<Task>();
        Tasks.AddRange(tasks);

        foreach (Task task in tasks)
        {
            task.activated = false;
            task.gameObject.SetActive(false);
        }

        StartTask(TaskName.FindDocumentsInArchive);
    }

    public void StartTask(TaskName taskName)
    {
        foreach (Task task in Tasks)
        {
            if (task.Name == taskName)
            {
                currentTask = task;
                task.gameObject.SetActive(true);
                task.activated = true;
                break;
            }
        }
    }

    public void ChangeTask(TaskName taskName)
    {
        if (currentTask != null)
        {
            currentTask.activated = false;
            currentTask.gameObject.SetActive(true);
        }

        StartTask(taskName);
    }

    public Task GetCurrentTask()
    {
        return currentTask;
    }

    //public virtual void PlaySound()
    //{
    //    audioSrc = GetComponent<AudioSource>();

    //    if (audioSrc == null)
    //    {
    //        audioSrc = gameObject.AddComponent<AudioSource>();
    //    }

    //    if (audio != null)
    //    {
    //        audioSrc.PlayOneShot(audio);
    //    }
    //}


}