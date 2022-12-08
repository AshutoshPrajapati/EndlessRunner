using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
   
    void Update()
    {
        transform.Rotate(90*Time.deltaTime,0,0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           Destroy(gameObject);
            UiManager.instance.CoinManager();
        }
    }
}
