using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bulletend());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator bulletend()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
