
using UnityEngine;


[System.Serializable] //Unity will save and load values inside this class for us so that they are visible in the inspector for us to edit
                      //tell Unity how to serialize the data
public class TurretBlueprint // no Monobehavior because we don't want this to be related to a game object
{
    public GameObject prefab;
    public int cost;

}
