using UnityEngine;
using System.Collections.Generic;
using System;
[System.Serializable]
public class ApiDataWrapper
{
   public ApiData[] items;
}
[System.Serializable]
public class ApiData
{
    public int id;
    public string title;
    public string worth;
    public string thumbnail;
    public string image;
    public string description;
    public string instructions;
    public string open_giveaway_url;
    public string published_date;
    public string type;
    public string platforms;
    public string end_date;
    public int users;
    public string status;
    public string gamerpower_url;
    public string open_giveaway;
}