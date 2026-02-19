using NUnit.Framework;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using System.Net.Sockets;
using System.Globalization;

public class checkRecipe : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] finalDishes;
    public int num_dishes = 3;
    public List<List<String>> recipeList = new List<List<String>>();
    List<Dictionary<string, string>> ingredientsList;
    public GameObject[] dishParts;

    private void Start()
    {
        foreach (GameObject dish in finalDishes)
        {
            dish.SetActive(false);
        }
        recipeList = new List<List<String>>() {
            new List<string>() {"Tomato", "Tortilla"},
            new List<string>() {"Turnip", "Lettuce"},
            new List<string>() {"Chicken", "Bread"},

        };
        ingredientsList = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>() { {"Garnish0", ""}, {"Main0", ""} },
            new Dictionary<string, string>() { {"Garnish1", ""}, {"Main1", ""} },
            new Dictionary<string, string>() { {"Garnish2", ""}, {"Main2", ""} }
        };
    }
    public void addIngredient(int dish_num, string socket, string ingredient)
    {
        string socket_num = socket + (dish_num.ToString());
        ingredientsList[dish_num][socket_num] = ingredient;
        checkRecipeTruth(dish_num);
    }

    public void removeIngredient(int dish_num, string socket, string ingredient)
    {
        string socket_num = socket + (dish_num.ToString());
        ingredientsList[dish_num][socket_num] = "";
        checkRecipeTruth(dish_num);
    }

    private void checkRecipeTruth(int dish_num)
    {
        Dictionary<string, string> dict = ingredientsList[dish_num];
        string garnish = "Garnish" + (dish_num.ToString());
        string main = "Main" + (dish_num.ToString());
        if ((dict[garnish] == recipeList[dish_num][0]) && (dict[main] == recipeList[dish_num][1])) {
            finalDishes[dish_num].SetActive(true);
        }
    }
}
