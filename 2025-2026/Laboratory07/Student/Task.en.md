# Lab15: Synchronization - OMEGALIDL

There is a flash sale on the new "Lidlomix 3000". Crowds are gathering outside the shop since dawn. To keep everything civil, the management has decided to enforce strict safety measures.

The program accepts three arguments:
* `M` - number of customers (`20 <= M <= 100`)
* `N` - number of restock workers (`3 <= N <= 12`, `N % 3 = 0`)
* `K` - number of cashiers (`2 <= K <= 5`)

Your task is to simulate the shop operation using Tasks and asynchronous synchronization.

### Stages:

1. **[3p]** Create `M` customer tasks. The shop capacity is limited to `8` customers. Use a `SemaphoreSlim` to control access. Upon entering, the customer prints: `CUSTOMER <ID>: Entered the shop`, waits 200ms using `Task.Delay`, and then leaves the shop (releasing the semaphore).

2. **[4p]** Create `N` worker tasks for restocking. Carrying a device requires a team of 3 workers. Workers work in fixed teams of 3 in a loop until at least `M` deliveries are made across all teams. Use a barrier to synchronize teams. When 3 workers gather, they print `TEAM <TEAM_ID>: Delivering stock`, wait 400ms, and increment the stock count. Customers wait for stock if it's 0. Once available, they decrement the count and print `CUSTOMER <ID>: Picked up Lidlomix`. You can use a semaphore for counting the stock.

3. **[3p]** Implement a checkout queue. Instead of leaving immediately, customers must join a FIFO blocking collection with capacity of 5. If it's full, the customer waits. Create `K` cashier tasks. A cashier takes a customer from the queue, prints `CASHIER <CashID>: Serving Customer <CustID>`, and waits 300ms. The customer must wait until the cashier finishes (you may use a `SemaphoreSlim`). Once done, the customer prints `CUSTOMER <CustID>: Paid and leaving`.

4. **[2p]** Handle `SIGINT`. All tasks must be notified to exit via `CancellationToken`. Ensure no task is left hanging on any synchronization primitive (barrier, semaphore, or collection). Cleanup all resources.
