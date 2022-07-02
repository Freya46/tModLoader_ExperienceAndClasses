﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

public static class Commons
{
    /// <summary>
    /// Creates and finalizes a recipe. Ingredients must be formatted new int[,] { {id1, num1}, {id2, num2}, ... }. Can build on an existing recipe.
    /// NOTE: Duplicate items in a recipe cause a bug where only one stack is checked/needed. The method below can be used to solve this.
    /// </summary>
    /// <param name="mod"></param>
    /// <param name="ingredients"></param>
    /// <param name="result"></param>
    /// <param name="numResult"></param>
    /// <param name="where"></param>
    
    /**
    public static void QuckRecipe(Mod mod, int[,] ingredients, ModItem result, int numResult = 1, Recipe buildOn = null, ushort where = TileID.WorkBenches)
    {
        //recipe
        Recipe recipe;
        if (buildOn == null) recipe = mod.CreateRecipe();
            else recipe = buildOn;

        //where to craft (use MaxValue to skip)
        if (where != ushort.MaxValue) recipe.AddTile(where);

        //add ingredients
        if (ingredients.GetLength(1) == 2)
        {
            for (int i = 0; i < ingredients.GetLength(0); i++)
            {
                recipe.AddIngredient(ingredients[i, 0], ingredients[i, 1]);
            }
        }

        //result
        recipe.SetResult(result, numResult);tModPorter Pass result to CreateRecipe. 

        //complete
        recipe.Register();
    }
    */

    /// <summary>
    /// Combines duplicate items and checks if the player has enough. Workaround for duplicate item recipe bug.
    /// Returns true if the player has enough of the item.
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    public static bool EnforceDuplicatesInRecipe(Recipe recipe)
    {
        List<int> types = new List<int>();
        List<int> stacks = new List<int>();
        List<Item> ingredients = recipe.requiredItem;
        int ind;

        ingredients.ForEach((ingredient) =>
        {
            ind = types.IndexOf(ingredient.type);
            if (ind >= 0)
            {
                stacks[ind] += ingredient.stack;
            }
            else
            {
                types.Add(ingredient.type);
                stacks.Add(ingredient.stack);
            }
        });

        int count;
        for (int i = 0; i < types.Count; i++)
        {
            count = Main.LocalPlayer.CountItem(types[i], stacks[i]);
            if (count > 0 & count < stacks[i])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Try to get from tag, else default to specified value. Supports int, float, double, bool, long, string, int[], byte[]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tag"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static T TryGet<T>(TagCompound tag, string key, T defaultValue)
    {
        try
        {
            T val;
            Type type = typeof(T);
            if (type == typeof(int)) val = (T)Convert.ChangeType(tag.GetInt(key), type);
            else if (type == typeof(float)) val = (T)Convert.ChangeType(tag.GetFloat(key), type);
            else if (type == typeof(double)) val = (T)Convert.ChangeType(tag.GetDouble(key), type);
            else if (type == typeof(bool)) val = (T)Convert.ChangeType(tag.GetBool(key), type);
            else if (type == typeof(long)) val = (T)Convert.ChangeType(tag.GetLong(key), type);
            else if (type == typeof(string)) val = (T)Convert.ChangeType(tag.GetString(key), type);
            else if (type == typeof(int[])) val = (T)Convert.ChangeType(tag.GetIntArray(key), type);
            else if (type == typeof(byte[])) val = (T)Convert.ChangeType(tag.GetByteArray(key), type);
            else throw new Exception();

            return val;
        }
        catch
        {
            return defaultValue;
        }
    }

    public static IList<T> TryGetList<T>(TagCompound tag, string key)
    {
        try
        {
            List<T> val = (List<T>)Convert.ChangeType(tag.GetList<T>(key), typeof(List<T>));
            return val;
        }
        catch
        {
            return new List<T>();
        }
    }
}