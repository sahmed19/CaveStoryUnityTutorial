using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName ="Item/Gun")]
public class Gun : ScriptableObject {

    public new string name = "NAME";
    [TextArea(3,5)]
    public string description = "DESCRIPTION";

    public Sprite sprite;

    public int handleOffsetX = -11;
    public int handleOffsetY = -1;


}
