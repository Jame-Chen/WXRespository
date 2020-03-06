using Reponsitory.Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Service
{
    public partial class UploadFileService : BaseService<UploadFile>
    {
        public UploadFileService(IUnitWork UnitWork, IReponsitory<UploadFile> reponsitory) : base(UnitWork, reponsitory)
        {

        }

    
    }
}
