using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    /*stats text*/
    TMP_Text happines_TEXT;
    TMP_Text economy_TEXT;
    TMP_Text morbidity_TEXT;
    
    /*places text*/
    TMP_Text school_BTN_TEXT;
    TMP_Text kindergarten_BTN_TEXT;
    TMP_Text park_BTN_TEXT;
    TMP_Text religion_BTN_TEXT;
    TMP_Text office_BTN_TEXT;
    TMP_Text store_BTN_TEXT;

    /*statistics text*/
    Statistics stats;


    public void NextTurn()
    {
        //calculate statistics
        stats.calcStatistics();
        //make sure everything is in range
        if (stats.Morbidity < 0) stats.Morbidity = 0;
        if (stats.Morbidity > 100) stats.Morbidity = 100;
        if (stats.Happines < 0) stats.Happines = 0;
        if (stats.Happines > 100) stats.Happines = 100;
        if (stats.Economy < 0) stats.Economy = 0;
        if (stats.Economy > 100) stats.Economy = 100;

        //update the values in text
        newTextStats();

        //increase turns by 1
        stats.turns++;


        //TODO: call news


    }

    #region togglePlaces
    /// <summary>
    ///  toggle the park condition.
    /// </summary>
    public void togglePark()
    {
        Debug.Log("togglePark");
        stats.park = !stats.park;
        if (park_BTN_TEXT.text == "OPEN")
        {
            park_BTN_TEXT.text = "CLOSE";
            park_BTN_TEXT.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            park_BTN_TEXT.text = "OPEN";
            park_BTN_TEXT.color = new Color32(0, 255, 0, 255);
        }
    }
    /// <summary>
    ///  toggle the religion condition.
    /// </summary>
    public void toggleReligion()
    {
        Debug.Log("toggleReligion");
        stats.religion = !stats.religion;
        if (religion_BTN_TEXT.text == "OPEN")
        {
            religion_BTN_TEXT.text = "CLOSE";
            religion_BTN_TEXT.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            religion_BTN_TEXT.text = "OPEN";
            religion_BTN_TEXT.color = new Color32(0, 255, 0, 255);
        }
    }
    /// <summary>
    ///  toggle the park condition.
    /// </summary>
    public void toggleSchool()
    {
        Debug.Log("toggleSchool");
        stats.school = !stats.school;
        if (school_BTN_TEXT.text == "OPEN")
        {
            school_BTN_TEXT.text = "CLOSE";
            school_BTN_TEXT.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            school_BTN_TEXT.text = "OPEN";
            school_BTN_TEXT.color = new Color32(0, 255, 0, 255);
        }
    }
    /// <summary>
    ///  toggle the school condition.
    /// </summary>
    public void toggleKindergarten()
    {
        Debug.Log("toggleKindergarten");
        stats.kindergarten = !stats.kindergarten;
        if (kindergarten_BTN_TEXT.text == "OPEN")
        {
            kindergarten_BTN_TEXT.text = "CLOSE";
            kindergarten_BTN_TEXT.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            kindergarten_BTN_TEXT.text = "OPEN";
            kindergarten_BTN_TEXT.color = new Color32(0, 255, 0, 255);
        }
    }
    /// <summary>
    ///  toggle the office condition.
    /// </summary>
    public void toggleOffice()
    {
        Debug.Log("toggleOffice");
        stats.office = !stats.office;
        if (office_BTN_TEXT.text == "OPEN")
        {
            office_BTN_TEXT.text = "CLOSE";
            office_BTN_TEXT.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            office_BTN_TEXT.text = "OPEN";
            office_BTN_TEXT.color = new Color32(0, 255, 0, 255);
        }
    }
    /// <summary>
    ///  toggle the store condition.
    /// </summary>
    public void toggleStore()
    {
        stats.store = !stats.store;
        if (store_BTN_TEXT.text == "OPEN")
        {
            store_BTN_TEXT.text = "CLOSE";
            store_BTN_TEXT.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            store_BTN_TEXT.text = "OPEN";
            store_BTN_TEXT.color = new Color32(0, 255, 0, 255);
        }
    }
    #endregion togglePlaces

    /// <summary>
    ///  apply the values to the text
    /// </summary>
    void newTextStats() 
    {
        happines_TEXT.text = stats.Happines.ToString();
        economy_TEXT.text = stats.Economy.ToString();
        morbidity_TEXT.text = stats.Morbidity.ToString();
    }

    private void Start()
    {
        //setup the stats variable
        stats = this.gameObject.GetComponent<Statistics>();
        //find the text 
        happines_TEXT = GameObject.Find("SCORE_happines").GetComponent<TMP_Text>();
        economy_TEXT = GameObject.Find("SCORE_economy").GetComponent<TMP_Text>();
        morbidity_TEXT = GameObject.Find("SCORE_morbidity").GetComponent<TMP_Text>();

        //setup the places condition in text
        school_BTN_TEXT = GameObject.Find("school_BTN_TEXT").GetComponent<TMP_Text>();
        kindergarten_BTN_TEXT = GameObject.Find("kindergarten_BTN_TEXT").GetComponent<TMP_Text>();
        park_BTN_TEXT = GameObject.Find("park_BTN_TEXT").GetComponent<TMP_Text>();
        store_BTN_TEXT = GameObject.Find("store_BTN_TEXT").GetComponent<TMP_Text>();
        office_BTN_TEXT = GameObject.Find("office_BTN_TEXT").GetComponent<TMP_Text>();
        religion_BTN_TEXT = GameObject.Find("religion_BTN_TEXT").GetComponent<TMP_Text>();

        //setup the statistics values in text
        newTextStats();
    }

}
