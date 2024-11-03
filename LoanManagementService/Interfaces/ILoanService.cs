using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LoanManagementService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoanService" in both code and config file together.
    [ServiceContract]
    public interface ILoanService
    {
        [OperationContract]
        bool IssueLoan(int userId, int bookId);

        [OperationContract]
        bool ReturnLoan(int loanId);

        [OperationContract]
        Loan GetLoanDetails(int loanId);

        [OperationContract]
        List<Loan> GetAllActiveLoans();
    }
}
