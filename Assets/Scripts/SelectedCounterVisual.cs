using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField]
    private BaseCounter _baseCounter;
    [SerializeField]
    private GameObject[] _visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedConuterChanged += Player_OnSelectedConuterChanged;
    }

    private void Player_OnSelectedConuterChanged(object sender, Player.OnSelectedConuterChangedEventArgs e)
    {
        if (e.SelectedCounter == _baseCounter)
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
        foreach (GameObject _visualGameObject in _visualGameObjectArray)
        {
            _visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject _visualGameObject in _visualGameObjectArray)
        {
            _visualGameObject.SetActive(false);
        }
    }
}
