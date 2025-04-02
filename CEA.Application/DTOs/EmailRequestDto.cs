using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Application.DTOs
{
    public class EmailRequestDto
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string?[] Cc { get; set; } = Array.Empty<string>();
        public IFormFile? Attachment { get; set; } = null;
    }
}
