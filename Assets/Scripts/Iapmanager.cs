using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class Iapmanager : MonoBehaviour, IDetailedStoreListener
{
    IStoreController m_StoreController; // The Unity Purchasing system.

    //Your products IDs. They should match the ids of your products in your store.

    string coin_500 = "com.samplegame.product";
    string coin_1500 = "com.samplegame.product1";
    string coin_3000 = "com.samplegame.product2";
    string coin_10000 = "com.samplegame.product3";


    void Start()
    {
        InitializePurchasing();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Add products that will be purchasable and indicate its type.
        builder.AddProduct(coin_500, ProductType.Consumable);
        builder.AddProduct(coin_1500, ProductType.Consumable);
        builder.AddProduct(coin_3000, ProductType.Consumable);
        builder.AddProduct(coin_10000, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void Buy500Coin()
    {
        soundmanager.instance.clicksound();
        print("500 coin");
        m_StoreController.InitiatePurchase(coin_500);
    }

    public void Buy1500Coin()
    {
        soundmanager.instance.clicksound();
        print("1500 coin");
        m_StoreController.InitiatePurchase(coin_1500);
    }
    
    public void Buy3000Coin()
    {
        soundmanager.instance.clicksound();
        print("3000 coin");
        m_StoreController.InitiatePurchase(coin_3000);
    }
    
    public void Buy10000Coin()
    {
        soundmanager.instance.clicksound();
        print("10000 coin");
        m_StoreController.InitiatePurchase(coin_10000);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        OnInitializeFailed(error, null);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

        if (message != null)
        {
            errorMessage += $" More details: {message}";
        }

        Debug.Log(errorMessage);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        //Retrieve the purchased product
        var product = args.purchasedProduct;

        //Add the purchased product to the players inventory
        if (product.definition.id == coin_500)
        {
            int coinearn = 500;
            AddCoin(coinearn);
        }
        else if (product.definition.id == coin_1500)
        {
            int coinearn = 1500;
            AddCoin(coinearn);
        }
        else if (product.definition.id == coin_3000)
        {
            int coinearn = 3000;
            AddCoin(coinearn);
        }
        else if (product.definition.id == coin_10000)
        {
            int coinearn = 10000;
            AddCoin(coinearn);
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
            $" Purchase failure reason: {failureDescription.reason}," +
            $" Purchase failure details: {failureDescription.message}");
    }

    void AddCoin(int coinearn)
    {
        print("Coin earn: " + coinearn);
        int totalcoin = helper.GetTotalCoin();
        totalcoin += coinearn;

        helper.settotalcoin(totalcoin);
    }

}
