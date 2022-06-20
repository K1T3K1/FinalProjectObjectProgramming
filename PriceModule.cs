using System;

namespace PublishingManagment
{
    /*public class PriceCalculator
    {
        public PriceCalculator() { }

        public double CalculatePricePrint(Paper PaperType, uint PagesAmount, double Pictures, bool ifColour)
        {
            double PicturesSize = Pictures / (PagesAmount * PaperType.getSize());
            double pricePerPage = PaperType.getPrice() * (Math.Sqrt(PicturesSize)) * (1 - PicturesSize * (1 / 10));
            
            if (ifColour) {return pricePerPage * PagesAmount;}
            else {return pricePerPage * PagesAmount * 0.7;}
        }
        
        public double CalculatePricePrint(Print print)
        {
            double PicturesSize = print.Pictures / (print.PagesAmount * print.PaperType.getSize());
            double pricePerPage = print.PaperType.getPrice() * (Math.Sqrt(PicturesSize)) * (1 - PicturesSize * (1 / 10));
            
            if (print.ifColour) {return pricePerPage * print.PagesAmount;}
            else {return pricePerPage * print.PagesAmount * 0.7;}
        }


        public double CalculatePriceOrder(Order order)
        {
            double TotalOrderPrice = 0;
            foreach(Print p in order.Prints)
            {
                TotalOrderPrice += order.Prints[p].quantity * order.Prints[p].
            }
        }
    }*/
    //methods moved to print and order classes

    

    public abstract class Paper
    {
        protected double dimensionX, dimensionY, priceFactor; //dimensions of the paper in cm
        public Paper(double dimensionX, double dimensionY, double priceFactor)
        { this.dimensionX = dimensionX; this.dimensionY = dimensionY; this.priceFactor = priceFactor; }

        public double getSize() { return dimensionX * dimensionY; }
        public double getPrice() { return priceFactor; }

    }

    public class A4 : Paper
    {
        public A4() : base(2.1, 2.97, 0.02) { }
    }


    public class A5 : Paper
    {
        public A5() : base(2.1, 1.48, 0.015) { }
    }

    public class B5 : Paper
    {
        public B5() : base(1.76, 2.5, 0.017) { }
    }
    public class B4 : Paper
    {
        public B4() : base(3.53, 2.5, 0.023) { }
    }


}