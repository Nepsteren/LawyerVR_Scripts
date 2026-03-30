using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionTrigger : MonoBehaviour
{
    public HandOverToTheCourierTask task;


        void Start()
        {
            //Debug.Log($"ReceptionTrigger Start. Объект: {gameObject.name}");

            task = FindObjectOfType<HandOverToTheCourierTask>();
            //if (task == null)
            //{
            //    Debug.LogError(" HandOverToTheCourierTask НЕ НАЙДЕН на сцене!");
            //}
            //else
            //{
            //    Debug.Log($"Задание найдено: {task.gameObject.name}");
            //}

            // Проверяем коллайдер
            //Collider collider = GetComponent<Collider>();
            //if (collider == null)
            //{
            //    Debug.LogError("Нет Collider компонента!");
            //}
            //else
            //{
            //    Debug.Log($"Collider есть. IsTrigger: {collider.isTrigger}");
            //}
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок вошел в зону");

            if (task != null)
            {
                task.ArriveAtReception();
            }
            else
            {
                Debug.LogWarning("HandOverToTheCourierTask не найден!");    
            }
        }
    }
}
