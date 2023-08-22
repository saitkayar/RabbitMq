using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDevletPdf.Common.Model
{
    public class CreateDocumentModel
    {
        public int UserId { get; set; }
        public string Url { get; set; }
        public DocumentTyoe DocumentTyoe { get; set; }
    }

    public enum DocumentTyoe
    {
        Pdf,Html,Png
    }
}
