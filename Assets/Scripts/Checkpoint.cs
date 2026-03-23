using System.Collections.ObjectModel;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;


[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
  public int checkpointTndex;
  
  private void  OnTriggerEnter(Collider other) 
  {
    if(other.gameObject.CompareTag("Player"))
        {
            RaceManager.Instance.CheckpointReached(checkpointTndex);
        }
  }
}
