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
        clockHand.fillAmount = (EraManager.Instance.Age / EraManager.Instance.CurrentEraData.lifeSpan) * .25f;
        clockText.text = EraManager.Instance.year.ToString();
    }
}
