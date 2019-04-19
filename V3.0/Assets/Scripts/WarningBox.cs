using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningBox : MonoBehaviour
{
    [SerializeField] public GameObject objectController;

    void OnTriggerEnter2D(Collider2D col)
    {
        objectController.GetComponent<objectController>().ingredientMissed(col.gameObject.name.Substring(0, col.gameObject.name.IndexOf('(')));
        Destroy(col.gameObject);
    }
}
