using System;
using System.Collections.Generic;

namespace Implementation
{

    public class Knapsack
    {

        public void Process()
        {

            int budget = 500;

            var items = new List<Item>()
            {
                new Item("Req", 100, 280, 1),
                new Item("Req2", 150, 260, 3),
                //new Item("Beer", 52, 10, 12),
                //new Item("Book", 30, 10, 2),
                //new Item("Camera", 32, 30, 1),
                //new Item("Cheese", 23, 30, 4),
                //new Item("Chocolate Bar", 15, 60, 10),
                //new Item("Compass", 13, 35, 1),
                //new Item("Jeans", 48, 10, 1),
                //new Item("Map", 9, 150, 1),
                //new Item("Notebook", 22, 80, 1),
                //new Item("Sandwich", 50, 160, 4),
                //new Item("Ski Jacket", 43, 75, 1),
                //new Item("Ski Pants", 42, 70, 1),
                //new Item("Socks", 4, 50, 2),
                //new Item("Sunglasses", 7, 20, 1),
                //new Item("Suntan Lotion", 11, 70, 1),
                //new Item("T-Shirt", 24, 15, 1),
                //new Item("Tin", 68, 45, 1),
                //new Item("Towel", 18, 12, 1),
                //new Item("Umbrella", 73, 40, 1),
                //new Item("Water", 153, 200, 1)
            };

            ItemCollection[] ic = new ItemCollection[budget + 1];

            for (int i = 0; i <= budget; i++) ic[i] = new ItemCollection();

            for (int i = 0; i < items.Count; i++)
                for (int j = budget; j >= 0; j--)
                    if (j >= items[i].Cost)
                    {
                        int quantity = Math.Min(items[i].Quantity, j/items[i].Cost);
                        for (int k = 1; k <= quantity; k++)
                        {
                            ItemCollection lighterCollection = ic[j - k*items[i].Cost];
                            int testValue = lighterCollection.TotalValue + k*items[i].Value;
                            if (testValue > ic[j].TotalValue) (ic[j] = lighterCollection.Copy()).AddItem(items[i], k);
                        }
                    }

            Console.WriteLine("Knapsack Capacity: " + budget + "\n" + 
                              "Filled Cost: " + ic[budget].TotalCost + "\n" + 
                              "Filled Value: " + ic[budget].TotalValue + "\n" + 
                              "Contents:\n");

            foreach (KeyValuePair<string, int> kvp in ic[budget].Contents)
                Console.Write("" + kvp.Key + " (" + kvp.Value + ")\n");

        }

        private class Item
        {

            public string Description;
            public int Cost;
            public int Value;
            public int Quantity;

            public Item(string description, int cost, int value, int quantity)
            {
                Description = description;
                Cost = cost;
                Value = value;
                Quantity = quantity;
            }

        }

        private class ItemCollection
        {

            public Dictionary<string, int> Contents = new Dictionary<string, int>();
            public int TotalValue;
            public int TotalCost;

            public void AddItem(Item item, int quantity)
            {
                if (Contents.ContainsKey(item.Description)) Contents[item.Description] += quantity;
                else Contents[item.Description] = quantity;
                TotalValue += quantity*item.Value;
                TotalCost += quantity*item.Cost;
            }

            public ItemCollection Copy()
            {
                var ic = new ItemCollection();
                ic.Contents = new Dictionary<string, int>(this.Contents);
                ic.TotalValue = this.TotalValue;
                ic.TotalCost = this.TotalCost;
                return ic;
            }

        }

    }
}