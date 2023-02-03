using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public List<EraInfo> eraInfos;
    public SpriteRenderer spriteRen;
    void Start()
    {
        PeriodManager.instance.onEraChange.AddListener(UpdateEra);
    }

    public void UpdateEra()
    {
        Sprite sprite = eraInfos.Find(x => x.era == PeriodManager.instance.currentEra).sprite;
        if (sprite != null)
        {
            spriteRen.sprite = sprite;
        }else
        {
         spriteRen.sprite = null;
        }
    }
}
public struct EraInfo
{
    public Era era;
    public Sprite sprite;
}
