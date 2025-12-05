#ifndef STORE_HEADER
#define STORE_HEADER

#include <map>
#include <list>
#include <vector>
#include <string>
#include <numeric>
#include <iomanip>
#include <iostream>
#include <algorithm>

namespace Lab02
{
#pragma region Already Implemented Classes

    class Customer; /* Forward Declaration Of Customer Class */

    class Order
    {
        std::vector<std::pair<int, int>> orderProducts; /* Represents All Products Inside A Single Customers' Order */

    public:

        /* Already Implemented */
        void AddProduct(int productID, int quantity)
        {
            this->orderProducts.push_back(std::make_pair(productID, quantity));
        }

        /* Already Implemented */
        const std::vector<std::pair<int, int>>& GetProducts() const
        {
            return this->orderProducts;
        }
    };

    struct Product
    {
        int productID; /* Represents Product ID - Compare Products Using This */

        std::string productName; /*  */

        Product(int _productID, std::string _productName) : productID(_productID), productName(_productName) { }
    };

    struct OrderProduct : public Product
    {
        int productQuantity; /* Represents Quantity Of Given Product */

        OrderProduct(int _productID, int _productQuantity, std::string _productName) : Product(_productID, _productName)
        {
            this->productQuantity = _productQuantity;
        }
    };

    struct StoreProduct : public OrderProduct
    {
        float productPrice; /* Represents Price Of Given Product Inside A Store */

        StoreProduct(int _productID, int _productQuantity, float _productPrice, std::string _productName) : OrderProduct(_productID, _productQuantity, _productName)
        {
            this->productPrice = _productPrice;
        }
    };

#pragma endregion

    class Store
    {
        std::vector<StoreProduct> productsInStore; /* Represents All Products (With Their Quantity And Price) Currently Available In Store */

    public:

        /* Declare Methods Here */

        Store(std::list<StoreProduct> storeProducts);

        float StoreStocktaking();
        StoreProduct GetMostValuableProduct();
        void PerformInflation(float inflationValue);
        bool CompleteTheOrder(const Order& order);
        std::vector<StoreProduct> GetProductsWithQuantityUpTo(float quantity);

        void RefillTheStore(std::map<int, int> newProducts);

        std::vector<StoreProduct>& GetItems()
        {
            return this->productsInStore;
        }

        friend std::ostream& operator<<(std::ostream& out, const Store& store);

    };
}

#endif /* STORE_HEADER */
