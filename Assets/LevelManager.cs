using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    Button[] lvls;
    public Transform target;
    public Button l;
    int N;
    List<string> levels;
    
    public void loadNew()
    {
        Model model = new Model();
        model.alter();
    }

    public void goToMenu()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        if (l == null || l == null)
            return;
        Model model = new Model();
        levels = model.getLevelList();
        N = levels.Count;
        lvls = new Button[N];
        for(int i = 0; i < N; i++)
        {
            int c = i;
            Button newbtn = Instantiate<Button>(l, target);
            lvls[i] = newbtn;
            lvls[i].GetComponentInChildren<TextMeshProUGUI>().text = (i+1) + "\n"+getN(i);
            lvls[i].onClick.AddListener(() => goToLevel(c));
        }
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
}
