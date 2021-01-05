using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class News : MonoBehaviour
{

    string path = "newsArticle.txt";
    int[] sections_index = new int[8];
    string[] lines;

    Statistics stats;

    void Start()
    {
        stats = this.gameObject.GetComponent<Statistics>();
        setupfile();
    }

    /// <summary>
    /// This function read the news text as save it, also check for each section line index
    /// </summary>
    private void setupfile()
    {
        lines = File.ReadAllLines(path, Encoding.UTF8);
        for (int i = 0; i < lines.Length; i++)
        {
            switch(lines[i])
            {
                case "--holidays": 
                    sections_index[0] = i+1;
                    break;
                case "--random":
                    sections_index[1] = i + 1;
                    break;
                case "--happy":
                    sections_index[2] = i + 1;
                    break;
                case "--unhappy":
                    sections_index[3] = i + 1;
                    break;
                case "--rich":
                    sections_index[4] = i + 1;
                    break;
                case "--poor":
                    sections_index[5] = i + 1;
                    break;
                case "--high morbidity":
                    sections_index[6] = i + 1;
                    break;
                case "--low morbidity":
                    sections_index[7] = i + 1;
                    break;
            }
        }
    }

    /// <summary>
    /// This function returns an article base on the arguments given
    /// </summary>
    public string getMsg(int morbidity, int happiness, int economy, bool is_holiday)
    {
        //if there is a holiday refer to it only
        if(is_holiday)
        {
            return lines[Random.Range(sections_index[0], sections_index[1])];
        }
        int tmp = Random.Range(0, 7);
        return lines[Random.Range(sections_index[tmp], sections_index[tmp + 1])];
    }

}
