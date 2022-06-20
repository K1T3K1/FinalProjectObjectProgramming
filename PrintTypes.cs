using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingManagment
{
    public class Order
    {
        /*public List<uint> Quantity;*/ // moved to individual prints so there is no need to track indexes of 2 lists at the same time and prevents index mismatches
        public List<Print> Prints;
        public Client OrdClient;
        public double OrderPrice;
        public double orderTime;
        public Order(){ Prints = new List<Print>(); }
        public Order(Client OrdClient)
        {
            Prints = new List<Print>();
            this.OrdClient = OrdClient;
        }

        public void addPrint(Print print)
        { this.Prints.Add(print); }

        public void CalculatePriceOrder()
        {
            double TotalOrderPrice = 0;
            foreach (Print p in Prints)
            {
                if (p.GetType() == typeof(Document)) { TotalOrderPrice += p.Price * 5000; }
                else { TotalOrderPrice += p.quantity * p.Price; }
            }
            this.OrderPrice = TotalOrderPrice * OrdClient.GetDiscount();
        }

        /*public void daysToOrder()
        {
            double DaysToGo = 0;
            //1 efficiency unit equals to 
            //1 book of 500 pages
            //in 1 day
            //doesn't matter whether it's a colour or bw book
            //for magazine it's
            //1 magazine of 60 pages per day
            foreach(Print p in Prints)
            {
                if(p.GetType() == typeof(Book))
                { DaysToGo += (p.PagesAmount / 500) * p.quantity;}
                if(p.GetType()==typeof(Magazine))
                { DaysToGo += (p.PagesAmount / 60) * p.quantity;}
                if(p.GetType()==typeof(Document))
                { DaysToGo += (p.PagesAmount / 50);}
            }
            this.orderTime = DaysToGo;
        }*/
    }


    public abstract class Print
    {
        protected Paper PaperType;
        public readonly uint PagesAmount;
        public readonly uint quantity;
        protected double Pictures; //Pictures Per Page defined as Pictures in m^2 divided by PagesAmount*PageSize so it stays between <0;1>
        public bool ifColour;
        protected bool ifCover; //Tells whether our Publishing designs cover or Client hires his own artist to do that
        internal double Price;
        internal double Time;
        public double timeToComplete;
        public Print(Paper PaperType, uint quantity, uint Pages, double Pictures, bool Colour, bool Cover)
        {
            this.quantity = quantity;
            this.PaperType = PaperType;
            this.PagesAmount = Pages;
            this.Pictures = Pictures;
            this.ifColour = Colour;
            this.ifCover = Cover;
            /*this.Price =*/ CalculatePricePrint();
            CalculateTimePrint();
            //I'm using this even though I don't need to, for code transparency
        }
        protected void CalculatePricePrint()
        {
            double PicturesSize = Pictures / PagesAmount * PaperType.getSize();
            double pricePerPage = PaperType.getPrice() * (Math.Sqrt(PicturesSize)) * (1 - PicturesSize * (1 / 10));
            //if(this.GetType() == typeof(Document)) {this.Price = pricePerPage* PagesAmount*0.2; }
            if (ifColour) { this.Price =  pricePerPage * PagesAmount; }
            else { this.Price =  pricePerPage * PagesAmount * 0.7; }
        }

        protected virtual void CalculateTimePrint()
        {
            this.Time = (PaperType.getSize()*PagesAmount*(quantity*0.001))*0.1;
        }
        public override string ToString()
        {
            return (

                "Typ wydruku: " + this.GetType().Name +
                " Rodzaj papieru: " + this.PaperType.GetType().Name +
                " Ilość stron: " + this.PagesAmount + 
                " Liczba sztuk: " + this.quantity +
                " Cena: " + this.Price.ToString("N2") +
                " Czas do ukończenia: " + this.timeToComplete.ToString("N2") + "\n");
        }

        //public uint getQuantity() { return this.quantity; }

    }

    public class Book : Print
    {
        public Book(Paper PaperType, uint quantity, uint Pages, double Pictures, bool Colour, bool Cover) : base(PaperType, quantity, Pages, Pictures, Colour, Cover) 
            {
                Time = (this.PagesAmount / 500) * this.quantity;
            }

    }


    public class Magazine : Print
    {
        public Magazine(Paper PaperType, uint quantity, uint Pages, double Pictures, bool Colour, bool Cover) : base(PaperType, quantity, Pages, Pictures, Colour, Cover) 
            {
                Time = (this.PagesAmount / 60) * this.quantity;
            }
    }


    public class Document : Print
    {
        public Document(Paper PaperType, uint Pages, double Pictures, bool Colour, bool Cover) : base(PaperType, 1 , Pages, Pictures, Colour, Cover) 
            {
                Time += (this.PagesAmount / 50);
            }

    }

}
