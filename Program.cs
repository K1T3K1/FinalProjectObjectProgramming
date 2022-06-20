using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingManagment
{
    class Program
    {
        static void Main(string[] args)
        {
            Publishing publishing = new Publishing();
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine(
                    "\n1. Dodaj zamówienie\n" +
                    "2. Dodaj drukarnię\n" +
                    "3. Przejrzyj trwające zamówienia\n" +
                    "4. Opuść program");
                switch (Int32.Parse(Console.ReadLine()))
                {
                    case 1:
                        bool nextPrint = true;
                        while (nextPrint)
                        {
                            publishing.AddOrder(new Order());
                            Paper PaperType;
                            uint PagesAmount, quantity;
                            double Pictures;
                            bool ifColour, ifCover;
                            while (true)
                            {
                                try { PaperType = ReturnPaper(); break; }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            while (true)
                            {
                                try
                                { PagesAmount = insertNumber("Podaj ilość stron wydruku"); break; }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            while (true)
                            {
                                try { quantity = insertNumber("Podaj ilość którą chcesz wydrukować (Dla dokumentu cyfrowego wpisz 1)"); break; }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            while (true)
                            {
                                try { ifColour = insertBool("Czy wydruk ma być w kolorze? "); break; }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            while (true)
                            {
                                try { ifCover = insertBool("Czy mamy zaprojektować okładkę?"); break; }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            while (true)
                            {
                                try { Pictures = insertDouble("Podaj łączny rozmiar obrazków w m^2 "); break; }
                                catch (Exception ex) { Console.WriteLine(ex.Message); };
                            }
                            while(true)
                            {
                                try { addPrint("Wybierz typ wydruku", publishing, PaperType, PagesAmount, quantity, Pictures, ifColour, ifCover); break; }
                                catch (Exception ex) { Console.WriteLine(ex.Message); } 
                            }
                            Console.WriteLine("Czy chcesz dodać kolejny wydruk? 1. Tak 2. Nie");
                            switch (Int32.Parse(Console.ReadLine()))
                            {
                                case 1:
                                    break;
                                case 2:
                                    nextPrint = false;
                                    break;
                            }
                            publishing.OrderAssigner();

                        }
                        break;
                        
                    case 2:

                        Console.WriteLine("Podaj efektywność drukarni: ");
                        uint efficiency = uint.Parse(Console.ReadLine());
                        Console.WriteLine("Wybierz typ drukarni:\n"
                            +
                            "1. Czarnobiała 2. Kolorwa 3. Cyfrowa");
                        switch(int.Parse(Console.ReadLine()))
                        {
                            case 1:
                                BWPrinting bwprinting = new BWPrinting(efficiency);
                                publishing.Printings.Add(bwprinting);
                                break;
                            case 2:
                                ColourPrinting colourprinting = new ColourPrinting(efficiency);
                                publishing.Printings.Add(colourprinting);
                                break;
                            case 3:
                                DigitalPrinting digitalprinting = new DigitalPrinting(efficiency);
                                publishing.Printings.Add(digitalprinting);
                                break;
                        }
                        break;
                    case 3:
                        foreach(Order order in publishing.AOrders)
                        {
                            foreach (Print print in order.Prints)
                            {
                                Console.WriteLine( print.ToString());
                            }
                        }
                        break;
                    case 4:
                        exit = true;
                        break;
                    case 5:
                        foreach(Printing printing in publishing.Printings)
                        {
                            Console.WriteLine(printing.ToString());
                        }
                        break;
                }
            }
            Environment.Exit(0);
        }

        static Paper ReturnPaper()
        {
            Console.WriteLine
                ("Wybierz rozmiar papieru:\n" +
                 "1. A4\n" +
                 "2. A5\n" +
                 "3. B4\n" +
                 "4. B5\n");
            switch (Int32.Parse(Console.ReadLine()))
            {
                case 1:
                    return new A4();
                case 2:
                    return new A5();
                case 3:
                    return new B4();
                case 4:
                    return new B5();
                default:
                    throw new Exception("Błędny Wybór!");
            }
        }
        static uint insertNumber(string message)
        {
            Console.WriteLine(message);
            if (uint.TryParse(Console.ReadLine(), out uint result)) { return result; }
            else { throw new Exception("Podaj liczbę naturalną"); }
        }
        static bool insertBool(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("1. Tak 2. Nie");
            switch (Int32.Parse(Console.ReadLine()))
            {
                case 1:
                    return true;
                case 2:
                    return false;
                default:
                    throw new Exception("Błędny wybór");
            }

        }
        static double insertDouble(string message)

        {
            Console.WriteLine(message);
            if (double.TryParse(Console.ReadLine(), out double result)) { return result; }
            else { throw new Exception("Podaj liczbę dodatnią"); }
        }

        static void addPrint(string message, Publishing publishing, Paper PaperType, uint PagesAmount, uint quantity, double Pictures, bool ifColour, bool ifCover)
        {
            Console.WriteLine(message);
            Console.WriteLine("1. Książka 2. Czasopismo 3. Dokument cyfrowy");
            switch (Int32.Parse(Console.ReadLine()))
            {
                case 1:
                    publishing.NotAOrders[publishing.NotAOrders.Count - 1].addPrint(new Book(PaperType, quantity, PagesAmount, Pictures, ifColour, ifCover));break;                   
                case 2:
                    publishing.NotAOrders[publishing.NotAOrders.Count - 1].addPrint(new Magazine(PaperType, quantity, PagesAmount, Pictures, ifColour, ifCover));break;
                case 3:
                    publishing.NotAOrders[publishing.NotAOrders.Count - 1].addPrint(new Document(PaperType, PagesAmount, Pictures, ifColour, ifCover));break;
                default:
                    throw new Exception("Błędny wybór");
            }
        }
    }
}
