#include <cstdlib>
#include <iostream>
#include <algorithm>

#include "Store.h"
#include "Customer.h"

int main(int argc, char** argv)
{
    std::cout << " ********************** Stage_1 (1.0 Pts) ********************** " << std::endl;

    Lab02::Store store_1({
        Lab02::StoreProduct(0,  15, 30.0f,   "Headphones"),
        Lab02::StoreProduct(1,  10, 190.0f,  "Shoes"),
        Lab02::StoreProduct(2,  6,  3.5f,    "Chocolate"),
        Lab02::StoreProduct(3,  4,  3599.0f, "Computer"),
        Lab02::StoreProduct(4,  10, 13.0f,   "Showel Gel"),
        Lab02::StoreProduct(5,  7,  5.5f,    "Coca-Cola"),
        Lab02::StoreProduct(6,  7,  15.5f,   "Paper"),
        Lab02::StoreProduct(7,  2,  150.0f,  "Bookshelf"),
        Lab02::StoreProduct(8,  4,  8.0f,    "Bateries"),
        Lab02::StoreProduct(9,  2,  1900.0f, "TV"),
        Lab02::StoreProduct(10, 5,  23.0f,   "Coffee"),
        Lab02::StoreProduct(11, 10, 7.0f,    "Wood"),
        Lab02::StoreProduct(12, 13, 5.5f,    "Juice"),
        Lab02::StoreProduct(13, 1,  800.0f,  "Dog"),
        });

    std::cout << "Store_1 Products:" << std::endl << store_1 << std::endl;

    std::cout << "Store_1 Stocktaking Value: " << store_1.StoreStocktaking() << std::endl << std::endl;

    Lab02::Customer customer_1({ 0,1,8,12,6,11 }, { 2,1,2,3,1,7 });
    Lab02::Customer customer_2({ 2,3,4,6,9,11 }, { 2,1,1,3,1,5 });

    std::cout << "Customer_1 Products:" << std::endl << customer_1 << std::endl;
    std::cout << "Customer_2 Products:" << std::endl << customer_2 << std::endl;

    std::cout << " ********************** Stage_2 (1.5 Pts) ********************** " << std::endl;

    customer_1.CreateOrder(store_1);
    customer_2.CreateOrder(store_1);

    std::cout << "Store_1 Products:" << std::endl << store_1 << std::endl;
    std::cout << "Store_1 Stocktaking Value: " << store_1.StoreStocktaking() << std::endl << std::endl;
    std::cout << "Customer_1 Products:" << std::endl << customer_1 << std::endl;
    std::cout << "Customer_2 Products:" << std::endl << customer_2 << std::endl;

    store_1.RefillTheStore({{2,8},{3,7},{4,6},{6,3},{9,2},{11,9}});

    std::cout << "Store_1 Products:" << std::endl << store_1 << std::endl;
    std::cout << "Store_1 Stocktaking Value: " << store_1.StoreStocktaking() << std::endl << std::endl;

    customer_2.CreateOrder(store_1);

    std::cout << "Customer_1 Products:" << std::endl << customer_1 << std::endl;
    std::cout << "Customer_2 Products:" << std::endl << customer_2 << std::endl;

    std::cout << "Store_1 Stocktaking Value: " << store_1.StoreStocktaking() << std::endl << std::endl;

    std::cout << " ********************** Stage_3 (1.5 Pts) ********************** " << std::endl;

    std::cout << std::endl << "Store_1 Stocktaking Value: " << store_1.StoreStocktaking() << std::endl;
    std::cout << "Performing Inflation With 1.2 Coefficient!" << std::endl;

    store_1.PerformInflation(1.2f);

    std::cout << "Store_1 Stocktaking Value: " << store_1.StoreStocktaking() << std::endl << std::endl;

    auto maxProduct = store_1.GetMostValuableProduct();

    std::cout << "Maximum Value Product: " << std::setw(10) << maxProduct.productName << " (" << std::setw(2) << maxProduct.productID << ") With Quantity: " << maxProduct.productQuantity << " And Price: " << maxProduct.productPrice << std::endl << std::endl;

    std::cout << "Store_1 Products:" << std::endl << store_1 << std::endl;
    std::cout << "Products With Quantity Up To 5:" << std::endl;
    
    auto productsWithQuantityUpTo = store_1.GetProductsWithQuantityUpTo(5);

    for (const Lab02::StoreProduct& storeItem : productsWithQuantityUpTo)
        std::cout << "Product: " << std::setw(10) << storeItem.productName << " (" << std::setw(2) << storeItem.productID << ") With Quantity: " << storeItem.productQuantity << std::endl;

    std::cout << " ********************** Stage_4 (1.0 Pts) ********************** " << std::endl;

    std::cout << "Store_1 Products:" << std::endl << store_1 << std::endl;

    std::cout << "Performing Cheap Order!" << std::endl << std::endl;

    customer_2.CreateCheapOrder(store_1, 6);

    std::cout << "Store_1 Products:" << std::endl << store_1 << std::endl;

    customer_2.CreateCheapOrder(store_1, 15);

    std::cout << "Store_1 Products:" << std::endl << store_1 << std::endl;

    return EXIT_SUCCESS;
}
