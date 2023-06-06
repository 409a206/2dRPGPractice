using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Example1();
        StartCoroutine(Example2());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Print10Lines() {
        for (int i = 0; i < 10; i++)
        {
            print("Line" +i.ToString());
            yield return new WaitForSeconds(2);
        }
    }

    void Example1() {
        StartCoroutine(Print10Lines());
        print("I started printing lines");
    }

    IEnumerator Example2() {
        yield return StartCoroutine(Print10Lines());
        print("I have finished printing lines");
    }
}
