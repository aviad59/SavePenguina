using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnActions : MonoBehaviour
{
    public GameObject snowTrans;
    public void loadSinglePlayer()
    {
        GameObject tmp = Instantiate(snowTrans, new Vector3(46, 180, -245), new Quaternion(0, 0, 0, 0)) as GameObject;
        DontDestroyOnLoad(tmp);
        StartCoroutine(waiter(tmp));
        
    }
    public void loadSimulation()
    {
        GameObject tmp = Instantiate(snowTrans, new Vector3(46, 180, -242), new Quaternion(67, 10, 0, 0)) as GameObject;
        DontDestroyOnLoad(tmp);
        SceneManager.LoadScene("AI simulation");
    }

    IEnumerator waiter(GameObject tmp)
    {
        yield return new WaitForSeconds(2);

        tmp.transform.position = new Vector3(-0.000475847017f, 22.6754189f, -9.92342377f);
        SceneManager.LoadScene("Main");
        
    }
    public void exit(Collision collision)
    {
        Application.Quit();
    }
}
