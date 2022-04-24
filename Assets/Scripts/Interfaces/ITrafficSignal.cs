namespace maross3
{
    // might implement interface, might extend base class. Finishing functionality first
    public interface ITrafficSignal
    {
        public abstract void ChangeState(Sequence seq);
        public abstract int CorridorNumber { get; set; }
        public abstract SignalState CurrentSignalState { set; get; }
    }
}