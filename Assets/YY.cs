using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{


    

    public class YY : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
           
            if (other.CompareTag("Obstacle"))
            {
              
                Debug.Log("Вы уничтожены");

              
                //Destroy(gameObject);
            }
        }
    }

}
