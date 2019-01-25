using UnityEngine;

public enum EventType
{
    EVENT_NULL,
}

public class Event
{
    public Event(EventType e_type)
    {
        event_type = e_type;
    }

    public EventType Type()
    {
        return event_type;
    }

    /*public class LevelUnload
    {
        public Level to_unload = null;
    }
    public LevelUnload level_unload = new LevelUnload();*/

    private EventType event_type = EventType.EVENT_NULL;
}

