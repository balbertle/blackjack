using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // CREATE a Card object that has a Name and Value and DISPLAYs the card as ASCII art
    class Card
    {
        public Card(){
            
        }
        public Card(string Name, int Value){
            this.Name = Name;
            this.Value = Value;
        }

        public string Name { get; set; }
        public int Value { get; set; }

        public override string ToString(){
            return $".-----.\n| {this.Name} |\n|     |\n'-----'\n";
        }
    }
}