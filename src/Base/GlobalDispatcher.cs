namespace GMessage
{
    public sealed class GlobalDispatcher : Dispatcher
    {
        public static GlobalDispatcher Instance => Instance<GlobalDispatcher>(GLOBAL) ?? Add<GlobalDispatcher>(GLOBAL);

        public override IDispatcher GetDispatcher() => null; }
}
