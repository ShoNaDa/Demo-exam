using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using AvaloniaApplication1.Models;
using HarfBuzzSharp;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        //create test data
        using (ApplicationContext db = new ApplicationContext())
        {
            if (db.Clients.ToList().Count == 0)
            {
                //2 clients
                Client client1 = new Client
                    { Fio = "BVV", Address = "Tomsk", Passport = "123123321", Phone = "89539150259" };
                Client client2 = new Client
                    { Fio = "KNS", Address = "Tomsk", Passport = "321321123", Phone = "89539151259" };

                db.Clients.AddRange(client1, client2);

                //2 products
                Product product1 = new Product
                    { ManufCountry = "Russia", Brand = "Kia", Model = "Rio", Color = "Red", Count = 2, Price = 100 };
                Product product2 = new Product
                    { ManufCountry = "Russia", Brand = "Skoda", Model = "Oktavia", Color = "Blue", Count = 3, Price = 200 };
                
                db.Products.AddRange(product1, product2);
                
                //all types purchases
                TypesPurchase typesPurchase1 = new TypesPurchase { CashTransfer = 0, CreditNow = 0 };
                TypesPurchase typesPurchase2 = new TypesPurchase { CashTransfer = 0, CreditNow = 1 };
                TypesPurchase typesPurchase3 = new TypesPurchase { CashTransfer = 1, CreditNow = 0 };
                TypesPurchase typesPurchase4 = new TypesPurchase { CashTransfer = 1, CreditNow = 1 };
                
                db.TypesPurchases.AddRange(typesPurchase1, typesPurchase2, typesPurchase3, typesPurchase4);
                
                //2 purchases
                Purchase purchase1 = new Purchase
                {
                    ProductIdFk = db.Products.ToList().FirstOrDefault(p => p.ProductId == 1).ProductId,
                    ClientIdFk = db.Clients.ToList().FirstOrDefault(c => c.ClientId == 1).ClientId,
                    DatePurchase = DateTime.Now,
                    TypePurchaseIdFk = db.TypesPurchases.ToList().FirstOrDefault(t => t.TypeId == 1).TypeId
                };
                Purchase purchase2 = new Purchase
                {
                    ProductIdFk = db.Products.ToList().FirstOrDefault(p => p.ProductId == 2).ProductId,
                    ClientIdFk = db.Clients.ToList().FirstOrDefault(c => c.ClientId == 2).ClientId,
                    DatePurchase = DateTime.Now,
                    TypePurchaseIdFk = db.TypesPurchases.ToList().FirstOrDefault(t => t.TypeId == 3).TypeId
                };
            }
        }
    }
}
