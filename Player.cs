using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp1
{
    // CREATE a Player object has a Name, Score, Hand of Card objects and DISPLAYs their Name and Score and possibly their hand
    class Player
    {
        public Player(string Name, int Score){
            this.Name = Name;
            this.Score = Score;
        }

        public string Name { get; set; }
        public int Score { get; set; }
        public ArrayList Hand = new ArrayList();

        public void insertCard(Card c){
            this.Hand.Add(c);
        }

        public void removeCard(Card c){
            this.Hand.Remove(c);
        }

        public int calculateScore(){
            this.Score = 0;
            foreach(Card card in this.Hand)
                this.Score += card.Value;
            return this.Score;
        }

        public string getHand(){
            string handString = "";
            foreach(Card c in this.Hand)
                handString += c;
            return handString;
        }

        public override string ToString(){
            return $"{this.Name}'s score is {this.Score}\n{getHand()}";
        }
    }
}