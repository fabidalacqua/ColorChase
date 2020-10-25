using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    [HideInInspector]
    public char color;

    [SerializeField]
    private float _speed = 200f;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D.AddForce(transform.right * _speed);
    }
}
