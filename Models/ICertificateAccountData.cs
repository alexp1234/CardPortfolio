using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface ICertificateAccountData
    {
        IEnumerable<CertificateAccount> GetAllCertificateAccounts();
        CertificateAccount GetById(int id);
        int AddCertificateAccount(CertificateAccount certificateAccount);
        int RemoveCertificateAccount(CertificateAccount certificateAccount);
        IEnumerable<CertificateAccount> GetUnassignedCertificateAccounts();
        int Commit();


    }
}
