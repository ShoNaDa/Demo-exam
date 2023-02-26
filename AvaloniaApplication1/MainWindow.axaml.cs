using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication1.Models;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        //create test data
        using (Gr692BvvContext db = new Gr692BvvContext())
        {
            #region test datas

            if (db.Clients.ToList().Count == 0)
            {
                //2 clients
                Client client1 = new Client
                    { Fio = "BVV", Address = "Tomsk", Passport = "123123321", Phone = "89539150259" };
                Client client2 = new Client
                    { Fio = "KNS", Address = "Tomsk", Passport = "321321123", Phone = "89539151259" };

                db.Clients.AddRange(client1, client2);
                db.SaveChanges();
            }

            if (db.Products.ToList().Count == 0)
            {
                //2 products
                Product product1 = new Product
                    { ManufCountry = "Russia", Brand = "Kia", Model = "Rio", Color = "Red", Count = 2, Price = 100 };
                Product product2 = new Product
                {
                    ManufCountry = "Russia", Brand = "Skoda", Model = "Oktavia", Color = "Blue", Count = 3, Price = 200
                };

                db.Products.AddRange(product1, product2);
                db.SaveChanges();
            }

            if (db.TypesPurchases.ToList().Count == 0)
            {
                //all types purchases
                TypesPurchase typesPurchase1 = new TypesPurchase { CashTransfer = 0, CreditNow = 0 };
                TypesPurchase typesPurchase2 = new TypesPurchase { CashTransfer = 0, CreditNow = 1 };
                TypesPurchase typesPurchase3 = new TypesPurchase { CashTransfer = 1, CreditNow = 0 };
                TypesPurchase typesPurchase4 = new TypesPurchase { CashTransfer = 1, CreditNow = 1 };

                db.TypesPurchases.AddRange(typesPurchase1, typesPurchase2, typesPurchase3, typesPurchase4);
                db.SaveChanges();
            }

            if (db.Purchases.ToList().Count == 0)
            {
                //2 purchases
                Purchase purchase1 = new Purchase
                {
                    ProductIdFk = db.Products.ToList()
                        .FirstOrDefault(p => p.ProductId == db.Products.ToList().Max(p => p.ProductId) - 1).ProductId,
                    ClientIdFk = db.Clients.ToList()
                        .FirstOrDefault(c => c.ClientId == db.Clients.ToList().Max(c => c.ClientId) - 1).ClientId,
                    DatePurchase = DateOnly.FromDateTime(DateTime.Now),
                    TypePurchaseIdFk = db.TypesPurchases.ToList()
                        .FirstOrDefault(t => t.TypeId == db.TypesPurchases.ToList().Max(t => t.TypeId) - 2).TypeId
                };
                Purchase purchase2 = new Purchase
                {
                    ProductIdFk = db.Products.ToList()
                        .FirstOrDefault(p => p.ProductId == db.Products.ToList().Max(p => p.ProductId) - 1).ProductId,
                    ClientIdFk = db.Clients.ToList()
                        .FirstOrDefault(c => c.ClientId == db.Clients.ToList().Max(c => c.ClientId)).ClientId,
                    DatePurchase = DateOnly.FromDateTime(DateTime.Now),
                    TypePurchaseIdFk = db.TypesPurchases.ToList()
                        .FirstOrDefault(t => t.TypeId == db.TypesPurchases.ToList().Max(t => t.TypeId)).TypeId
                };

                db.Purchases.AddRange(purchase1, purchase2);
                db.SaveChanges();
            }

            #endregion

            //binding 1 window
            CarTextBlock.Text = "Car - '' '' exist";

            ListBrandCB.Items = db.Products.GroupBy(p => p.Brand).Select(p => p.FirstOrDefault()).ToList();
            ListBrandCB.SelectedIndex = 0;
            ListModelCB.Items = db.Products.ToList().Where(p => p.Brand == ((Product)ListBrandCB.SelectedItem).Brand);
            ListModelCB.SelectedIndex = 0;

            //binding 2 window
            ListModels.Items = db.Products.GroupBy(p => p.Model).Select(p => p.FirstOrDefault()).ToList();
            ListModels.SelectedIndex = 0;
        }
    }

    private void SelectExistButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string selectedBrand, selectedModel;
        selectedBrand = ((Product)ListBrandCB.SelectedItem).Brand;
        selectedModel = ((Product)ListModelCB.SelectedItem).Model;

        using (Gr692BvvContext db = new Gr692BvvContext())
        {
            int? countCars = db.Products.ToList().Where(p => p.Brand == selectedBrand && p.Model == selectedModel)
                .FirstOrDefault().Count;

            if (countCars != 0)
                CarTextBlock.Text = $"Car - '{selectedBrand}' '{selectedModel}' exist";
            else
                CarTextBlock.Text = $"Car - '{selectedBrand}' '{selectedModel}' not exist";
        }
    }

    //1 window
    private void ListBrandCB_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ListBrandCB.SelectedIndex != -1)
        {
            using (Gr692BvvContext db = new Gr692BvvContext())
            {
                ListModelCB.Items =
                    db.Products.ToList().Where(p => p.Brand == ((Product)ListBrandCB.SelectedItem).Brand);
                ListModelCB.SelectedIndex = 0;
            }
        }
    }

    private void SelectInfoButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using (Gr692BvvContext db = new Gr692BvvContext())
        {
            InfoAboutCar.Items = db.Products.ToList().Where(p => p.Model == ((Product)ListModels.SelectedItem).Model);
        }
    }
}