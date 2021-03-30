using System;
using System.Collections.Generic;

namespace disgustingBreakfastSimulator // Yup, I know. This name is terrible.
{
    public class Content // Goa grejer
    {
        private string item_name; // Olika object som håller namn, typ och pris.
        private string item_type;
        private double item_price;

        public string ItemName { get { return item_name; } set { item_name = value; } }

        public string ItemType { get { return item_type; } set { item_type = value; } }

        public double ItemPrice { get { return item_price; } set { item_price = value; } }

        public Content(string itemName, string itemType, double itemPrice) // Konstruktor
        {
            item_name = itemName;
            item_type = itemType;
            item_price = itemPrice;
        }
    }

    class disgustingBreakfastSimluator
    {
        private Content[] frukost = new Content[24]; // Array som stashar object av typ Content
        private List<Content> frukost_list = new List<Content>(); // Listan

        public void content_list()
        {
            frukost_list.Add(new Content("Surströmming", "Käk", 23.95));
            frukost_list.Add(new Content("Gammeldansk", "Dryck", 55.90));
            frukost_list.Add(new Content("Kaffesump", "Käk", 2.95));
            frukost_list.Add(new Content("Räksmoothie", "Dryck", 92.95));
            frukost_list.Add(new Content("Askfat", "Käk", 0.95));
            frukost_list.Add(new Content("Avloppsrens", "Dryck", 0.01));
            frukost_list.Add(new Content("Propplösare", "Dryck", 42.50));
            frukost_list.Add(new Content("Jäger", "Dryck", 78.90));
            frukost_list.Add(new Content("Marlboro Light", "Käk", 42.20));
            frukost_list.Add(new Content("Ettan Lös", "Käk", 32.89));
            frukost_list.Add(new Content("Ostron", "Käk", 114.98));
            frukost_list.Add(new Content("Ben & Jerrys: Tobak", "Käk", 55.40));
            frukost_list.Add(new Content("420", "Käk", 42.20));
        }

        public void stats() // Info
        { 
            Console.Clear();
            Console.WriteLine("\n -Erik's breakfast simulator-");
            Console.WriteLine(" Number of grejer änna: [{0}/24] total prajs: {1:0.00}kr", RecursiveFormula(0), calc_total());
            if (RecursiveFormula(0) == 24) // Meddelande om arrayen är full
                Console.WriteLine(" -That's enough för fan!-");
            else
                Console.WriteLine("");
        }

        public int RecursiveFormula(int index) // Rekursiv algoritm.
        {
            int antal = index < 24 && frukost[index] == null ? 
                RecursiveFormula(index + 1) : index < 24 && frukost[index] != null ? 
                    +1 + RecursiveFormula(index + 1) : +0;

            return antal;
        }

        public void Run() // In till meny
        {
            string choice;

            do // Meny i do-loop så inte programmet avslutas efter menyval.
            {
                stats();
                Console.WriteLine("\n -Meny");
                Console.WriteLine("\n Välj alternativ:");
                Console.WriteLine(" 1: Lägga till goa grejer");
                Console.WriteLine(" 2: Ta bort nåt");
                Console.WriteLine(" 3: Kvitto");
                Console.WriteLine(" 4: Sök go grej");
                Console.WriteLine(" 5: Sortera grejor");
                Console.WriteLine(" 0: Skit i det änna");

                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        add_frukost();
                        break;
                    case "2":
                        remove_frukost();
                        break;
                    case "3":
                        print_kvitto();
                        break;
                    case "4":
                        find_frukost();
                        break;
                    case "5":
                        sort_frukost();
                        break;
                    case "0": 
                        break;
                }
            } while (choice != "0"); // Villkoret för loopen. 
        }

        public void add_frukost() 
        {
            Random random = new Random();
            string str;

            do
            {
                stats();

                Console.WriteLine("\n -Meny Lägg till dryck-");

                Console.WriteLine("\n     {0,-17}  {1,-15} {2,6}\n", "Namn:", "Sort:", "Pris:");

                int str_index = 1;
                foreach (var frukost in frukost_list)
                {
                    string formatString = String.Format(" {3,2}: {0,-18} {1,-15} {2,5:0.00}kr", frukost.ItemName, frukost.ItemType, frukost.ItemPrice, str_index++);
                    Console.WriteLine(formatString);
                }
                Console.WriteLine("\n [S]ortera listan");
                Console.WriteLine(" [L]ägga till fler goa grejer");
                Console.WriteLine(" [F]yll med random");
                Console.WriteLine(" [G]å till menyn");
                Console.WriteLine("\n Välj dryck med index att lägga i backen:");

                str = Console.ReadLine();
                int choice = check(str);

                if (choice != -1 && choice < 25 && choice > 0)
                {
                    for (int i = 0; i < frukost.Length; i++) 
                    {
                        if (frukost[i] == null)
                        {
                            choice--;
                            frukost[i] = frukost_list[choice];
                            break;
                        }
                        else if (i == frukost.Length - 1)
                        {
                            Run();
                        } 
                    }
                }
                else
                {
                    if (str == "s" || str == "S")
                        sort_list();
                    else if (str == "l" || str == "L")
                    {
                        stats();
                        Console.WriteLine("\n Skriv in namn, sort och pris på grejor, '0' för att gå tillbaka:");
                        Console.Write(" Namn: ");
                        string namn = Console.ReadLine();
                        if (namn == "0")
                            break;
                        else
                        {
                            Console.Write(" Sort: ");
                            string sort = Console.ReadLine();
                            Console.Write(" Pris: ");
                            double pris = -1;
                            try
                            {
                                string temp = Console.ReadLine();
                                pris = Convert.ToDouble(temp);
                            }
                            catch
                            {
                                pris = -1;
                            }
                            if (pris != -1)
                            {
                                frukost_list.Add(new Content(namn, sort, pris));
                                Console.Clear();
                            }
                        }
                    }
                    else if (str == "f" || str == "F")
                    {
                        for (int i = 0; i < frukost.Length; i++)
                        {
                            if (frukost[i] == null) 
                            {
                                int rnd = random.Next(1, frukost_list.Count); 
                                frukost[i] = frukost_list[rnd];
                            }
                        }
                        break;
                    }
                    else if (str == "g" || str == "G") 
                        break; 
                                
                }
            } while (str != "g" || str != "G");
        }

        public void sort_list()
        {
            stats();
            string choice;

            Console.WriteLine("\n -Sortera meny-");
            Console.WriteLine("\n Sortera efter:");
            Console.WriteLine(" [N]amn");
            Console.WriteLine(" [S]ort");
            Console.WriteLine(" [P]ris");
            Console.WriteLine(" [G]å till menyn");

            choice = Console.ReadLine();

            if (choice == "n" || choice == "N") 
            {
                frukost_list.Sort((x, y) => x == null ? 1 : y == null ? -1 : x.ItemName.CompareTo(y.ItemName));
            }
            else if (choice == "s" || choice == "S") 
            {
                frukost_list.Sort((x, y) => x == null ? 1 : y == null ? -1 : x.ItemType.CompareTo(y.ItemType));
            }
            else if (choice == "p" || choice == "P") 
            {
                frukost_list.Sort((x, y) => x == null ? 1 : y == null ? -1 : x.ItemPrice.CompareTo(y.ItemPrice));
            }
            else if (choice == "g" || choice == "G") 
            {
                Run();  
            }
            else
            {
                sort_list(); 
            }
        }

        public void remove_frukost()
        {
            do   
            {
                stats();
                Console.WriteLine("\n Ta bort nåt");
                int num = 1;
                Console.WriteLine("\n     {0,-17}  {1,-13} {2,6}\n", "Namn:", "Sort:", "Pris:");

                foreach (var frukost in frukost)
                {
                    if (frukost != null)
                    {
                        string formatString = String.Format(" {3,2}: {0,-18} {1,-13} {2,5:0.00}kr", frukost.ItemName, frukost.ItemType, frukost.ItemPrice, num++);
                        Console.WriteLine(formatString);
                    }
                    else
                    {
                        string formatString = String.Format(" {1,2}: {0}", "Tom plats", num++);
                        Console.WriteLine(formatString);
                    }
                }
                Console.WriteLine("\n [S]kit i allt");
                Console.WriteLine(" [G]å till menyn");
                Console.Write("\n Ta bort nåt: ");
                string str = Console.ReadLine();

                if (str == "s" || str == "S")
                {
                    for (int i = 0; i < frukost.Length; i++)
                        if (frukost[i] != null) 
                            frukost[i] = null;
                }
                else if (str == "g" || str == "G")
                    break;
                else
                {
                    int index = check(str);

                    if (index > 0 && index <= frukost.Length)
                    {
                        index--;
                        frukost[index] = null;
                    }
                }
            } while (true);
        }

        public void print_kvitto()
        {
            stats();
            Console.WriteLine("\n -Visa order-");
            int num = 1;
            Console.WriteLine("\n     {0,-17}  {1,-15} {2,6}\n", "Namn:", "Sort:", "Pris:");

            foreach (var frukost in frukost)
            {
                if (frukost != null)
                {
                    string formatString = String.Format(" {3,2}: {0,-18} {1,-15} {2,5:0.00}kr", frukost.ItemName, frukost.ItemType, frukost.ItemPrice, num++);
                    Console.WriteLine(formatString);
                }
                else
                {
                    string formatString = String.Format(" {1,2}: {0}", "Tom plats", num++);
                    Console.WriteLine(formatString);
                }
            }
            Console.WriteLine("\n Tryck på nån tangent för meny änna...");
            Console.ReadKey();
        }

        public double calc_total()
        {
            double total_pris = 0;

            foreach (var frukost in frukost)
                if (frukost != null)
                    total_pris += frukost.ItemPrice;

            return total_pris;
        }

        public void find_frukost()
        {
            stats();

            int index;
            string namn;

            Console.WriteLine("\n -Sök efter nåt gött-");
            Console.WriteLine("\n Skriv in namnet på det göttiga:");

            namn = Console.ReadLine();

            index = LinearSearch(frukost, namn);
            if (index == -1)
                Console.WriteLine("\n Nope.");
            else
            {
                Console.WriteLine("\n {0} finns, vill du ha lite sånt eller?:", frukost[index].ItemName);
                Console.WriteLine("\n [L]ägga till samma");
                Console.WriteLine(" [T]a bort");
                Console.WriteLine(" [G]å till menyn");

                string choice = Console.ReadLine();      

                if (choice == "l" || choice == "L")      
                {
                    for (int i = 0; i < frukost.Length; i++)  
                    {
                        if (frukost[i] == null)
                        {
                            frukost[i] = frukost[index];       
                            break;
                        }
                        else if (i == frukost.Length - 1)  
                        {
                            Run();                     
                        }                                  
                    }
                }
                else if (choice == "t" || choice == "T")    
                {
                    frukost[index] = null;
                }
                else if (choice == "g" || choice == "G")   
                {
                    Run();
                }
            }
            Console.WriteLine("\n Tryck på valfri tangent för meny...");
            Console.ReadKey();
        }

        public int LinearSearch(Content[] frukost, string key)  
        {
            for (int i = 0; i < frukost.Length; i++)        
            {
                if (frukost[i] != null)                   
                    if (frukost[i].ItemName == key)
                        return i;                      
            }
            return -1;                                 
        }

        public void sort_frukost()                 
        {                                         
            stats();

            string choice;
            Console.WriteLine("\n -Sortera meny-"); 
            Console.WriteLine("\n Sortera efter:");
            Console.WriteLine(" [N]amn");
            Console.WriteLine(" [S]ort");
            Console.WriteLine(" [P]ris");
            /*Console.WriteLine(" [B]ubble sortering"); // Just wanted to try this after watching https://www.youtube.com/watch?v=IXLLwm_WN68&t=64s*/
            Console.WriteLine(" [G]å till menyn");

            choice = Console.ReadLine();             

            if (choice == "n" || choice == "N")       
            {
                Array.Sort(frukost, (x, y) => x == null ? 1 : y == null ? -1 : x.ItemName.CompareTo(y.ItemName)); 
                print_kvitto();
            }
            else if (choice == "s" || choice == "S")   
            {
                Array.Sort(frukost, (x, y) => x == null ? 1 : y == null ? -1 : x.ItemType.CompareTo(y.ItemType));
                print_kvitto();
            }
            else if (choice == "p" || choice == "P")   
            {
                Array.Sort(frukost, (x, y) => x == null ? 1 : y == null ? -1 : x.ItemPrice.CompareTo(y.ItemPrice));
                print_kvitto();
            }
            /*else if (choice == "b" || choice == "B")            
            {                                                       
                double temp; // Bubble
                for (int j = 0; j <= frukost.Length - 2; j++)
                {
                    for (int i = 0; i <= frukost.Length - 2; i++)
                    {
                        if (frukost[i + 1] == null)                
                            break;
                        else
                        {
                            if (frukost[i].ItemPrice > frukost[i + 1].ItemPrice)      
                            {
                                temp = frukost[i + 1].ItemPrice;           
                                frukost[i + 1].ItemPrice = frukost[i].ItemPrice;
                                frukost[i].ItemPrice = temp;
                            }
                        }
                    }
                }
                print_kvitto();
            }*/
            else if (choice == "g" || choice == "G")            
                Run();
            else
                sort_frukost();                        
        }

        public int check(string input)               
        {
            int correctValue;

            try
            {
                correctValue = int.Parse(input);
                return correctValue;
            }
            catch
            {
                return correctValue = -1;             
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)              
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;   
            Console.ForegroundColor = ConsoleColor.White;
            

            var disgustingBreakfastSimulator = new disgustingBreakfastSimluator();                
            disgustingBreakfastSimulator.content_list();                       
            disgustingBreakfastSimulator.Run();                                
        }
    }
    
    // TODO: Split things up?
}
