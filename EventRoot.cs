using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public enum TYPE
    {
        NONE = -1,
        SHIP = 0,
        NUM
    };
};

public class EventRoot : MonoBehaviour
{

    public Event.TYPE getEventType(GameObject event_go)
    {
        Event.TYPE type = Event.TYPE.NONE;

        if (event_go != null)
        {
            if (event_go.tag == "SHIP")
            {
                type = Event.TYPE.SHIP;
            }
        }
        return (type);
    }

    public bool isEventIgnitable(Item.TYPE carried_item, GameObject event_go)
    {
        bool ret = false;
        Event.TYPE type = Event.TYPE.NONE;

        if (event_go != null)
        {
            type = this.getEventType(event_go);
        }

        switch (type)
        {
            case Event.TYPE.SHIP:
                if (carried_item == Item.TYPE.TREE)
                {
                    ret = true;
                }
                if (carried_item == Item.TYPE.CORN)
                {
                    ret = true;
                }
                break;
        }
        return (ret);
    }

    public string getIgnitableMessage(GameObject event_go)
    {
        string message = "";
        Event.TYPE type = Event.TYPE.NONE;
        if (event_go != null)
        {
            type = this.getEventType(event_go);
        }

        switch (type)
        {
            case Event.TYPE.SHIP:
                message = "수리합니다";
                break;
        }
        return (message);
    }

}

