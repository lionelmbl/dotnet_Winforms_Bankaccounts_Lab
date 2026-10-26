using System;

namespace WindowsFormsApp1
{
    public class BankAccount
    {
        private static Random random = new Random();

        public string Owner { get; set; }
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }

        public BankAccount(string owner)
        {
            if (string.IsNullOrWhiteSpace(owner))
                throw new ArgumentException("Owner name cannot be empty.");

            Owner = owner.Trim();
            AccountNumber = random.Next(100000000, 999999999).ToString();
            Balance = 0m;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                return false;

            if (amount > Balance)
                return false;

            Balance -= amount;
            return true;
        }
    }
}
