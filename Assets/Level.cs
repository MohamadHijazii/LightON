using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.IO;

public class Level 
{
    [BsonId]
    public ObjectId id { get; set; }
    public int N;
    public List<List<int>> cases;
    //public string solution;
    //public string note;

    public Level(string sid)
    {
        ObjectId id = ObjectId.Parse(sid);
        BsonDocument doc = getLevel(id);
        this.id = id;
        cases = new List<List<int>>();
        N = doc["number"].AsInt32;
        for (int i = 0; i < N; i++)
        {
            List<int> temp = new List<int>();
            string s = doc["n" + i].AsString;
            string[] ss = s.Split(' ');
            for (int j = 0; j < ss.Length; j++)
            {
                int r = int.Parse(ss[j]);
                temp.Add(r);
            }
            cases.Add(temp);
        }
    }

    public Level(ObjectId id)
    {
        BsonDocument doc = getLevel(id);
        this.id = id;
        cases = new List<List<int>>();
        N = doc["number"].AsInt32;
        for(int i = 0; i < N; i++)
        {
            List<int> temp = new List<int>();
            string s = doc["n" + i].AsString;
            string[] ss = s.Split(' ');
            for(int j = 0; j < ss.Length; j++)
            {
                int r = int.Parse(ss[j]);
                temp.Add(r);
            }
            cases.Add(temp);
        }
        

    }

    private BsonDocument getLevel(ObjectId inp)
    {
        MongoClient client = new MongoClient("mongodb://127.0.0.1:27017");
        var server = client.GetServer();
        var db = server.GetDatabase("Light");
        var lvl = db.GetCollection<BsonDocument>("levels");
        BsonDocument doc = lvl.FindOneByIdAs<BsonDocument>(inp);
        return doc;

    }

    public void saveToFile()
    {
        string path = Application.persistentDataPath +"\\levels" +id.ToString()+".lvl";
        using (StreamWriter wr = File.CreateText(path))
        {
            wr.WriteLine(N);
            for (int i = 0; i < N; i++)
            {
                List<int> temp = cases[i];
                for (int j = 0; j < temp.Count; j++)
                {
                    wr.Write(temp[j]);
                    if (j != temp.Count - 1)
                        wr.Write(' ');
                }
                wr.WriteLine("");
            }

        }
        Model model = new Model();
        model.addLevel(id);
    }


}
