using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("FireData")]
public class FireData
{
    [XmlArray("fireInfoList")]
    [XmlArrayItem("fireInfo")]
    public List<FireInfo> fireInfoList = new List<FireInfo>();
}

public class FireInfo
{
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public int type;
    [XmlAttribute]
    public int num;
    [XmlAttribute]
    public float cd;
    [XmlAttribute]
    public string ids;
    [XmlAttribute]
    public float delay;
}
