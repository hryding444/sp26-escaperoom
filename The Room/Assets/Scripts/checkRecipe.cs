using NUnit.Framework;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using System.Net.Sockets;

public class checkRecipe : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject finalDish;
    public int num_dishes = 3;
    public List<List<String>> recipeList = new List<List<String>>();
    List<Dictionary<string, string>> ingredientsList;

    private void Start()
    {
        finalDish.SetActive(false);
        recipeList = new List<List<String>>() {
            new List<string>() {"Carrot", "Tomato"},
            new List<string>() {"Cheese", "Dough"},
            new List<string>() {"Carrot", "Tomato"},

        };
        ingredientsList = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>() { {"Garnish", ""}, {"Main", ""} },
            new Dictionary<string, string>() { {"Garnish", ""}, {"Main", ""} },
            new Dictionary<string, string>() { {"Garnish", ""}, {"Main", ""} }
        };
    }
    public void addIngredient(int dish_num, string socket, string ingredient)
    {
        ingredientsList[dish_num][socket] = ingredient;
        checkRecipeTruth(dish_num);
    }

    public void removeIngredient(int dish_num, string socket, string ingredient)
    {
        ingredientsList[dish_num][socket] = "";
        checkRecipeTruth(dish_num);
    }

    private void checkRecipeTruth(int dish_num)
    {
        Dictionary<string, string> dict = ingredientsList[dish_num];
        if ((dict["Garnish"] == recipeList[dish_num][0]) && (dict["Main"] == recipeList[dish_num][1])) {
            finalDish.SetActive(true);
        }
    }
}
