using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTeleporter : MonoBehaviour
{
    public EdgeTeleporter otherTeleporter;
    public Vector3 Pos => transform.position;
    public Vector3 offset;
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
            Vector3 oldPos = col.gameObject.transform.position;
            
            Vector3 newPos = new Vector3(otherTeleporter.Pos.x + otherTeleporter.offset.x, col.gameObject.transform.position.y,
                otherTeleporter.Pos.z + otherTeleporter.offset.z);
            col.gameObject.transform.position = newPos;

            CameraManager.Instance.TeleportCamera(newPos - oldPos);
        }
    }
}
