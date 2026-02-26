using UnityEngine;

public class NPCinteractible : Interactible
{
   public NPCData npcData;

   public override void Interact(CCPlayer ccPlayer)
   {
      if (npcData == null)
      {
         Debug.Log("npc has no data:" + gameObject.name);
      }
      
      ccPlayer.RequestDialogue(npcData);
   }
}
