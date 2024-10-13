using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO _kitchenObjectSO;
    [SerializeField]
    private Transform _counterTopPoint;
   public void Interact()
    {
        Debug.Log("Interect");
        Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.Prefeb, _counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().ObjectName);
    }
}
