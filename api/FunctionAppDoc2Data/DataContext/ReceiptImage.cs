using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class ReceiptImage
    {
        public Guid ReceiptImageId { get; set; }
        public Guid ReceiptId { get; set; }
        public string OriginalFileName { get; set; }
        public string ImagePath { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public bool IsDelete { get; set; }

        public virtual Receipt Receipt { get; set; }
    }
}
