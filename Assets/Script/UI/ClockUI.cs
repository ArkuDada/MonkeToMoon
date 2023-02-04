using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Image clockHand;
    [SerializeField] private TMP_Text clockText;
    
    private void Update()
    {
        //clockHand.transform.eulerAngles = new Vector3(0,0,Time.time * 360 / 60);
        clockText.text = EraManager.Instance.year.ToString();
    }
}
