#ifndef CUSTOMER_HEADER
#define CUSTOMER_HEADER

#include <list>
#include <vector>
#include <iomanip>
#include <iostream>
#include <algorithm>

namespace Lab02
{
    class Store; /* Forward Declaration Of Store Class */
    class Order; /* Forward Declaration Of Order Class */

    class Customer
    {
        std::vector<Order> customerOrders; /* Represents All Customers' Order */
        std::list<std::pair<int, int>> shoppingList; /* Represents Shopping List */

    public:

        /* Declare Methods Here */
        Customer(std::vector<int> items, std::vector<int> quantities);

        void CreateOrder(Store& store);
        void CreateCheapOrder(Store& store, int productsCount);

        friend std::ostream& operator<<(std::ostream& out, const Customer& customer);
    };
}

#endif /* CUSTOMER_HEADER */
