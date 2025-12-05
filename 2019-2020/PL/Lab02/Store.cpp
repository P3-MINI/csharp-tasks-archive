#include "Store.h"
#include "Customer.h"

namespace Lab02
{

    /* Already Implemented */
    std::ostream& operator<<(std::ostream& out, const Store& store)
    {
        for (const StoreProduct& storeItem : store.productsInStore)
        {
            out << "Product: " << std::setw(10) << storeItem.productName << " (" << std::setw(2) << storeItem.productID << ") With Price: " << std::setw(6) << storeItem.productPrice << " And Quantity: " << storeItem.productQuantity << std::endl;
        }
        return out;
    }

    /* Already Implemented */
    void Store::RefillTheStore(std::map<int, int> newProducts)
    {
        std::for_each(newProducts.cbegin(), newProducts.cend(), [&](const std::pair<int, int>& productToQuantity)
        {
            auto itemFound = std::find_if(this->productsInStore.begin(), this->productsInStore.end(), [=](const StoreProduct& storeProduct)
            {
                return storeProduct.productID == productToQuantity.first;
            });

            if (itemFound != this->productsInStore.end())
            {
                itemFound->productQuantity = productToQuantity.second;
            }
            else
            {
                this->productsInStore.push_back(StoreProduct(productToQuantity.first, productToQuantity.second, 10.0, "NewProduct_" + std::to_string(productToQuantity.first)));
            }
        });
    }

    /* Implement Methods Here */

    Store::Store(std::list<StoreProduct> storeProducts)
    {
        for (const StoreProduct& storeItem : storeProducts)
            this->productsInStore.push_back(storeItem);
    }

    bool Store::CompleteTheOrder(const Order& order)
    {
        /* Check If There Is Possibility To Complete The Order */
        for (const std::pair<int, int>& productWithQuantity : order.GetProducts())
        {
            auto foundStoreItem = std::find_if(this->productsInStore.begin(), this->productsInStore.end(), [productWithQuantity](const StoreProduct& storeItem)
            {
                return productWithQuantity.first == storeItem.productID;
            });

            if (foundStoreItem != this->productsInStore.end())
            {
                if (foundStoreItem->productQuantity < productWithQuantity.second)
                {
                    return false; /* Not Enough Products' Quantity */
                }
            }
            else return false; /* Product Not Available In The Store */
        }

        /* Complere The Order */
        for (const std::pair<int, int>& productWithQuantity : order.GetProducts())
        {
            auto foundStoreItem = std::find_if(this->productsInStore.begin(), this->productsInStore.end(), [productWithQuantity](const StoreProduct& storeItem)
            {
                return productWithQuantity.first == storeItem.productID;
            });

            /* We Already Know That Product Is In The Store - No Need To Check */

            foundStoreItem->productQuantity -= productWithQuantity.second;
        }

        return true;
    }

    float Store::StoreStocktaking()
    {
        return std::accumulate(this->productsInStore.cbegin(), this->productsInStore.cend(), 0.0f, [](float& resultValue, const StoreProduct& storeItem)
        {
            return resultValue + storeItem.productPrice * storeItem.productQuantity;
        });
    }

    void Store::PerformInflation(float inflationValue)
    {
        std::transform(this->productsInStore.begin(), this->productsInStore.end(), this->productsInStore.begin(), [=](StoreProduct& storeItem)
        {
            storeItem.productPrice *= inflationValue;

            return storeItem;
        });
    }

    StoreProduct Store::GetMostValuableProduct()
    {
        auto resultIterator = std::max_element(this->productsInStore.begin(), this->productsInStore.end(), [](const Lab02::StoreProduct& storeProduct1, const Lab02::StoreProduct& storeProduct2)
        {
            return storeProduct1.productQuantity * storeProduct1.productPrice < storeProduct2.productQuantity * storeProduct2.productPrice;
        });

        return *resultIterator;
    }

    std::vector<StoreProduct> Store::GetProductsWithQuantityUpTo(float quantity)
    {
        std::vector<StoreProduct> resultProducts;

        std::for_each(this->productsInStore.begin(), this->productsInStore.end(), [quantity, &resultProducts](const StoreProduct& storeProduct)
        {
            if (storeProduct.productQuantity < quantity)
                resultProducts.push_back(storeProduct);
        });

        return resultProducts;
    }
}
