using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardEnemy : MoveForward
{
    private WaitForSeconds _changeDirectionDelay = new WaitForSeconds(3f);

    private void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            yield return _changeDirectionDelay;
            _transform.Rotate(0, Random.Range(-90f, 90f), 0);
        }
    }
}