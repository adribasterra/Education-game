using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenManagerUI : MonoBehaviour
{
    public Text text;
    private int numCargo;

    //Encapsulation
    public int NumCargo {
        get { return numCargo; }
        set {
            if (numCargo == value) return;
            numCargo = value;
            OnVariableChange?.Invoke(numCargo);
        }
    }

    public delegate void OnVariableChangeDelegate(int num);
    public event OnVariableChangeDelegate OnVariableChange;

    void Start()
    {
        numCargo = int.Parse(text.text);
        OnVariableChange += VariableChangeHandler;
    }

    private void VariableChangeHandler(int num)
    {
        numCargo = num;
        text.text = num.ToString();
    }
}
