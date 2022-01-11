using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TheToolBox/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float speed = 1;
    public GameObject model;
    public float healthPts = 100;
}
