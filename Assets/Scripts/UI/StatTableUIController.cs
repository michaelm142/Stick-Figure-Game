using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFG;
using TMPro;

public class StatTableUIController : MonoBehaviour
{
    public TMP_Text stealthStat;
    public TMP_Text strengthStat;
    public TMP_Text socialStat;
    public TMP_Text staminaStat;
    public TMP_Text scienceStat;

    public StatTable StatTable;

    // Start is called before the first frame update
    void Start()
    {
        StatTable = new StatTable(15/*stealth*/, 10/*strength*/, 10/*social*/, 10/*science*/, 15/*stamina*/);
    }

    // Update is called once per frame
    void Update()
    {
        stealthStat.text = StatTable.Stealth.ToString();
        strengthStat.text = StatTable.Strength.ToString();
        socialStat.text = StatTable.Social.ToString();
        staminaStat.text = StatTable.Stamina.ToString();
        scienceStat.text = StatTable.Science.ToString();
    }
}
