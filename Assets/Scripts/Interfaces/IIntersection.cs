using System.Collections.Generic;

namespace maross3
{
    // might implement interface, might extend base class. Finishing functionality first
    public interface IIntersection
    {
        public abstract Sequence CurrentSequence { set; get; }
        public abstract Dictionary<int, ITrafficSignal> TrafficSignalsByCorridor { set; get; }
    }
}