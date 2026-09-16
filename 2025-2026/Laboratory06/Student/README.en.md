# Laboratory Task: Network Streams, Pipes, Memory-Mapped Files

**Role:** Lead Network Engineer  
**Deadline:** 2 Hours to Launch  
**Status:** Critical Failure

## The Situation
Congratulations! You are the lead network developer for the highly anticipated **Quack III Arena**. The game looks great, the physics are bouncy, and the ducks are ready to fight.

However, the previous developer (an intern who has since "migrated" to another company) left the networking code in a shambles. Clients can't connect, messages aren't being sent or received correctly, and the entire communication layer is broken, leading to random disconnections and instability.

**We launch in 2 hours.** Your job is to implement the network communication so that clients can connect, send input, and receive game state updates reliably and efficiently.

## The Protocol
The game uses a custom TCP-based protocol mixed with JSON and Binary payloads. Every message sent over the wire follows a strict 5-byte header format.

### Message Framing
```text
[  Length  ] [  Type  ] [      Payload      ]
| 4 Bytes  | | 1 Byte | |     N Bytes ...   |
|  Int32   | |  Enum  | |  JSON/Binary Data |
```

1.  **Payload Size (4 bytes):** A 32-bit integer indicating how many bytes of the payload follow.
2.  **Message Type (1 byte):** A byte representing the `MessageType` enum (e.g., `Join`, `Input`, `UpdateState`).
3.  **Payload (N bytes):** The serialized data.

**Important:** The client and server must strictly adhere to this framing. If you read too few or too many bytes, the connection will desynchronize, causing critical communication failures.

## Your Mission

You will find `// TODO` comments in the codebase (`Quack.Messages` and `Quack.Client` projects). Complete the following stages to save the launch.

**Note:** Each stage can be verified using the automated tests provided in the `Quack.Tests` project.

### Stage 1: Message Serialization (3 points)
**Project:** `Quack.Messages`  
**Files:** `JsonMessages.cs`, `BinaryMessages.cs`

> Before any communication can happen, game objects need to be converted into bytes (serialize) and bytes back into game objects (deserialize). This is the foundation of network communication.

*   **Tasks:**
    1.  **JSON Messages (`JsonMessages.cs`):** Implement `Serialize()` and `Deserialize()` methods.
    2.  **Binary Messages (`BinaryMessages.cs`):** Implement `Serialize()` and `Deserialize()` for the custom binary format.

#### Binary Layout Specification
You must adhere to the following byte layout for the binary messages to ensure compatibility with the server:

**1. JoinMessage**
Used when a player connects to the arena.
*   **Size:** 4 bytes for Name Length + N bytes for Name Data.
*   **Layout:**
    *   **Name Length (4 bytes):** A standard `Int32` (Little Endian) representing the number of UTF8 bytes in the `Name` string.
    *   **Name Data (N bytes):** The UTF8 encoded bytes of the player's `Name`.

**2. InputMessage**
Used to send player controls. Designed for minimal bandwidth.
*   **Size:** Exactly 1 byte.
*   **Layout:** A single byte, where each bit represents a specific input state.
    *   **Bit 0:** `Up` (true if key is pressed, false otherwise)
    *   **Bit 1:** `Down` (true if key is pressed, false otherwise)
    *   **Bit 2:** `Left` (true if key is pressed, false otherwise)
    *   **Bit 3:** `Right` (true if key is pressed, false otherwise)
    *   **Bit 4:** `Sprint` (true if key is pressed, false otherwise)
    *   *Bits 5-7 are unused.*

### Stage 2: Client Connection (2 points)
**Project:** `Quack.Client`  
**File:** `GameClient.cs`

> With your messages now able to be converted, the next critical step is for the client to establish a connection to the server. Without this, your duck is forever alone.

*   **Task:** Implement the `ConnectAsync` method.
*   **Requirements:**
    1.  Resolve the host (e.g., `localhost`, `192.168.1.111`, `pw.mini.edu.pl`) to an IP address. If it already is an IP address simply parse it.
    2.  Establish a `TcpClient` connection to the specified server IP address and port.

### Stage 3: The Reading Loop (4 points)
**Project:** `Quack.Client`  
**File:** `NetworkConnection.cs`

> Once connected, your client needs to continuously listen for incoming game state updates from the server, adhering strictly to the defined message protocol. Failure here leads to desynchronization and a very confused duck.

*   **Task:** Implement the `StartReadingAsync` method.
*   **Requirements:**
    1.  Read the messages in a continuous loop that reads from the `NetworkStream` while the client is connected.
    2.  **Read Header:** Read exactly 5 bytes from the stream. From these bytes, extract the 4-byte `PayloadLength` and the 1-byte `MessageType`.
    3.  **Read Payload:** Read exactly `PayloadLength` bytes into a buffer.
    4.  **Deserialize:** Convert these payload bytes into an `IJsonMessage` object (using your Stage 1 logic).
    5.  **Event:** Invoke the `MessageReceived` event with the deserialized message.

### Stage 4: Sending Messages (3 points)
**Project:** `Quack.Client`  
**File:** `NetworkConnection.cs`

> Finally, enable the client to talk back.

*   **Task:** Implement the `SendAsync` method.
*   **Requirements:**
    1.  **Serialize Message:** Convert the `IMessage` into its binary byte representation (using your Stage 1 logic).
    2.  **Construct Header:** Create the 5-byte header containing the `PayloadLength` (from your serialized message) and the `MessageType`.
    3.  **Send Data:** Write the 5-byte header to the `NetworkStream`, immediately followed by the serialized message payload.
*   **Hint:** Consider using `ArrayPool` to efficiently manage temporary buffers.
