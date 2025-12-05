#include "Customer.h"
#include "Store.h"

namespace Lab02
{
    std::ostream& operator<<(std::ostream& out, const Customer& customer)
    {
        for (const std::pair<int, int>& orderItem : customer.shoppingList)
        {
            out << "ProductID: " << std::setw(2) << orderItem.first << " With Quantity: " << orderItem.second << std::endl;
        }

        return out;
    }

    /* Implement Methods Here */

    Customer::Customer(std::vector<int> items, std::vector<int> quantities)
    {
        for (unsigned int i = 0; i < items.size(); i++)
            this->shoppingList.push_back(std::make_pair(items[i], quantities[i]));
    }

    void Customer::CreateCheapOrder(Store& store, int productsCount)
    {
        auto storeItems = store.GetItems();

        std::sort(storeItems.begin(), storeItems.end(), [](const StoreProduct& itemA, const StoreProduct& itemB)
        {
            return itemA.productPrice < itemB.productPrice;
        });

        Order newCustomerOrder; int storeItemIndex = 0; int currentQuantity = productsCount;

        while (currentQuantity > 0)
        {
            if (storeItems[storeItemIndex].productQuantity >= currentQuantity) 
            {
                newCustomerOrder.AddProduct(storeItems[storeItemIndex].productID, currentQuantity);

                currentQuantity -= currentQuantity;
            }
            else
            {
                newCustomerOrder.AddProduct(storeItems[storeItemIndex].productID, storeItems[storeItemIndex].productQuantity);

                currentQuantity -= storeItems[storeItemIndex].productQuantity; storeItemIndex += 1;
            }
        }

        this->customerOrders.push_back(newCustomerOrder);

        /* Can Complete Order - Store Has Enough Products */
        if (store.CompleteTheOrder(newCustomerOrder) == true)
            this->shoppingList.clear();
    }

    void Customer::CreateOrder(Store& store)
    {
        Order newCustomerOrder;

        for (const auto& itemQuantity : this->shoppingList)
            newCustomerOrder.AddProduct(itemQuantity.first, itemQuantity.second);

        this->customerOrders.push_back(newCustomerOrder);

        /* Can Complete Order - Store Has Enough Products */
        if (store.CompleteTheOrder(newCustomerOrder) == true)
            this->shoppingList.clear();
    }
}
