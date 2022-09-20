using System;
using System.Collections.Generic;
using System.Text;

namespace MySuperBank {
    public class BankAccount {
        public string Number { get; }
        public decimal Balance {             
            get 
            {
                decimal balance = 0;
                foreach (var item in allTransactions) {
                    balance += item.Amount;
                }
                return balance;
            } 
        }
        public string Owner { get; }

        private int Seed = 123;

        public List<Transaction> allTransactions = new List<Transaction>();

        public BankAccount(string name, int amount) {
            this.Owner = name;
            //this.Balance = initialBalance;
            MakeDeposit(amount, DateTime.Now, "Initial balance from first deposit.");
            this.Number = Seed.ToString();
            Seed++;
        }

        public void MakeDeposit(decimal amount, DateTime date, string note) {
            if (amount <= 0) {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note) {
            if (amount <= 0) {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0) {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        public string GetAccountHistory() {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Date\t\tAmount\tNote");

            foreach (var item in allTransactions) {
                //sb.AppendLine(item.Date.ToShortDateString() + "\t" + item.Amount + "\t" + item.Notes);
                sb.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{item.Notes}");
            }

            return sb.ToString();
        }
    }
}
