using System;
using System.Collections.Generic;
using System.Text;

namespace RegisterApp.Models.APIModels
{
    public class APIReqeustStatus
    {
        public APIReqeustStatus()
        {
            ErrorMessage = new List<string>();
        }
        public string RequestStatus { get; set; }

        public List<string> ErrorMessage { get; set; }
    }
}
