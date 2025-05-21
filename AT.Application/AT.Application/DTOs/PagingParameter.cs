﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Application.DTOs
{
    public class PagingParameter
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
