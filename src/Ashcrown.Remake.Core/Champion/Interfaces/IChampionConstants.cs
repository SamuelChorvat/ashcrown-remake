namespace Ashcrown.Remake.Core.Champion.Interfaces;

public interface IChampionConstants
{
    static abstract string Name { get; }
    static abstract string Title { get; }
    static abstract string Bio { get; }
    static abstract string Artist { get; }
    static abstract int[] Attributes { get; }
}