# Casino Provider

A Full-Stack Slot Machine simulation built with **.NET 8**. This project demonstrates the architecture of a remote game server (RGS), separating the game client from the backend logic.

## 🛠 Tech Stack

* **Frontend:** Blazor WebAssembly (WASM)
    * Custom CSS (No external UI libraries used)
    * C# for client-side logic and state management
* **Backend:** ASP.NET Core Web API
    * RESTful API endpoints
    * Server-side game logic and RNG
* **Language:** C# (.NET 8)

## 📂 Application Structure

The client application is structured into two main distinct areas:

1.  **Lobby (Home Page `/`):**
    The entry point of the application. It serves as a visual landing page featuring the branding, promotional banners, and navigation to the game areas.
    
2.  **Game Room (`/casino`):**
    This is where the actual "Play Slots" experience takes place. This page hosts the Slot Machine component and is the only part of the application that establishes a connection with the RGS Server.



[Image of client server architecture diagram]


## 📐 Data Flow (Game Logic)

Interaction with the backend occurs specifically when the user is on the **Game Room** page:

1.  User clicks the **"SPIN"** button.
2.  Client sends a `POST` request to the API endpoint (`api/casino/spin`) containing the bet amount.
3.  **RgsServer** receives the request, executes the RNG (Random Number Generator) to determine symbols, and calculates wins based on the paytable.
4.  Server responds with a JSON object containing the reel positions, win amount, and updated balance.
5.  Client receives the data and renders the visual result (animations and balance update).

## ⚠️ Production vs. Demo Considerations

This project is a **technical demo**. To keep the architecture focused on the core logic, specific design choices were made that differ from a real-money gambling system.

In a production environment handling real money, the following security and architectural changes would be implemented:

* **State Management:** Currently, the client maintains session state. In a production system, the balance is never trusted from the client side; it is retrieved strictly from a secure database on the server.
* **Database:** This demo uses in-memory storage. A real system uses a transactional database (SQL Server/PostgreSQL) to ensure ACID compliance for every financial transaction.
* **Authentication:** Integration with JWT/OAuth to secure endpoints and identify users.
* **RNG:** Implementation of a certified CSPRNG (Cryptographically Secure Pseudo-Random Number Generator) instead of the standard .NET `Random` class.