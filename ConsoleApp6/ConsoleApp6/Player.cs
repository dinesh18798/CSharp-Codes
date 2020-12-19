using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class Player
    {
        private List<string> inventory = new List<string>();
        public Player(List<string> items)
        {
            inventory = items;

            NonStaticClass.
        }
        public List<string> GetItems()
        {
            return new List<string>(inventory);
        }
    }
  
}
