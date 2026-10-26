using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<BankAccount> accounts = new List<BankAccount>();
        private BindingSource bindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            AddSampleAccounts();
        }

        private void InitializeGrid()
        {
            bindingSource.DataSource = accounts;
            dgvAccounts.DataSource = bindingSource;

            if (dgvAccounts.Columns["Owner"] != null)
                dgvAccounts.Columns["Owner"].HeaderText = "Owner";
            if (dgvAccounts.Columns["AccountNumber"] != null)
                dgvAccounts.Columns["AccountNumber"].HeaderText = "Account Number";
            if (dgvAccounts.Columns["Balance"] != null)
                dgvAccounts.Columns["Balance"].HeaderText = "Amount";
        }

        private void AddSampleAccounts()
        {
            try
            {
                accounts.Add(new BankAccount("John Ndongo"));
                accounts.Add(new BankAccount("Mary Tamba"));
                bindingSource.ResetBindings(false);
            }
            catch
            {
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string ownerName = txtOwner.Text;

            if (string.IsNullOrWhiteSpace(ownerName))
            {
                MessageBox.Show("Please enter the owner's name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BankAccount account = new BankAccount(ownerName);
                accounts.Add(account);
                bindingSource.ResetBindings(false);
                txtOwner.Clear();
                txtOwner.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private BankAccount GetSelectedAccount()
        {
            if (dgvAccounts.SelectedRows.Count == 0)
                return null;

            int index = dgvAccounts.SelectedRows[0].Index;
            if (index < 0 || index >= accounts.Count)
                return null;

            return accounts[index];
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            BankAccount account = GetSelectedAccount();
            if (account == null)
            {
                MessageBox.Show("Please select an account from the table.", "No Account Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal amount = numAmount.Value;
            try
            {
                account.Deposit(amount);
                bindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            BankAccount account = GetSelectedAccount();
            if (account == null)
            {
                MessageBox.Show("Please select an account from the table.", "No Account Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal amount = numAmount.Value;
            if (account.Withdraw(amount))
            {
                bindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Withdrawal failed. Amount must be greater than zero and not exceed the available balance.", "Invalid Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
