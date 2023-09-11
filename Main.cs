using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
// IMPLEMENT the using statement for ArrayList
using System.Collections;

namespace ConsoleApp1
{
    class Program
    {
        // CREATE an ArrayList for the deck of cards
        private static ArrayList deck = new ArrayList();

        // CREATE a random object
        private static Random rand = new Random();

        // CREATE player objects
        private static Player player;
        private static Player dealer;

        // CREATE array of card names
        public static String[] cardNames = {"AC ", "2C ", "3C ", "4C ", "5C ", "6C ", "7C ", "8C ", "9C ", "10C", "JC ", "QC ", "KC ",
            "AS ", "2S ", "3S ", "4S ", "5S ", "6S ", "7S ", "8S ", "9S ", "10S", "JS ", "QS ", "KS ",
            "AD ", "2D ", "3D ", "4D ", "5D ", "6D ", "7D ", "8D ", "9D ", "10D", "JD ", "QD ", "KD ",
            "AH ", "2H ", "3H ", "4H ", "5H ", "6H ", "7H ", "8H ", "9H ", "10H", "JH ", "QH ", "KH ", "***"};
 
        // CREATE array of card values
        public static int[] cardValues = {11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10,
            11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10,
            11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10,
            11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 0};

        public static void createDeck(){
            deck.Clear();
            for(int i = 0; i < cardNames.Length - 1; i++)
                deck.Add(new Card(cardNames[i], cardValues[i]));
            
            Card tempCard = new Card();
            for(int i = deck.Count - 1; i >= 0; i--)
            {
                int number = rand.Next(i);
                tempCard = (Card)deck[i];
                deck[i] = deck[number];
                deck[number] = tempCard;
            }
        }

        public static Card getCardFromDeck(){
            Card tempCard = (Card)deck[0];
            deck.Remove(tempCard);
            return tempCard;
        }

        public static bool checkAce(string name){
            if(name.Equals("Player")){
                foreach(Card card in player.Hand)
                    if(card.Value == 11){
                        card.Value = 1;
                        return false;
                    }
                return true; // Returns true if player has busted
            }
            else{
                foreach(Card card in dealer.Hand)
                    if(card.Value == 11){
                        card.Value = 1;
                        return false;
                    }
                return true; // Returns true if dealer has busted
            }
        }

        static void Main(string[] args)
        {
            // START game
            while(true){
                // CREATE a new deck of Card objects AND SHUFFLE the deck
                createDeck();

                // CREATE a Player and a Dealer
                player = new Player("Player", 0);
                dealer = new Player("Dealer", 0);

                // ASK the Player IF they would like to play
                Console.WriteLine("Would you like to play Blackjack? [y/n]");
                string response = Console.ReadLine().Trim().ToLower();

                // IF the Player does want to play THEN Deal cards (1 to Player face up, 1 to Dealer face up, 1 to Player face up, and 1 to Dealer face down)
                if(response.Equals("y")){            
                    // CREATE the Deal Card method which will take the next card off the top of the deck AND add it to each player's hand AND remove that card from the deck
                    
                    player.insertCard(getCardFromDeck());
                    dealer.insertCard(getCardFromDeck());
                    player.insertCard(getCardFromDeck());
                    Card blankCard = new Card(cardNames[52], cardValues[52]);
                    dealer.insertCard(blankCard); // INSERT blank card
                    Console.WriteLine($"Player's hand is:\n {player.getHand()}");
                    Console.WriteLine($"Dealer's hand is:\n{dealer.getHand()}");
                    Console.WriteLine($"Player's current score is {player.calculateScore()}");
                
                    // CREATE a checkAce method to DETERMINE IF the score is GREATER THAN 21 AND IF the Player has an ace to reduce their score.
                    if(player.calculateScore() > 21 && checkAce(player.Name)){
                        Console.WriteLine("Player has busted!");
                        // WHEN the Dealer has a turn THEN (Remove blank card) Deal a card to them
                        // IF the Player has busted THEN Dealer automatically wins AND End the Round
                        // IF the Player has not busted THEN checkAce AND checkScore
                        // WHILE the Dealer has LESS THAN 17 points Deal a card, checkAce, checkScore
                    }
                    else{
                        // CREATE a checkScore method to DETERMINE IF the player has a blackjack AND IF true THEN automatically Stand.
                        if(player.calculateScore() == 21){
                            Console.WriteLine("Player has a Blackjack!");
                        }
                        else{
                            // ASK the Player if they want to Hit UNTIL they Stand OR Bust
                        
                            while(response != ("s")){
                                back:
                                Console.WriteLine("Would you like to Hit or Stand? [h/s]");
                                response = Console.ReadLine().Trim().ToLower();
                                if(response != "s" && response != "h")
                                {
                                    Console.WriteLine("Please enter an h or s");
                                    goto back;
                                }
                                    if(response == "s")
                                        break;
                                    player.insertCard(getCardFromDeck());
                                    Console.WriteLine(player.getHand());
                                    if(player.calculateScore() > 21 && checkAce(player.Name)){
                                        Console.WriteLine("Player has busted!");
                                        Console.WriteLine($"Player score is {player.calculateScore()}");
                                        break;
                                }
                                Console.WriteLine($"Player's current score is {player.calculateScore()}");
                                Console.WriteLine($"Dealer's current score is {dealer.calculateScore()}");
                            }
                            // IF the Player Stands OR Bust THEN Dealer's turn
                            Console.WriteLine(dealer.getHand());
                            //<-- Remember that the dealer does not draw an additional card if the player busted
                            while(dealer.calculateScore() < 16)
                            {
                                dealer.removeCard(blankCard);
                                dealer.insertCard(getCardFromDeck());
                                Console.WriteLine(dealer.getHand());
                                if(player.calculateScore() > 21){
                                    Console.WriteLine("Dealer has won!");
                                    break;
                                }
                                if(dealer.calculateScore() > 21 && checkAce(dealer.Name) && player.calculateScore() < 21){
                                    Console.WriteLine("Dealer has busted!");
                                    Console.WriteLine($"Dealer's  score is {dealer.calculateScore()}");
                                    break;
                                }
                                if(player.calculateScore() > 21){
                                    break;
                                }
                                if(dealer.calculateScore() > 16)
                                {
                                    Console.WriteLine($"Dealer's score is {dealer.calculateScore()}");
                                    break;
                                }
                                Console.WriteLine($"Dealer's current score is {dealer.calculateScore()}");
                            } 
                            // IF the Player Hits THEN deal a card AND checkAce AND checkScore
                        }
                    }

                    // DETERMINE the winner AND DISPLAY results
                    int playerScore = player.calculateScore();
                    int dealerScore = dealer.calculateScore();
                    if(playerScore == dealerScore && playerScore < 21)
                        Console.WriteLine("It is a tie.");
                    else if(playerScore <= 21 && playerScore > dealerScore)
                        Console.WriteLine("Player wins!");
                    else
                        Console.WriteLine("Dealer wins!");

                }
                // IF the Player doesn't want to play THEN Thank them for playing AND EXIT the program
                else if(response.Equals("n")){
                    Console.WriteLine("Thanks for playing!");
                    break;
                }
                else{
                    Console.WriteLine("Please enter y or n");
                }
            }
        }
    }
}
