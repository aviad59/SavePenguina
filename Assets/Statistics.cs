using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    /*************************
    *  
    *  places conditions
    *  
    *************************/
    public bool park;
    public bool religion;
    public bool office;
    public bool store;
    public bool school;
    public bool kindergarten;

    /********************
     *
     *  city status
     *
     ********************/
    private int happines;
    private int morbidity;
    private int economy;
    public int Happines
    {
        get { return this.happines; }
        set { this.happines = value;}
    }
    public int Morbidity
    {
        get { return this.morbidity; }
        set { this.morbidity = value;}
    }
    public int Economy
    {
        get { return this.economy; }
        set {  this.economy = value;}
    }

    private string getEffect(string place, bool isOpen, string attribute)
    {
        XmlDocument document = new XmlDocument();
        document.Load(@".\Assets\effects.xml");
        string query = "//effects/" + place + "/";
        query += isOpen ? "open/" : "close/";
        query += attribute;
        XmlNode node = document.SelectSingleNode(query);
        return node.InnerText;
    }
    public int[] calcStatistics()
    {
        int[] result = new int[3];
        int change_happines = 0;
        int change_morbidity = 0;
        int change_economy = 0;

        change_economy += int.Parse(getEffect("park", park, "economy"));
        change_economy += int.Parse(getEffect("religion", religion, "economy"));
        change_economy += int.Parse(getEffect("office", office, "economy"));
        change_economy += int.Parse(getEffect("store", store, "economy"));        
        change_economy += int.Parse(getEffect("school", school, "economy"));       
        change_economy += int.Parse(getEffect("kindergarten", kindergarten, "economy"));
        result[0] = change_economy;

        change_morbidity += int.Parse(getEffect("park", park, "morbidity"));
        change_morbidity += int.Parse(getEffect("religion", religion, "morbidity"));
        change_morbidity += int.Parse(getEffect("office", office, "morbidity"));
        change_morbidity += int.Parse(getEffect("store", store, "morbidity"));
        change_morbidity += int.Parse(getEffect("school", school, "morbidity"));
        change_morbidity += int.Parse(getEffect("kindergarten", kindergarten, "morbidity"));
        result[1] = change_morbidity;

        change_happines += int.Parse(getEffect("park", park, "happines"));
        change_happines += int.Parse(getEffect("religion", religion, "happines"));
        change_happines += int.Parse(getEffect("office", park, "happines"));
        change_happines += int.Parse(getEffect("store", store, "happines"));
        change_happines += int.Parse(getEffect("school", school, "happines"));
        change_happines += int.Parse(getEffect("kindergarten", kindergarten, "happines"));
        result[2] = change_happines;

        Debug.Log("Turn effects changes: " + "economy: " + result[0]);
        Debug.Log("Turn effects changes: " + "morbidity: " + result[1]);
        Debug.Log("Turn effects changes: " + "happines: " + result[2]);

        happines += change_happines;
        morbidity += change_morbidity;
        economy += change_economy;

        return result;
    }

    private void Start()
    {
        happines = 100;
        economy = 75;
        morbidity = 50;

        park = true;
        religion = true;
        office = true;
        store = true;
        school = true;
        kindergarten = true;
}

}
