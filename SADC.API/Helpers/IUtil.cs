﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADC.API.Helpers
{
    public interface IUtil
    {
        Task<string> SaveImage(IFormFile imageFile, string destiny);
        void DeleteImage(string imageName, string destiny);
    }
}
