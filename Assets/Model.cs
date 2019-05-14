using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MongoDB.Bson;
using MongoDB.Driver;

public class Model
{
    string lvl_path = Application.persistentDataPath + "/levels.lvl";
    List<string> act;

    public Model()
    {
        act = new List<string>();
        if (!File.Exists(lvl_path))
        {
            FileStream stream = new FileStream(lvl_path, FileMode.Create);
            File.Create(lvl_path);
        }
        readLevels();
    }

    public List<string> getLevelList()
    {
        return act;
    }


    public void alter()
    {
        int c = 0;
        MongoClient client = new MongoClient("mongodb://127.0.0.1:27017");
        var server = client.GetServer();
        var db = server.GetDatabase("Light");
        var lvl = db.GetCollection<BsonDocument>("levels");
        var levels = lvl.FindAll();
        foreach (var level in levels)
        {
            if (!act.Contains(level["_id"].ToString()))
            {
                ObjectId newid = level["_id"].AsObjectId;
                Level l = new Level(newid);
                l.saveToFile();
                c++;
            }

        }
        Debug.LogAssertion(c + " levels are added!");

    }

    private void readLevels()
    {
        using(StreamReader sr = File.OpenText(lvl_path))
        {
            while(!sr.EndOfStream)
                act.Add(sr.ReadLine());
        }
    }

    public void addLevel(ObjectId id)
    {
        using (StreamWriter sw = File.AppendText(lvl_path))
        {
            sw.WriteLine(id);
        }
    }

}
