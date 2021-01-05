using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinsEmotions : MonoBehaviour
{
    public GameObject deadEffect;
    public GameObject poorEffect;
    public GameObject sadEffect;

    private GameObject tmp;

    private GameObject[] pinguins;
    Statistics stats;
    void Start()
    {
        pinguins = GameObject.FindGameObjectsWithTag("Pinguin");
        Debug.Log(pinguins[0]);
        
        stats = this.gameObject.GetComponent<Statistics>();

        StartCoroutine(express(Random.Range(3, 8)));
    }

    bool checkChance(int stat)
    {
        int chance = Random.Range(0, 151);
        return (chance - stat > 0);
    }
    IEnumerator express(int sec)
    {
        yield return new WaitForSeconds(sec);
        if (checkChance(stats.Economy))
            tmp = Instantiate(poorEffect, pinguins[Random.Range(0, pinguins.Length - 1)].transform) as GameObject;

        yield return new WaitForSeconds(sec);
        if (checkChance(stats.Happines))
            Instantiate(sadEffect, pinguins[Random.Range(0, pinguins.Length - 1)].transform);

        yield return new WaitForSeconds(sec);
        if (checkChance(stats.Morbidity))
            Instantiate(deadEffect, pinguins[Random.Range(0, pinguins.Length - 1)].transform);
    }

}
