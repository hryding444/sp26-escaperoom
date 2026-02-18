using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

public class checkIngredients : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public XRSocketInteractor soupSocket;
    public int dish_num;
    public checkRecipe checkRecipe;
    private string socket;

    // Update is called once per frame

    private void Start()
    {
        socket = transform.name;
    }

    void OnEnable()
    {
        soupSocket.selectEntered.AddListener(OnSelectEntered);
        soupSocket.selectExited.AddListener(OnSelectExited);
    }

    void OnDisable()
    {
        soupSocket.selectEntered.RemoveListener(OnSelectEntered);
        soupSocket.selectExited.RemoveListener(OnSelectExited);
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        string selected = args.interactableObject.transform.name;
        Debug.Log("Added " + socket + " " + selected);
        checkRecipe.addIngredient(dish_num, socket, selected);
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        string selected = args.interactableObject.transform.name;
        Debug.Log("Removed " + socket + " " + selected);
        checkRecipe.removeIngredient(dish_num, socket, selected);

    }

}
