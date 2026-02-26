using UnityEngine;

public class destroyinteractible : Interactible
{
   public override void Interact(CCPlayer ccPlayer)
   {
      Destroy(gameObject);
      Debug.Log("Destroyed:" + gameObject.name);
   }
}
