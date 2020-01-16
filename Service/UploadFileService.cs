
using Sys.Reponsitory.Domain.Model;
using Sys.Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Service
{
    public class UploadFileService : BaseService<UploadFile>
    {
        public UploadFileService(IUnitWork UnitWork, IReponsitory<UploadFile> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}

