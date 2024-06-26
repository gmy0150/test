using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D other) {
        if(other.collider.tag == "Cube"){
            
        }
    }
}
