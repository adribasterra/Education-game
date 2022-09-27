using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForm : MonoBehaviour
{
    public GameObject[] boidForms;

    // Start is called before the first frame update
    public void setRandomForm()
    {
        float val = Random.value;
        if (boidForms.Length > 0)
        {
            float partSize = 1.0f / boidForms.Length;
            for (int i = 0; i < boidForms.Length; i++)
            {
                if (val >= partSize * i && val < partSize * i + partSize)
                {
                    boidForms[i].SetActive(true);
                }
            }
        }
    }
}
