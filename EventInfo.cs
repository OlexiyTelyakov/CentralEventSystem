using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KiwiKaleidoscope 
{
    public abstract class EventInfo 
    {
        //Some Info
        //Considering the degree to which the different events rely on different information, it will be inherited by classes.
        //As opposed to carrying any information in of itself, unless there is a need for a very very basic event.
    }

    public class InventoryEventInfo : EventInfo 
    {
        public bool enabledUI;
        public bool itemDropped;
        public List<InventorySlot> inventory;
        public Item changedItem;

        public InventoryEventInfo(List<InventorySlot> inventory, bool enabledUI, bool itemDropped, Item changedItem)
        {
            this.enabledUI = enabledUI;
            this.inventory = inventory;
            this.itemDropped = itemDropped;
            this.changedItem = changedItem;
        }
    }

    public class EquipItemInfo : EventInfo
    {
        public Item equippedItem;

        public EquipItemInfo(Item equippedItem)
        {
            this.equippedItem = equippedItem;
        }
    }

    public class WaterCanEventInfo : EventInfo
    {
        public float waterAmount;
        public float waterCapacity;

        public WaterCanEventInfo(float waterAmount, float waterCapacity)
        {
            this.waterAmount = waterAmount;
            this.waterCapacity = waterCapacity;
        }
    }

    public class ItemUsedInfo : EventInfo
    {
        public Item usedItem;
        public TileGrid.Tile useTarget;

        public ItemUsedInfo(Item usedItem, TileGrid.Tile useTarget)
        {
            this.usedItem = usedItem;
            this.useTarget = useTarget;
        }
    }

    public class AudioEffectInfo : EventInfo
    {
        public AudioClip clip;
        public float volume;

        public AudioEffectInfo(AudioClip clip, float volume = 1)
        {
            this.clip = clip;
            this.volume = volume;
        }
    }

    public class ShopEventInfo : EventInfo 
    {
        public IShop shop;
        public Item item;
        public bool soldFromShop;
        public int itemQuantity;

        public ShopEventInfo(IShop shop, Item item, bool soldFromShop, int itemQuantity)
        {
            this.shop = shop;
            this.item = item;
            this.soldFromShop = soldFromShop;
            this.itemQuantity = itemQuantity;
        }
    }

    public class TimeEventInfo : EventInfo
    {
        public bool dayOver;
        public int dayCount;
        public int timeOfDay;
        public bool fromBed;

        //Season change would go here.

        public TimeEventInfo(bool dayOver, int dayCount, int timeOfDay, bool fromBed)
        {
            this.dayOver = dayOver;
            this.dayCount = dayCount;
            this.timeOfDay = timeOfDay;
            this.fromBed = fromBed;
        }
    }

    public class RoomTraversalEvent : EventInfo 
    {
        public Room destination;
        public string traverserName;

        public RoomTraversalEvent(string traverserName, Room destination)
        {
            this.traverserName = traverserName;
            this.destination = destination;
        }
    }

    public class KillzoneEvent: EventInfo
    {
        //This is a notification that doesn't require any information.
    }

    public class PestKillEvent : EventInfo
    {
        //This is a notification that doesn't require any information.
    }
}


