using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PruebasLambda : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Action saludar = () => Debug.Log("Hola!");
        saludar(); // Hola!

        Action<string> imprimir = s => { Debug.Log(s); };
        imprimir("Adiós!"); // Adiós!

        Func<int, int> cuadrado = num => num * num;
        var resul = cuadrado(5);
        Debug.Log(resul);

        //Action funcionBasica = () => Debug.Log("HOLA");
        //var funcionBasica = Function;
        transform.DOMove(new Vector3(5, 0, -5), 5).SetEase(Ease.OutCirc).SetLoops(3, LoopType.Yoyo);
        //transform.DOShakeScale(7).OnComplete(() => Debug.Log("He terminado el shake"));
    }

    void Function()
    {

    }

}
