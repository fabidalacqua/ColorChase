﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class TeleportPlayer : MonoBehaviour
    {
        [SerializeField]
        private GameObject teleport;

        Vector2 newpos;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject)
            {
                if (teleport.transform.position.x > 0)
                    newpos = new Vector2(teleport.transform.position.x - 1.5f, teleport.transform.position.y);
                else
                    newpos = new Vector2(teleport.transform.position.x + 1.5f, teleport.transform.position.y);
                other.transform.position = newpos;
            }
        }
    }
}
