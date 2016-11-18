using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu {
    public string name;

    public delegate void Execute();
    [System.Serializable]
    public class Item {
        public string name;
        public Transform transform;
        public Execute execute;

        public Item(string name)                      { this.name = name; }
        public Item(string name, Transform transform) { this.name = name; this.transform = transform; }
        public Item()                                 { this.name = ""; }

    }

    public List<Item> elements = new List<Item>();
    public Item element(string name) {
        Item item = new Item("null item");
        for(int i = 0; i < elements.Count; i++){
            if(elements[i].name == name) {
                item = elements[i];
            }
        }
        return item;
    }
}
