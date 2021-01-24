using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
    public int Yintensity = 50;
    public int Zintensity = 0;
    public int Xintensity = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Xintensity, Yintensity, Zintensity) * Time.deltaTime);
    }
}
