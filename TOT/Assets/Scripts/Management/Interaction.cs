using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
  public bool Interactable;
  public Vector3 InteractionSize;
  public LayerMask InteractionMask;
  public GameObject player;

  void Start()
  {
    Interactable = false;
  }

  void Updaate()
  {
    IsInteractable();
  }

  void IsInteractable()
  {
    Vector3 InteractionCenter = transform.position;
    Vector3 playerposition = player.transform.position;

    Bounds bounds = new Bounds(InteractionCenter, InteractionSize);

    if(bounds.Contains(playerposition))
    {
      Interactable = true;
    }
    else
    {
      Interactable = false;
    }
  }

  void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(transform.position, InteractionSize);
    }
}
