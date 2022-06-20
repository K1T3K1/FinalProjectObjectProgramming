using System.Collections.Generic;
using System; 

namespace PublishingManagment
{
    public class Publishing
    {
        public List<Printing> Printings;
        public List<Client> Clients;
        public List<Order> NotAOrders;
        public List<Order> AOrders;
        public Publishing() { Printings = new List<Printing>();
            Clients = new List<Client>();
            NotAOrders = new List<Order>();
            AOrders = new List<Order>();
        }
        public Publishing(List<Printing> Printings, List<Client> Clients) { this.Printings = Printings; this.Clients = Clients;
            NotAOrders = new List<Order>();
            AOrders = new List<Order>();
        }
        public List<Printing> GetPrintings() { return Printings; }
        public List<Client> GetClients() { return Clients; }
        public void AddOrder(Order newOrder) { this.NotAOrders.Add(newOrder); }
        public void OrderAssigner() 
        {
            foreach(Order order in this.NotAOrders.ToArray())
            {
                foreach(Print print in order.Prints)
                {
                    if(print.GetType() == typeof(Document))
                    {
                        assignDigital(print);
                    }
                    if(print.ifColour == true && print.GetType() != typeof(Document))
                    {
                        assignColour(print);
                    }
                    if(print.ifColour == false && print.GetType() != typeof(Document))
                    {
                        assignBW(print);
                    }
                }
                AOrders.Add(order);
                NotAOrders.Remove(order);
            }

        }
        private void assignDigital(Print print)
        {
            DigitalPrinting lowestLoad;
            Printing[] tempPrintings = Printings.FindAll(x => x is DigitalPrinting).ToArray();
            lowestLoad = (DigitalPrinting)tempPrintings[0];
            foreach(DigitalPrinting printing in tempPrintings)
            {
                if(lowestLoad.getFreeLoad() > printing.getFreeLoad())
                {
                    lowestLoad = printing;
                }
            }
            lowestLoad.addLoad(print.Time);
            lowestLoad.assignedPrints.Add(print);
            print.timeToComplete = lowestLoad.load / lowestLoad.efficiency;

        }
        private void assignColour(Print print)
        {
            ColourPrinting lowestLoad;
            Printing[] tempPrintings = Printings.FindAll(x => x is ColourPrinting).ToArray();
            lowestLoad = (ColourPrinting)tempPrintings[0];
            foreach (ColourPrinting printing in tempPrintings)
            {
                if (lowestLoad.getFreeLoad() > printing.getFreeLoad())
                {
                    lowestLoad = printing;
                }
            }
            lowestLoad.addLoad(print.Time);
            lowestLoad.assignedPrints.Add(print);
            print.timeToComplete = lowestLoad.load / lowestLoad.efficiency;

        }
        private void assignBW(Print print)
        {
            BWPrinting lowestLoad;
            Printing[] tempPrintings = Printings.FindAll(x => x is BWPrinting).ToArray();
            lowestLoad = (BWPrinting)tempPrintings[0];
            foreach (BWPrinting printing in tempPrintings)
            {
                if (lowestLoad.getFreeLoad() > printing.getFreeLoad())
                {
                    lowestLoad = printing;
                }
            }
            lowestLoad.addLoad(print.Time);
            lowestLoad.assignedPrints.Add(print);
            print.timeToComplete = lowestLoad.load / lowestLoad.efficiency;

        }
    }



    public class Client
    {
        public string name;
        private uint ordersDone;
        public Client(string name) { this.name = name; }
        public Client(string name, uint ordersDone) { this.name = name; this.ordersDone = ordersDone; }
        public double GetDiscount() { return (1 - (ordersDone / 10000)); }
    }

    /* Printing abstract class allows to inherint diffirent Printings for diffirent order types,
     * but making it abstract denies creating of undefined Printing object
    */

    public abstract class Printing
    {
        public double efficiency; // efficiency measured by ability to print x prints per week
        public double load;
        public List<Print> assignedPrints;
        //protected List<Order> Orders; //It is going to stay, it will serve as list of order assigned to the printing

        protected Printing(uint efficiency) { this.efficiency = efficiency; this.load = 0;
            assignedPrints = new List<Print>();
        }

        //moved to Publishing class/and moved back huh// and moved out again. I don't want to multiply orders. All Load managment and order assigning will stay on the side of publishing module
        /*public void AddOrder(Order newOrder)
        {
            this.Orders.Add(newOrder);
        }*/
        public double getFreeLoad()
        {
            return this.efficiency - this.load;
        }
        public void addLoad(double toAdd)
        {
            if (this.getFreeLoad() < toAdd)
                throw new PrintingFullException(toAdd, efficiency, load, "No free printing!");
            else
                this.load += toAdd;
        }
            
    }
    



    public class DigitalPrinting : Printing
    {
        public DigitalPrinting(uint efficiency) : base(efficiency) { }
    }




    public class ColourPrinting : Printing
    {
        public ColourPrinting(uint efficiency) : base(efficiency) { }
    }

    public class BWPrinting : Printing
    {
        public BWPrinting(uint efficiency) : base(efficiency) { }
    }
}


