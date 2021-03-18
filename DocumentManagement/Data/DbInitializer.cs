using DocumentManagement.Data;
using DocumentManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentManagement.Data
{
    public class DbInitializer
    {
        public static void Initialize(DocumentContext context)
        {
            //Create a migration
            context.Database.Migrate();
        }
    }
}
