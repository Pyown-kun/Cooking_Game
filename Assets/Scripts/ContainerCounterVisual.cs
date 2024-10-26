using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter _containerCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _containerCounter.OnPlayerGrabbedObject += Contaner_OnPlayerGrabbedObject;
    }

    private void Contaner_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger("OpenClose");
    }
}
