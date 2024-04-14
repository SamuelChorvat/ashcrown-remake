﻿namespace Ashcrown.Remake.Core.Ability.Extensions;

public static class AbilityDescriptionExtensions
{
    public static string HighlightInYellow(this string input)
    {
        return $"<color=yellow>{input}</color>";
    }
    
    public static string HighlightInPurple(this string input)
    {
        return $"<color=#FF00CD>{input}</color>";
    }
    
    public static string HighlightInGold(this string input)
    {
        return $"<color=#EAB65B>{input}</color>";
    }
    
    public static string HighlightInOrange(this string input)
    {
        return $"<color=#FF5C04>{input}</color>";
    }
    
    public static string HighlightInGreen(this string input)
    {
        return $"<color=#1AB000>{input}</color>";
    }
}