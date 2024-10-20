using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO _kitchenObjectSO;
    [SerializeField]
    private Transform _counterTopPoint;

    private KitchenObject _kitchenObject;
   public void Interact()
    {
        if (_kitchenObject == null)
        {
            Debug.Log("Interect");
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.Prefeb, _counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            
            _kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            _kitchenObject.SetClearCounter(this);
        }
        else
        {
            Debug.Log("Has Object");
        }
        }
}
