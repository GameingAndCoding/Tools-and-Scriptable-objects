using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Asteroid type" , menuName ="Asteroid")]
public class AsteroidScriptableObject : ScriptableObject
{
    public float _minForce;
    public float _maxForce;
    public float _minSize;
    public float _maxSize;
    public float _minTorque;
    public float _maxTorque;
    public Sprite asteroidSprite;


}
