using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public GameObject[] parkObj = new GameObject[2];
    public GameObject[] religionObj = new GameObject[2];
    public GameObject[] officeObj = new GameObject[2];
    public GameObject[] storeObj = new GameObject[2];
    public GameObject[] schoolObj = new GameObject[2];
    public GameObject[] kindergartenObj = new GameObject[2];

    // Start is called before the first frame update
    public void applyChange(bool park, bool religion, bool office, bool store, bool school, bool kindergarten)
    {
        parkObj[0].SetActive(park);
        parkObj[1].SetActive(!park);

        religionObj[0].SetActive(religion);
        religionObj[1].SetActive(!religion);

        officeObj[0].SetActive(office);
        officeObj[1].SetActive(!office);

        storeObj[0].SetActive(store);
        storeObj[1].SetActive(!store);

        schoolObj[0].SetActive(school);
        schoolObj[1].SetActive(!school);

        kindergartenObj[0].SetActive(kindergarten);
        kindergartenObj[1].SetActive(!kindergarten);
    }

}
