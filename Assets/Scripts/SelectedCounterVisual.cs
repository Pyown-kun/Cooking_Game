using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField]
    private ClearCounter _clearCounter;
    [SerializeField]
    private GameObject _visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedConuterChanged += Player_OnSelectedConuterChanged;
    }

    private void Player_OnSelectedConuterChanged(object sender, Player.OnSelectedConuterChangedEventArgs e)
    {
        if (e.SelectedCounter == _clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        _visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        _visualGameObject.SetActive(false);
    }
}
