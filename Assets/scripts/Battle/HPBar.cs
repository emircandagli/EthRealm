using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject health;
    private void Start(){
        health.transform.localScale = new Vector3(0.5f,1f);
    }
}
