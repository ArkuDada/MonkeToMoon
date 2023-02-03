using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatNoclip : MonoBehaviour
{
    private bool isOn;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PlayerController.Instance.Rigid2D.isKinematic = !isOn;
            //PlayerController.Instance.Rigid2D.gravityScale = isOn ? 1 : 0;
            
            isOn = !isOn;
        }

        if (isOn)
        {
            PlayerController.Instance.Rigid2D.velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                PlayerController.Instance.transform.position += Vector3.up * 0.1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                PlayerController.Instance.transform.position += Vector3.down * 0.1f;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerController.Instance.transform.position += Vector3.left * 0.1f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                PlayerController.Instance.transform.position += Vector3.right * 0.1f;
            }
            
        }
    }
}
