using System;
using System.Collections.Generic;

public abstract class BankAccount
{
    private double balance;

    public double Balance
    {
        get => balance;
        protected set => balance = Math.Max(value, 0);
    }

    public abstract void SendTransfer(double amount, BankAccount recipient);
    public abstract void ReceiveTransfer(double amount, BankAccount sender);
    public abstract void DisplayBalance();
}

public class Account : BankAccount
{
    public Account(double initialBalance) => Balance = initialBalance;

    public override void SendTransfer(double amount, BankAccount recipient)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            recipient.ReceiveTransfer(amount, this);
            Console.WriteLine($"Transfer of ${amount} sent.");
        }
        else
        {
            Console.WriteLine("Insufficient funds for the transfer.");
        }
    }

    public override void ReceiveTransfer(double amount, BankAccount sender)
    {
        Balance += amount;
        Console.WriteLine($"Received ${amount} from {sender}.");
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"Account Balance: ${Balance}");
    }

    public override string ToString() => "Account";
}

public class SmartAccount : Account
{
    private int rewardsPoints;

    public SmartAccount(double initialBalance, int initialRewardsPoints) : base(initialBalance) =>
        RewardsPoints = initialRewardsPoints;

    private int RewardsPoints
    {
        get => rewardsPoints;
        set => rewardsPoints = Math.Max(value, 0); 
    }

    public override void ReceiveTransfer(double amount, BankAccount sender)
    {
        base.ReceiveTransfer(amount, sender);
        RewardsPoints += (int)(amount * 0.1);
        Console.WriteLine($"Earned {amount * 0.1} rewards points. Total points: {RewardsPoints}");
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"SmartAccount Balance: ${Balance}, Rewards Points: {RewardsPoints}");
    }

    public override string ToString() => "SmartAccount";
}

class Program
{
    static void Main()
    {
        BankAccount account1 = new Account(1000);
        BankAccount account2 = new SmartAccount(500, 50);

        Console.WriteLine("Initial Balances:");
        account1.DisplayBalance();
        account2.DisplayBalance();

        Console.Write("\nEnter the transfer amount from Account 1 to Account 2: ");
        double transferAmount = Convert.ToDouble(Console.ReadLine());

        account1.SendTransfer(transferAmount, account2);

        Console.WriteLine("\nUpdated Balances after Transfer:");
        account1.DisplayBalance();
        account2.DisplayBalance();

        Console.Write("\nEnter the transfer amount from Account 2 to Account 1: ");
        transferAmount = Convert.ToDouble(Console.ReadLine());

        account2.SendTransfer(transferAmount, account1);

        Console.WriteLine("\nFinal Balances after Transfer:");
        account1.DisplayBalance();
        account2.DisplayBalance();
    }
}
