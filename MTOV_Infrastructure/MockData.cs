using MTOV_Domain.Models;

namespace MTOV_Infrastructure
{
    /// <summary>
    /// Mock data for the assessment
    /// </summary>
    public static class MockData
    {
        public static List<AccountModel> GetAccounts()
        {
            return
            [
                new()
                {
                    TradingPlatform = "MT5",
                    AccountId = "12345",
                    Balance = 10000.50m,
                    Equity = 9700.25m,
                    MarginLevel = 325.67m,
                    LastLogin = DateTime.Parse("2025-07-21T14:10:00Z").ToUniversalTime(),
                    Status = "Active"
                },
                new()
                {
                    TradingPlatform = "MT5",
                    AccountId = "8801001",
                    Balance = 70000.50m,
                    Equity = 7700.25m,
                    MarginLevel = 725.67m,
                    LastLogin = DateTime.Parse("2025-08-21T07:10:00Z").ToUniversalTime(),
                    Status = "Active"
                },
                new()
                {
                    TradingPlatform = "MT5",
                    AccountId = "8801002",
                    Balance = 60000.50m,
                    Equity = 6700.25m,
                    MarginLevel = 625.67m,
                    LastLogin = DateTime.Parse("2025-07-21T08:10:00Z").ToUniversalTime(),
                    Status = "Active"
                },
                new()
                {
                    TradingPlatform = "MT5",
                    AccountId = "8801003",
                    Balance = 60000.50m,
                    Equity = 6700.25m,
                    MarginLevel = 625.67m,
                    LastLogin = DateTime.Parse("2025-03-21T01:10:00Z").ToUniversalTime(),
                    Status = "Active"
                }
            ];
        }

        public static List<TradesModel> GetOpenTrades()
        {
            return
            [
                new() {
                    AccountId= "12345",
                    TradingPlatform = "MT5",
                    Ticket = "10001",
                    Symbol = "EURUSD",
                    Volume = 1.0m,
                    Profit = 100.20m
                },
                new ()
                {
                    AccountId= "12345",
                    TradingPlatform = "MT5",
                    Ticket = "10002",
                    Symbol = "GBPUSD",
                    Volume = 0.5m,
                    Profit = -45.10m
                },
                new ()
                {
                    AccountId= "12345",
                    TradingPlatform = "MT5",
                    Ticket = "10003",
                    Symbol = "USDJPY",
                    Volume = 0.75m,
                    Profit = 75.30m
                },
                new() {
                    AccountId= "8801001",
                    TradingPlatform = "MT5",
                    Ticket = "10004",
                    Symbol = "EURUSD",
                    Volume = 1.0m,
                    Profit = 67.20m
                },
                new ()
                {
                    AccountId= "8801001",
                    TradingPlatform = "MT5",
                    Ticket = "10005",
                    Symbol = "GBPUSD",
                    Volume = 0.5m,
                    Profit = -5.10m
                },
                new ()
                {
                    AccountId= "8801001",
                    TradingPlatform = "MT5",
                    Ticket = "10006",
                    Symbol = "USDJPY",
                    Volume = 0.75m,
                    Profit = 15.30m
                },
                new ()
                {
                    AccountId= "8801001",
                    TradingPlatform = "MT5",
                    Ticket = "10005",
                    Symbol = "GBPUSD",
                    Volume = 0.5m,
                    Profit = -500.10m
                },
                new ()
                {
                    AccountId= "8801001",
                    TradingPlatform = "MT5",
                    Ticket = "10006",
                    Symbol = "USDJPY",
                    Volume = 0.75m,
                    Profit = 1500.30m
                },
                new() {
                    AccountId= "8801002",
                    TradingPlatform = "MT5",
                    Ticket = "10001",
                    Symbol = "EURUSD",
                    Volume = 1.0m,
                    Profit = 100.20m
                },
                new ()
                {
                    AccountId= "8801002",
                    TradingPlatform = "MT5",
                    Ticket = "10002",
                    Symbol = "GBPUSD",
                    Volume = 0.5m,
                    Profit = -45.10m
                },
                new ()
                {
                    AccountId= "8801002",
                    TradingPlatform = "MT5",
                    Ticket = "10003",
                    Symbol = "USDJPY",
                    Volume = 0.75m,
                    Profit = 75.30m
                }
            ];
        }
    }
}