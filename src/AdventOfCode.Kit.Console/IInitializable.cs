namespace AdventOfCode.Kit.Console.Core
{
    internal interface IInitializable<TCallbackParameter>
    {
        internal Task Initialize(Action<TCallbackParameter> onInitialized);
    }
}
