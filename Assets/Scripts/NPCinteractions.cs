using UnityEngine;

public class NPCInteractions : MonoBehaviour
{
    public GameObject player;
    public float interactionRange = 3f;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= interactionRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithPlayer();
            }
        }
    }
    void InteractWithPlayer()
    {
        Debug.Log("HI GOOD TO SEE YOU");
    }
}
