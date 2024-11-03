using System;
using System.Runtime.Serialization;

namespace LoanManagementService
{
    [DataContract]
    public class Loan
    {
        [DataMember]
        public int LoanID { get; private set; }

        [DataMember]
        public string BookID { get; private set; }

        [DataMember]
        public string MemberID { get; private set; }

        [DataMember]
        public string LibrarianID { get; private set; }

        [DataMember]
        public DateTime IssueDate { get; private set; }

        [DataMember]
        public DateTime DueDate { get; private set; }

        [DataMember]
        public DateTime? ReturnDate { get; private set; }

        [DataMember]
        public int RenewalsCount { get; private set; } = 0;

        [DataMember]
        public int MaxRenewals { get; private set; } = 2;

        [DataMember]
        public LoanStatus Status { get; private set; } = LoanStatus.ACTIVE;

        [DataMember]
        public decimal FineAmount { get; private set; } = 0.0m;

        [DataMember]
        public bool FinePaid { get; private set; } = false;

        [DataMember]
        public string Notes { get; private set; }

        // Constructor
        public Loan(int loanId, string bookId, string memberId, string librarianId)
        {
            LoanID = loanId;
            BookID = bookId;
            MemberID = memberId;
            LibrarianID = librarianId;
            IssueDate = DateTime.Now;
            DueDate = IssueDate.AddDays(14);
        }

        public Loan(int loanId, string bookId, string memberId, string librarianId, DateTime issueDate = default, DateTime dueDate = default, DateTime? returnDate = null, int renewalsCount = 0, int maxRenewals = 2, LoanStatus status = LoanStatus.ACTIVE, decimal fineAmount = 0.0m, bool finePaid = false, string notes = "")
        {
            LoanID = loanId;
            BookID = bookId;
            MemberID = memberId;
            LibrarianID = librarianId;
            IssueDate = issueDate == default ? DateTime.Now : issueDate;
            DueDate = dueDate == default ? IssueDate.AddDays(14) : dueDate;
            ReturnDate = returnDate;
            RenewalsCount = renewalsCount;
            MaxRenewals = maxRenewals;
            Status = status;
            FineAmount = fineAmount;
            FinePaid = finePaid;
            Notes = notes;
        }



        // Core Methods (similar to previous implementation)
        public bool RenewLoan(string librarianId)
        {
            if (RenewalsCount < MaxRenewals && (Status == LoanStatus.ACTIVE || Status == LoanStatus.RENEWED))
            {
                RenewalsCount++;
                DueDate = DueDate.AddDays(14);
                Status = LoanStatus.RENEWED;
                LibrarianID = librarianId;
                return true;
            }
            return false;
        }
        public void ReturnBook()
        {
            ReturnDate = DateTime.Now;
            Status = LoanStatus.RETURNED;

            if (ReturnDate > DueDate)
            {
                CalculateFine();
            }
        }
        private void CalculateFine()
        {
            if (ReturnDate > DueDate)
            {
                int overdueDays = (ReturnDate.Value - DueDate).Days;
                FineAmount = overdueDays * 0.50m;
            }
        }
        public void MarkAsLost(decimal replacementFee)
        {
            Status = LoanStatus.LOST;
            FineAmount += replacementFee;
        }

        public void MarkAsReturned(DateTime returnDate)
        {
            if (Status == LoanStatus.RETURNED)
            {
                throw new InvalidOperationException("Loan is already marked as returned.");
            }

            Status = LoanStatus.RETURNED;
            ReturnDate = returnDate;
        }


        public void CheckStatus()
        {
            if (DateTime.Now > DueDate && (Status == LoanStatus.ACTIVE || Status == LoanStatus.RENEWED))
            {
                Status = LoanStatus.OVERDUE;
                CalculateFine();
            }
        }

    }

    [DataContract]
    public enum LoanStatus
    {
        [EnumMember]
        ACTIVE,

        [EnumMember]
        OVERDUE,

        [EnumMember]
        RETURNED,

        [EnumMember]
        RENEWED,

        [EnumMember]
        LOST
    }
}
