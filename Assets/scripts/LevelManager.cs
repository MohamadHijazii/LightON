using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    Button[] lvls;
    public Button l;
    int N;
    List<string> levels;
    int general_index;
    public Button next, prev;
    public Transform panCenter;
    public GameObject panel;
    private int g;

    public void loadNew()
    {
        Model model = new Model();
        model.alter();
    }

    public void goToMenu()
    {
        SceneManager.LoadScene(1);  //edit this 
    }

    private void Start()
    {
        if (l == null || l == null)
            return;
        Model model = new Model();
        levels = model.getLevelList();
        N = levels.Count;
        lvls = new Button[N];
        Debug.Log(N);
        
        general_index = 0;
        iniPanels();
        //ini button here
        g = 0;
        checkEnable();
    }

    private void checkEnable()
    {
        if (g == 0)
            prev.interactable = false;
        if (g > 0)
            prev.interactable = true;
        if (g > N / 8)
            next.interactable = false;
        if (g <= N / 8)
            next.interactable = true;
        
    }

    private int getN(int n)
    {
        Level l = new Level(levels[n]);
        return l.N;
    }

    private void goToLevel(int n)
    {
        Level l = new Level(levels[n]);
        Manager.lv = l;
        SceneManager.LoadScene(2);
    }

    private void iniButton(Transform target)
    {
        int n = general_index;
        
        for (int i = n; i < n + 8 && i<N; i++)
        {            
            int c = i;
            Button newbtn = Instantiate<Button>(l, target);
            lvls[i] = newbtn;
            lvls[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1) + "";// + "\n"+getN(i);
            lvls[i].onClick.AddListener(() => goToLevel(c));
        }
        general_index += 8;
        
    }


    public void iniPanels()
    {
        Vector3 v = new Vector3(640, 260);
        panCenter.SetPositionAndRotation(v , Quaternion.identity);
        Transform toAdd = panCenter;
        Vector3 vec = v;
        for(int i = 0; i <= N/8; i++)
        {
            var brain = Instantiate(panel,toAdd);
            var brain1 = brain;
            iniButton(brain1.transform);
        }
        
    }


    public void Next()
    {
        g += 8;
        checkEnable();
        Debug.Log("go to next");
    }

    public void previews()
    {
        g -= 8;
        checkEnable();
        Debug.Log("go to previews");
    }
}
