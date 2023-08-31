using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public class Stat
    {
        public int strength;
        public int intelligence;
        public int luck;
        public int speed;

        public float hp;
        public float atk;
        public float def;
        public int cost;

        public Stat(int _strength, int _intelligence, int _luck, int _speed, float _hp, float _atk, float _def, int _cost)
        {
            strength = _strength;
            intelligence = _intelligence;
            luck = _luck;
            speed = _speed;
            hp = _hp;
            atk = _atk;
            def = _def;
            cost = _cost;
        }
    }


    public List<Stat> stat = new List<Stat>();
}
