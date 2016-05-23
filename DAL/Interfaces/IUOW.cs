using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces.UOW;

namespace DAL.Interfaces
{
    public interface IUOW : BaseIUOW

    {
       
        void RefreshAllEntities();

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        //UOW Methods, that dont fit into specific repo
    }
}