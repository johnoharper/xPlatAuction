using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xPlatAuction.Backend.DataObjects
{
    public class Comment : StorageData
    {
        public string CommentText { get; set; }
    }
}
