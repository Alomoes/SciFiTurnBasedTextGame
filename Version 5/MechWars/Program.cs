using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechWars
{
    class Program
    {
        static void Main(string[] args)
        {
            Game Game = new Game();
            Game.Start();
        }
    }

    class Game
    {
        public int mecha { get; private set; }
        public int mechgain { get; private set; }
        public int mines { get; private set; }
        public int money { get; private set; }
        public int factories { get; private set; }
        public int turn { get; private set; }
        public int emecha { get; private set; }
        public int emoney { get; private set; }
        public int emines { get; private set; }
        public int efactories { get; private set; }
        public int AI { get; private set; }
        public ConsoleKeyInfo input { get; private set; }
        public Random rnd = new Random();


        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            
            Console.WriteLine("##############IntializingGame###############"); //Flavor text, kind of like 'reticulating spleens'.  If you get the reference, you win!  I mainly want to showcase how fast it loads.  
            //print list of methods here
            Console.WriteLine("############################################");
            Console.WriteLine("#############Loading Scenarios###############");
            //Print list of scenarios here
            Console.WriteLine("############################################");
            Console.WriteLine("#############Loading Designs###############");
            //print list of designs here
            Console.WriteLine("############################################");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Loading Done");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            scenarioload();
            //end start
            Turn();
        }

        public void Turn()
        {
            
            printStats();
            Console.WriteLine("Press b to build factories, a to attack, q to quit, and any other button to end turn.");
            input = Console.ReadKey();
            Console.WriteLine();
            if (input.KeyChar.ToString() == "b")
            {
                buildfactories();

            }
            else if (input.KeyChar.ToString() == "q")
            {
                System.Environment.Exit(1);
            }
            else if (input.KeyChar.ToString() == "a")
            {
                if (mecha > 0)
                {
                    attack();
                }
                else
                {
                    Console.WriteLine("You fool!  You cannot attack with nothing!");
                    Turn();
                }
            }
            else
            {
                //next turn
            }
            nextturn();
        }
        public void nextturn()//###############################################################################NEXT TURN#################################################################  <  --- So I don't miss it.  
        {
            turnAI();// AI goes roit after player.  
            for (int ctr = 0; ctr < mecha; ctr++)//Player mine chance.  
            {
                int Pull = rnd.Next(1, 100);
                if (Pull <= 20)
                {
                    mines = mines + 1;
                }

            }
                        turnAI();
            for (int ctr = 0; ctr < emecha; ctr++)//Enemy mine chance.  
            {
                int Pull = rnd.Next(1, 100);
                if (Pull <= 20)
                {
                    emines = emines + 1;
                }

            }

            money = money + (mines * 50);
            int factorycost = factories * 100;
            money = money - factorycost;
            int remainder, quotient = Math.DivRem(factorycost, 100, out remainder);
            money = money + remainder * 100;
            if (money < 0)
            {
                factorycost = factorycost - money;
                Console.WriteLine("Your Factories are overproducing.");
                money = 0;
            }
            mechgain = (factorycost / 100) - remainder;
            if (mechgain < 0)
            { mechgain = 0; }
            mecha = mecha + mechgain;

            //EnemyTurn
            emoney = emoney + (emines * 50);
            int efactorycost = efactories * 100;
            quotient = Math.DivRem(efactorycost, 100, out remainder);
            emoney = emoney + remainder * 100;
            if (emoney < 0)
            {
                efactorycost = efactorycost - emoney;
                emoney = 0;
            }
            int emechgain = (efactorycost / 100) - remainder;
            if (emechgain < 0)
            { emechgain = 0; }
            emecha = emecha + emechgain;
            turn = turn + 1;
            Turn();



        }
        public void buildmech()
        {
            Console.WriteLine("Each Mech Costs 100 Ducats.  How many would you like to produce?");
            string prodmech = Console.ReadLine();
            int mechgain = 0;
            int.TryParse(prodmech, out mechgain);
            mecha = mecha + mechgain;
            money = money - (mechgain * 100);
            if (money < 0)
            {
                money = money + (mechgain * 100);
                Console.WriteLine("You fool!  You cannot have any negative money.");
                Console.WriteLine("The value of money is "+money+".");
                Turn();
            }

            printStats();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        public void scenarioload()
        {
            Console.WriteLine("Choose a scenario.");
            //ScenarioList();  // Lists all the scenarios one could pick, and attaches each char to an alphanumeric key.    
            //all scenarios are in the scenarios folder.  
            Console.WriteLine("a. Factories vs Mines");
            Console.WriteLine("b. Balanced Play");
            input = Console.ReadKey();
            if (input.KeyChar.ToString() == "a")
            {
                turn = 0;
                mecha = 0;
                money = 2000;
                mines = 0;
                emecha = 0;
                efactories = 10;
                emines = 10;

            }
            if (input.KeyChar.ToString() =="b")
            {
                turn = 0;
                mecha = 5;
                money = 0;
                mines = 0;
                emecha = 5;
                efactories = 0;
                emines = 0;
                AI = 1;


            }
            else
            {
                Console.WriteLine("You fool! Without a scenario you cannot play!");
                scenarioload();
            }
            Console.WriteLine();

        }
        public void import()
        {
            string faction = Console.ReadLine();
            System.IO.StreamReader file =
    new System.IO.StreamReader(@"c:\"+ faction +"ML.txt");
        }


        public void attack()
        {
            //select how many to attack with. *Not in this version

            //select where to attack. *Not in this version.  
            for (int cnt = 0; cnt < (mecha + emecha)/2; cnt++)
            {
                int attack = rnd.Next(1, 100);
                if (attack < 25)
                {
                emecha = emecha - 1;
                }
                else if(attack < 30)
                {
                    efactories = efactories - 1;
                }
                else if(attack < 35)
                {
                    emines = emines - 1;
                }



                if (attack < 25)
                {
                    mecha = mecha - 1;
                }
                else if (attack < 30)
                {
                    factories = factories - 1;
                }
                else if (attack < 35)
                {
                    mines = mines - 1;
                }

            }



            if (mecha <= 0)
            {
                factories = factories - 1;
                
                mecha = 0;
            }
            if (factories <= 0)
            {
                mines = mines - 1;
            }


            if (emecha <= 0)
            {
                efactories = efactories - 1;
                emecha = 0;
            }


            if (efactories <= 0)
            {
                efactories = 0;
                emines = emines - 1;
            }
            Turn();
         }

        public void pAttack()
        {
            //select how many to attack with. *Not in this version

            //select where to attack. *Not in this version.  
            for (int cnt = 0; cnt < (mecha + emecha) / 2; cnt++)
            {
                int attack = rnd.Next(1, 100);
                if (attack < 25)
                {
                    emecha = emecha - 1;
                }
                else if (attack < 30)
                {
                    efactories = efactories - 1;
                }
                else if (attack < 35)
                {
                    emines = emines - 1;
                }



                if (attack < 25)
                {
                    mecha = mecha - 1;
                }
                else if (attack < 30)
                {
                    factories = factories - 1;
                }
                else if (attack < 35)
                {
                    mines = mines - 1;
                }

            }



            if (mecha <= 0)
            {
                factories = factories - 1;

                mecha = 0;
            }
            if (factories <= 0)
            {
                mines = mines - 1;
            }


            if (emecha <= 0)
            {
                efactories = efactories - 1;
                emecha = 0;
            }


            if (efactories <= 0)
            {
                efactories = 0;
                emines = emines - 1;
            }
            Turn();
        }

        public void eAttack()
        {
            //select how many to attack with. *Not in this version

            //select where to attack. *Not in this version.  
            for (int cnt = 0; cnt < (mecha + emecha) / 2; cnt++)
            {
                int attack = rnd.Next(1, 100);

                if (attack < 25)
                {
                    mecha = mecha - 1;
                }
                else if (attack < 30)
                {
                    factories = factories - 1;
                }
                else if (attack < 35)
                {
                    mines = mines - 1;
                }

                if (attack < 25)
                {
                    emecha = emecha - 1;
                }
                else if (attack < 30)
                {
                    efactories = efactories - 1;
                }
                else if (attack < 35)
                {
                    emines = emines - 1;
                }





            }



            if (mecha <= 0)
            {
                factories = factories - 1;

                mecha = 0;
            }
            if (factories <= 0)
            {
                mines = mines - 1;
            }


            if (emecha <= 0)
            {
                efactories = efactories - 1;
                emecha = 0;
            }


            if (efactories <= 0)
            {
                efactories = 0;
                emines = emines - 1;
            }
            
        }
        public void printStats()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("It is turn " + turn + ".");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The value of money is " + money + ".");
            Console.WriteLine("You have " + mines + " mines.");
            Console.WriteLine("You have " + factories + "factories");
            Console.WriteLine("You have " + mecha + " mecha.");
            printEnemyStats();
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        
        public void buildfactories()
        {
            Console.WriteLine("Each factory costs 100 monies, and produces one mecha per turn for 100 money.  How many would you like to produce?");
            string prodfactory = Console.ReadLine();
            int factorygain = 0;
            int.TryParse(prodfactory, out factorygain);
            factories = factories + factorygain;
            money = money - (factorygain * 100);
            if (money < 0)
            {
                money = money + (factorygain * 100);
                Console.WriteLine("You fool!  You cannot have any negative money.");

                Turn();
            }
            
            printStats();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public void passiveAI()
        {
            int income;
            int expenses;
            int profit;
            int prodcost;
            if (emoney >= 500)
            {
                if (efactories < 5)
                {
                    efactories = efactories + 5;
                    emoney = emoney - 500;

                }
            }

            income = emines * 50;
            expenses = efactories * 100;
            profit = income - expenses;
            prodcost = profit / 200;
            efactories = efactories + prodcost;
            int ratio = emecha / mecha;
            if (ratio > 5)
            {
                eAttack();
            }
            
        }
        public void printEnemyStats()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("The enemy has " + emoney + " money.");
            Console.WriteLine("The enemy has " + emecha + " mecha.");
            Console.WriteLine("The enemy has " + efactories + " facotries.");
            Console.WriteLine("The enemy has " + emines + " mines.");
        }
        public void turnAI()
        {
            if (AI == 1)
            {
                passiveAI();
            }
        }

    }
}