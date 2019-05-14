using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Driver;
using MongoDB.Bson;
public class Manager : MonoBehaviour
{
    public int N;
    Button[] bulbs;    
    public Sprite on, off;
    public Button bulb;
    public Transform target;
    public bool[] states;
    public Image win;
    public Sprite green;
    public static Level lv;
    private List<List<int>> cases;
    public TextMeshProUGUI cas;
    

    private void Start()
    {
        Model m = new Model();
        m.alter();
        N = lv.N;
        cases = lv.cases;

        //cases = read();
        bulbs = new Button[N];
        states = new bool[N];
        ini();
        
        for(int i = 0; i < N; i++)
        {
            int c = i;
            Button newbtn = Instantiate<Button>(bulb, target);
            bulbs[i] = newbtn;
            bulbs[i].onClick.AddListener(() => press(c));
            bulbs[i].GetComponent<Image>().sprite = off;
            bulbs[i].GetComponentInChildren<TextMeshProUGUI>().text = i + "";
        }

        for(int i = 0; i < cases.Count; i++)
        {
            List<int> temp = cases[i];
            cas.text += i + ": ";
            for(int j = 0; j < temp.Count; j++)
            {
                cas.text += temp[j]+" "; 
            }
            cas.text += "\n";
        }
    }


    private void ini()
    {
        for (int i = 0; i < N; i++)
        {
            states[i] = false;
        }
    }

    private void press(int n)
    {
        List<int> temp = cases[n];
        for (int i = 0; i < temp.Count; i++)
            switcch_state(temp[i]);
        if (solvebale())
        {
            Win();
        }
    }

    private void switcch_state(int pos)
    {
        states[pos] = !states[pos];
        if (bulbs[pos].GetComponent<Image>().sprite == on)
            bulbs[pos].GetComponent<Image>().sprite = off;
        else
            bulbs[pos].GetComponent<Image>().sprite = on;

    }

    private bool solvebale()
    {
        for (int i = 0; i < N; i++)
            if (!states[i])
                return false;
        return true;
    }

    private void Win()
    {
        win.GetComponent<Image>().sprite = green;
        disableAll();
        Debug.Log("You won");
    }

    private void disableAll()
    {
        for (int i = 0; i < N; i++)
        {
            bulbs[i].enabled = false;
        }
    }

    public List<List<int>> read()
    {
        List<List<int>> cases;
        using (StreamReader sr = File.OpenText("D:\\LightOnGenerator\\LightOnGenerator\\test"))
        {
            string s;
            s = sr.ReadLine();
            N = int.Parse(s);
            int n = 0;
            cases = new List<List<int>>();

            while (n < N)
            {
                List<int> temp = new List<int>();
                s = sr.ReadLine();
                string[] ss = s.Split(' ');
                for (int i = 0; i < ss.Length; i++)
                {
                    int d = int.Parse(ss[i]);
                    temp.Add(d);
                }
                cases.Add(temp);
                n++;
            }
        }
        return cases;
    }
}
