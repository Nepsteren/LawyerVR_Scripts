using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// отвечает за телепортацию по нажатию ui по ключевым точкам
/// </summary>
public class KeyZoneTeleportation : MonoBehaviour
{
    public Transform player;
    public Transform workZone;
    public Transform questZone;
    public Transform archiveZone;
    
    /// <summary>
    /// скрипт реализующий телепортацию по ключевым точкам
    /// </summary>
    /// <param name="targetZone"></param>
    public void Teleport(Transform targetZone)
    {
        player.position = targetZone.position;
    }
}
