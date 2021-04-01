using CardDeck;
using System;
using System.Collections.Generic;
using System.Linq;



namespace _3Card
{
    public static class Extensions
    {
        public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator) => enumerator;
    }

    class Play
    {

        public int bet;
        public int score;
        public int rank;
        public int value;

        public int CheckRank(string stringCardRank)
        {
            if (stringCardRank == "Ace")
            { int rank = 14;
                return rank;
            }
            if (stringCardRank == "King")
            { int rank = 13;
                return rank;
            }
            if (stringCardRank == "Queen")
            { int rank = 12;
                return rank;
            }
            if (stringCardRank == "Jack")
            { int rank = 11;
                return rank;
            }
            if (stringCardRank == "Ten")
            {
                int rank = 10;
                return rank;
            }
            if (stringCardRank == "Nine")
            {
                int rank = 9;
                return rank;
            }
            if (stringCardRank == "Eight")
            {
                int rank = 8;
                return rank;
            }
            if (stringCardRank == "Seven")
            {
                int rank = 7;
                return rank;
            }
            if (stringCardRank == "Six")
            {
                int rank = 6;
                return rank;
            }
            if (stringCardRank == "Five")
            {
                int rank = 5;
                return rank;
            }
            if (stringCardRank == "Four")
            {
                int rank = 4;
                return rank;
            }
            if (stringCardRank == "Three")
            {
                int rank = 3;
                return rank;
            }
            if (stringCardRank == "Two")
            {
                int rank = 2;
                return rank;
            }

            return rank;

        }

        public int[] FetchRanks(IEnumerable<Card> cards)
        {
            // Deck doesnt return the rank as int (unfortunate..)
            // Therefore..
            // Fetch the string
            string stringCard1Rank = cards.ElementAt(0).CardNumber.ToString();
            string stringCard2Rank = cards.ElementAt(1).CardNumber.ToString();
            string stringCard3Rank = cards.ElementAt(2).CardNumber.ToString();

            // Convert to int
            int rankCard1 = CheckRank(stringCard1Rank);
            int rankCard2 = CheckRank(stringCard2Rank);
            int rankCard3 = CheckRank(stringCard3Rank);

            // Put ranks into a sorted array to allow for straight equation
            int[] ranks = { rankCard1, rankCard2, rankCard3 };
            Array.Sort(ranks);
            return ranks;
        }
        public int Score(IEnumerable<Card> cards)
        {
            // Fetch ranks through method to prevent repetition- (cant measure high card against eachother within this scope)
            int[] ranks = FetchRanks(cards);

            // Fetch the suits
            string suitCard1 = cards.ElementAt(0).Suit.ToString();
            string suitCard2 = cards.ElementAt(1).Suit.ToString();
            string suitCard3 = cards.ElementAt(2).Suit.ToString();

            // This is stupid but quicker to write.. Change this
            string s1 = suitCard1;
            string s2 = suitCard2;
            string s3 = suitCard3;

            // Base score :
            // Straight flush - 100 000 + highest rank
            // Three of a kind - 50 000 + rank
            // Straight - 25000 + highest card
            // Flush - 10000 + highest card
            // Pair - 1000 + 100 * pair rank + kicker

            // If the hand is a pair or three of a kind, it cannot be either straight or a flush therefore we check these first
            if (ranks[0] == ranks[1] || ranks[0] == ranks[2] || ranks[1] == ranks[2])
            {
                // If theres a pair, check if theres a three of a kind
                if (ranks[0] == ranks[1] && ranks[1] == ranks[2])
                {
                    // Three of a kind can only be beaten by straight flush
                    // Let three of a kind have a base score of 50 000, then add the value of one card
                    score = 50000;
                    score += ranks[0] * 1000;
                    Console.WriteLine($"Hand is three of a kind, score : {score}");
                    return score;
                };
                // If theres no three of a kind check which cards makes the pair
                // Let pair have a base score of 1000, then add the pair * 100 + kicker
                
                if (ranks[0] == ranks[1])
                {
                    score = 1000 + 100 * ranks[0] + ranks[2];
                    Console.WriteLine($"First two cards makes the pair, score is {score}");
                    return score;
                }
                if (ranks[0] == ranks[2])
                {
                    
                    score = 1000 + 100 * ranks[0] + ranks[1];
                    Console.WriteLine($"First and last card makes the pair, score is {score}");
                    return score;
                }
                if (ranks[1] == ranks[2])
                {
                   
                    score = 1000 + 100 * ranks[2] + ranks[0];
                    Console.WriteLine($"Last two cards makes the pair, score is {score}");
                    return score;
                }
            }
            //  If there is no three of a kind or pairs
            //  Check if user has a straight
            else if (
                // If user has any straight except ACE-TWO-THREE
                ranks[0] + 1 == ranks[1] && ranks[1] + 1 == ranks[2]
                ||
                // If user has an ACE-TWO-THREE straight
                ranks[2] - ranks[0] == 12 && ranks[2] - ranks[1] == 11
                )
            {   
                // If the hand is a straight it could still be a flush
                if (s1 == s2 && s1 == s3)
                {
                    // Let straight flush have a base score of 100 000, then add the highest rank 
                    score = 100000 + ranks[2];
                    Console.WriteLine($"Hand is a straight flush! score : {score}");
                    return score;
                }
                else
                {   
                    // 25 000 + highest card
                    score = 25000 + ranks[2];
                    Console.WriteLine($"Hand is a straight, score : {score}");
                    return score;
                }
            }
            // If the hand is not a straight 
            if (s1 == s2 && s1 == s3)
            {
                // Hand is a flush
                // Let flush have a base score of 100, then add the rank of the highest card
                score = 10000 + ranks[2];
                Console.WriteLine($"Hand is a flush, score : {score}");
                this.value += 500;
            }
            else {
                // We've concluded there are no pairs or better therefore there could only be highest card
                // If the dealer doesnt show a Queen or higher, the dealer doesnt play
                // Issue- Highest card hands cant be measured within this method..
                score = 0;
                return score;
            }
            return score;
        }
     
        public int Call(IEnumerable<Card> userCards, IEnumerable<Card> dealerCards)
        {
            // Display and compare dealer and user cards

            Console.WriteLine("User shows : ");

            foreach (var item in userCards)
            {
                Console.WriteLine($"{item.CardNumber} of {item.Suit}");
            }

            Console.WriteLine("Dealer shows : ");

            foreach (var item in dealerCards)
            {
                Console.WriteLine($"{item.CardNumber} of {item.Suit}");
            }

            // Calculate hand score

            int userScore = Score(userCards);
            int dealerScore = Score(dealerCards);

            // Declare payout, subtract the player bet from the start 
            int payout = 0;
            // Declare win boolean
            bool userWins = false;

            // Since we cant measure highest card within the score function as only one hand can be used at one time
            // Fetch the card ranks 
            int[] dealerCardRanks = FetchRanks(dealerCards);
            int[] userCardRanks = FetchRanks(userCards);

            // And the dealer only play hands that are better than Queen high or better
            Array.Reverse(dealerCardRanks);
            Array.Reverse(userCardRanks);

            // Check if dealer plays the hand (only plays queen high or better)
            if (dealerScore == 0 && dealerCardRanks[0] < 12)
            {
                Console.WriteLine("Dealer doesnt play");
                payout = payout - this.bet;
                userWins = true;
            }
           
            else if (userScore > dealerScore && dealerCardRanks[0] >= 12)
            {
                // Dealer plays the hand and user wins
                userWins = true;
                Console.WriteLine(userWins);
            }
            // Check if neither dealer or player had pair or better but if dealer still plays the hand
            else if (userScore == dealerScore && dealerCardRanks[0] >= 12)
            {
                // Compare user and dealer highest cards heirarchy
                Console.WriteLine(""); 
                if (userCardRanks[0] > dealerCardRanks[0])
                {
                    userWins = true;
                    Console.WriteLine("User wins with high card");
                }
                else if (userCardRanks[0] < dealerCardRanks[0])
                {
                    Console.WriteLine("Dealer wins with high card");
                    userWins = false;
                }
                // If highest rank is the same check second rank
                else if (userCardRanks[0] == dealerCardRanks[0])
                {
                    if (userCardRanks[1] > dealerCardRanks[1])
                    {
                        Console.WriteLine("User wins with high card");
                        userWins = true;
                    }
                    else if (userCardRanks[1] < dealerCardRanks[1])
                    {
                        Console.WriteLine("Dealer wins with high card");
                        userWins = false;
                    }
                    // If second rank is also the same check last card
                    else if (userCardRanks[1] == dealerCardRanks[1])
                    {
                        if (userCardRanks[2] > dealerCardRanks[2])
                        {
                            Console.WriteLine("User wins with high card");
                            userWins = true;
                        }
                        else if (userCardRanks[2] < dealerCardRanks[2])
                        {
                            Console.WriteLine("Dealer wins with high card");
                            userWins = false;
                        }
                    }
                }
            }

            if (userScore >= 25000 && userScore < 50000)
            {
                payout = this.bet * 2;
                Console.WriteLine($" User has a straight. User awarded their bet x 1 : {payout}");
                return payout;
            }
            if (userScore >= 50000 && userScore < 100000)
            {
                payout = this.bet * 4;
                Console.WriteLine($" User has Three of a kind. User awarded their bet x 4 : {payout}");
                return payout;
            }
            if (userScore >= 100000)
            {
                payout = this.bet * 5;
                Console.WriteLine($"User has a straight flush. User awarded their bet x 5 : {payout}");
                return payout;
            }
            if (userWins)
            {
                Console.WriteLine("User wins the hand!");
                payout = payout + this.bet * 2;
            }
            if (!userWins)
            {
                Console.WriteLine("Dealer wins the hand!");
                payout = payout - this.bet;
            }
            return payout;
            
        }
        public int PlayHand (int bet)
        {

            this.bet = bet;
            int payout = 0 - bet ;
          

            // Fetch deck, shuffle and draw user cards
            var deck = new Deck();
            deck.Shuffle();
            IEnumerable<Card> userCards = deck.TakeCards(3);
            IEnumerable<Card> dealerCards = deck.TakeCards(3);


            // Display user card
            Console.WriteLine("User recieves : ");
            foreach (var item in userCards)
            {
                Console.WriteLine($"{item.CardNumber} of {item.Suit}");
            }
            Console.WriteLine(" ");

            // Let user choose to continue or not, (call or fold)

            Console.WriteLine($"{this.bet} to call.");
            Console.WriteLine(" ");
            Console.WriteLine($"1: Call");
            Console.WriteLine($"2: Fold");
            int.TryParse(Console.ReadLine(), out int callOrFoldValue);

            if (callOrFoldValue == 2)
            {
                payout = 0;
                Console.WriteLine("User folds their hand");
              
            }else
            {
                // Play the hand by calling and return payout
                payout = payout - this.bet;
                payout = Call(userCards, dealerCards);
                return payout;
            }
            return payout;
        }
    }
    class User 
    {
        public string name;
        public int balance;

        public User(string name)
        {
            this.name = name;
        }
        public void Balance(int balance)
        {
            this.balance = balance;
        }
        public void PlaceBet(int bet)
        {
            if (bet > this.balance)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Insufficient funds");
                Console.WriteLine(" ");
            }
            else
            {
                Console.WriteLine(" ");
                Console.WriteLine($"{this.name} bet {bet}. Shuffling deck..");
                this.balance -= bet;
               
                var hand = new Play();
                int result = hand.PlayHand(bet);
                this.balance += result;
               
            }
        }

        public void Deposit(int value)
        {
            this.balance += value;
            Console.WriteLine($"{value} has been added to your balance. Current balance : {this.balance}");
            return;
        }
        public void DisplayBalance()
        {
            Console.WriteLine($"Current balance : {this.balance}");
        }
    
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to three card poker!");
            Console.WriteLine("Hi! State your name please.");
       
            var name = Console.ReadLine();
            
            if (name != null)
            {
                var user = new User(name);
              
                while (true)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("This is a menu. You now have three options. Type number and press enter.");
                    Console.WriteLine("1: Play a hand");
                    Console.WriteLine("2: Display balance");
                    Console.WriteLine("3: Make a deposit");
                    Console.WriteLine("4: Exit the game");
                    Console.WriteLine(" ");
                    int.TryParse(Console.ReadLine(), out int option);
                    if (option == 1)
                    {
                        Console.WriteLine("Option 1 - Lets start the show");
                        Console.WriteLine("Place a bet by entering the amount you want to play for:");
                        Console.WriteLine(" ");
                        int.TryParse(Console.ReadLine(), out int bet);
                        user.PlaceBet(bet);
                    }
                    if (option == 2)
                    {
                        Console.WriteLine("Option 2 - display balance");
                        user.DisplayBalance();
                        continue;
                    }
                    if (option == 3)
                    {
                        Console.WriteLine("Option 3 - Enter the amount you wish to deposit");
                        int.TryParse(Console.ReadLine(), out int value);
                        user.Deposit(value);
                        
                    }
                    if (option == 4)
                    {
                        Console.WriteLine("Option 4 - Press Enter key to exit the game");
                        break;
                    }

                }
            };

            Console.ReadKey();
        }
    }
}
