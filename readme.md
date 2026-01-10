# Slot Machine Simulation – Full Stack .NET 8

**Note:** This is a technical simulation/demo project. It does not involve real money and is intended for learning or demonstration purposes only.

A Full-Stack Slot Machine technical demo built with .NET 8, demonstrating a Remote Game Server (RGS) architecture, with clear separation between client-side UI and server-side game logic.

---

## Tech Stack

| Component | Technology | Details |
| :--- | :--- | :--- |
| **Frontend** | Blazor WebAssembly (WASM) | Client-side simulation and UI |
| | C# | State management and interactions |
| | Custom CSS | No external UI libraries used |
| **Backend** | ASP.NET Core Web API | RESTful API endpoints |
| | Server Logic | Server-side game logic and RNG (Random Number Generator) |
| **Language** | C# (.NET 8) | - |
| **Session** | Simulated | Simulated session management (for demo purposes only) |

![Casino Demo Preview](assets/preview.png)

## Key Architectural Features

* **Server-Authoritative Logic:** The client is purely for visualization. All game logic, win calculations, and balance updates happen strictly on the server.
* **Strict State Validation:** The server validates every spin request against its internal state to prevent balance manipulation or negative funds.
* **Stateless Frontend:** The Blazor client does not hold the "truth". It synchronizes with the server state on initialization and after every transaction.
* **Secure Data Transfer:** The client sends only the user's intent (e.g., "Bet 1.00"), never the outcome or the user's current balance.

## Application Structure

The application is divided into two main areas:

### 1. Lobby (`/`)
* Entry point and landing page.
* Displays branding, promotional banners, and navigation to game areas.

### 2. Game Room (`/casino`)
* Hosts the Slot Machine component.
* Establishes connection with the RGS Server to fetch simulated results.

---

## Data Flow (Simulation Logic)

The primary interaction follows this flow:

1.  **Initialization:** On load, the Client requests the current balance from the Server (`GET /balance`) to ensure synchronization.
2.  **Action:** User clicks "SPIN". Client sends `POST api/casino/spin` with the bet amount only.
3.  **Validation:** Server checks its **in-memory state** to verify sufficient funds (Client balance is ignored for security).
4.  **Execution:** Server executes the RNG logic and calculates wins.
5.  **State Update:** Server updates the authoritative state and responds with the new balance and reel positions.
6.  **UI Update:** Client receives the data and animates the result.

> **Important:** All values are simulated for demo purposes only. No real money is involved.

---

## Production Considerations (for real-money systems)

In a real-world environment, additional security and architecture would be required:

* **Persistent Storage:** Currently, state is held In-Memory (RAM) for high-performance demonstration. In production, this would be replaced with a transactional Database (SQL) for persistence across server restarts.
* **Authentication:** JWT/OAuth to secure endpoints and identify users.
* **RNG:** Replace .NET Random with a certified Cryptographically Secure PRNG for fair gameplay.
* **Logging & Audit:** All transactions and game results logged server-side.
* **Rate Limiting & Security:** To prevent abuse and ensure system stability.

This project is for educational and demonstration purposes only. It does not promote or handle real gambling.

