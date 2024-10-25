using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO _kitchenObjectSO;

    private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() { 
        return _kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent _kitchenObjectParent)
    {
        if (this._kitchenObjectParent != null)
        {
            this._kitchenObjectParent.ClearKitchenObject();
        }

        this._kitchenObjectParent = _kitchenObjectParent;

        if (_kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter alredy has a KitchenObject");
        }

        _kitchenObjectParent.SetKitchenObject(this);

        transform.parent = _kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return _kitchenObjectParent;
    }
}
