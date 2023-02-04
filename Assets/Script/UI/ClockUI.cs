using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Image clockHand;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text clockText;
    public EraManager em => EraManager.Instance;
    
    private void Update()
    {
        healthBar.value = 1-(LifeManager.Instance.Age+ em.timer / em.yearDuration) / em.CurrentEraData.lifeSpan;
        clockHand.fillAmount = (em.timer / em.yearDuration) * .25f;
        clockText.text = em.year.ToString();
    }
}
