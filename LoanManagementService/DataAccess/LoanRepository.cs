using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace LoanManagementService.DataAccess
{
    public class LoanRepository
    {
        private readonly string _connectionString;

        public LoanRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LibraryDbConnection"].ConnectionString;
        }

        // Get all loans
        public List<Loan> GetAllLoans()
        {
            var loans = new List<Loan>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Loans";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(MapToLoan(reader));
                    }
                }
            }

            return loans;
        }

        // Get a loan by ID
        public Loan GetLoanById(int loanId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Loans WHERE loan_id = @LoanID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoanID", loanId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapToLoan(reader);
                        }
                    }
                }
            }

            return null;
        }

        // Add a new loan
        public void AddLoan(Loan loan)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO Loans (book_id, member_id, librarian_id, issue_date, due_date, renewals_count, max_renewals, status, fine_amount, fine_paid, notes) 
                    VALUES (@BookID, @MemberID, @LibrarianID, @IssueDate, @DueDate, @RenewalsCount, @MaxRenewals, @Status, @FineAmount, @FinePaid, @Notes)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookID", loan.BookID);
                    command.Parameters.AddWithValue("@MemberID", loan.MemberID);
                    command.Parameters.AddWithValue("@LibrarianID", loan.LibrarianID);
                    command.Parameters.AddWithValue("@IssueDate", loan.IssueDate);
                    command.Parameters.AddWithValue("@DueDate", loan.DueDate);
                    command.Parameters.AddWithValue("@RenewalsCount", loan.RenewalsCount);
                    command.Parameters.AddWithValue("@MaxRenewals", loan.MaxRenewals);
                    command.Parameters.AddWithValue("@Status", loan.Status.ToString());
                    command.Parameters.AddWithValue("@FineAmount", loan.FineAmount);
                    command.Parameters.AddWithValue("@FinePaid", loan.FinePaid);
                    command.Parameters.AddWithValue("@Notes", loan.Notes);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Update an existing loan
        public void UpdateLoan(Loan loan)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE Loans 
                    SET book_id = @BookID, member_id = @MemberID, librarian_id = @LibrarianID, 
                        issue_date = @IssueDate, due_date = @DueDate, return_date = @ReturnDate, 
                        renewals_count = @RenewalsCount, max_renewals = @MaxRenewals, 
                        status = @Status, fine_amount = @FineAmount, fine_paid = @FinePaid, notes = @Notes 
                    WHERE loan_id = @LoanID";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoanID", loan.LoanID);
                    command.Parameters.AddWithValue("@BookID", loan.BookID);
                    command.Parameters.AddWithValue("@MemberID", loan.MemberID);
                    command.Parameters.AddWithValue("@LibrarianID", loan.LibrarianID);
                    command.Parameters.AddWithValue("@IssueDate", loan.IssueDate);
                    command.Parameters.AddWithValue("@DueDate", loan.DueDate);
                    command.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate);
                    command.Parameters.AddWithValue("@RenewalsCount", loan.RenewalsCount);
                    command.Parameters.AddWithValue("@MaxRenewals", loan.MaxRenewals);
                    command.Parameters.AddWithValue("@Status", loan.Status.ToString());
                    command.Parameters.AddWithValue("@FineAmount", loan.FineAmount);
                    command.Parameters.AddWithValue("@FinePaid", loan.FinePaid);
                    command.Parameters.AddWithValue("@Notes", loan.Notes);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete a loan by ID
        public void DeleteLoan(int loanId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Loans WHERE loan_id = @LoanID";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoanID", loanId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Helper method to map data from MySQL reader to a Loan object
        private Loan MapToLoan(MySqlDataReader reader)
        {
            return new Loan(
                loanId: reader.GetInt32("loan_id"),
                bookId: reader.GetString("book_id"),
                memberId: reader.GetString("member_id"),
                librarianId: reader.GetString("librarian_id"),
                issueDate: reader.GetDateTime("issue_date"),
                dueDate: reader.GetDateTime("due_date"),
                returnDate: reader.IsDBNull(reader.GetOrdinal("return_date")) ? (DateTime?)null : reader.GetDateTime("return_date"),
                renewalsCount: reader.GetInt32("renewals_count"),
                maxRenewals: reader.GetInt32("max_renewals"),
                status: (LoanStatus)Enum.Parse(typeof(LoanStatus), reader.GetString("status")),
                fineAmount: reader.GetDecimal("fine_amount"),
                finePaid: reader.GetBoolean("fine_paid"),
                notes: reader.IsDBNull(reader.GetOrdinal("notes")) ? "" : reader.GetString("notes")
             );
        }

    }
}
