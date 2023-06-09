using System.Collections.Concurrent;

namespace SyncLink.Server.SignalR.TextPlotGame;

public interface ITimerEventQueue
{
    void QueueTimerEvent(TimerEvent timerEvent);
    TimerEvent? Dequeue();
}

public class TimerEventQueue : ITimerEventQueue
{
    private readonly ConcurrentQueue<TimerEvent> _events = new ConcurrentQueue<TimerEvent>();

    public void QueueTimerEvent(TimerEvent timerEvent)
    {
        _events.Enqueue(timerEvent);
    }

    public TimerEvent? Dequeue()
    {
        _events.TryDequeue(out var timerEvent);
        return timerEvent;
    }
}

public class TimerEvent
{
    public string TimerId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public bool IsCancelled { get; set; }
}
